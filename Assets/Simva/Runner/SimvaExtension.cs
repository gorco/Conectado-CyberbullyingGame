using AssetPackage;
using SimpleJSON;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityFx.Async;
using UnityFx.Async.Promises;
using SimvaPlugin;
using Simva.Api;
using Simva.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Simva
{
    public class SimvaExtension : MonoBehaviour
    {
		public UnityEngine.UI.Text tokenField;
		public float flushInterval = 3;

		private static SimvaExtension instance;

        public delegate void LoadingDelegate(bool loading);
        public delegate void ResponseDelegate(string message);

        private LoadingDelegate loadingListeners;
        private ResponseDelegate responseListeners;

        private AuthorizationInfo auth;
        private Schedule schedule;
        private SimvaApi<IStudentsApi> simvaController;
		private float nextFlush;
		private bool flushRequested = false;
		private bool allowQuitting = true;

		protected void Awake()
        {
            instance = this;
			DontDestroyOnLoad(this.gameObject);
        }

        public static SimvaExtension Instance
        {
            get
            {
                return instance;
            }
        }

        public Schedule Schedule
        {
            get
            {
                return schedule;
            }
        }

        public SimvaApi<IStudentsApi> API
        {
            get
            {
                return simvaController;
            }
        }

        public string Token
        {
            get
            {
                if (auth != null)
                {
                    return auth.RefreshToken;
                }
                else if (PlayerPrefs.HasKey("username"))
                {
                    return PlayerPrefs.GetString("username");
                }
                else if (PlayerPrefs.HasKey("Simva.Token"))
                {
                    return PlayerPrefs.GetString("Simva.Token");
                }
                else
                {
                    return "";
                }
            }
        }

        public bool IsActive
        {
            get
            {
                return this.simvaController != null && this.auth != null && Schedule != null;
            }
        }

        public string CurrentActivityId
        {
            get
            {
                if (schedule != null)
                {
                    return Schedule.Next;
                }
                return null;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return !string.IsNullOrEmpty(SimvaConf.Local.Study);
            }
        }

        public SimvaBridge SimvaBridge { get; private set; }

        public void Start()
        {
			NotifyLoading(true);
            if (!IsEnabled)
            {
                Debug.Log("No study was set for Simva");
				SceneManager.LoadScene("UserInfo");
				return;
            }

            if (IsActive)
            {
                // No need to restart
                return;
            }
			NotifyLoading(false);
		}
		
        public bool OnGameFinished()
        {
            if (IsActive)
            {
                Activity activity = GetActivity(CurrentActivityId);
                string activityType = activity.Type;
                if (activityType.Equals("gameplay", StringComparison.InvariantCultureIgnoreCase) 
                    && activity.Details != null && activity.Details.ContainsKey("backup") && (bool)activity.Details["backup"])
                {
                    string traces = SimvaBridge.Load(((TrackerAssetSettings)TrackerAsset.Instance.Settings).BackupFile);
                    SaveActivityAndContinue(CurrentActivityId, traces, true);
                }
                else
                {
                    Continue(CurrentActivityId, true);
                }

                return false;
            }
            else
            {
                // The application can be closed
                return true;
            }
        }

        public void InitUser()
        {
            LoginAndSchedule();
        }

        public void LoginAndSchedule()
        {
			var token = tokenField.text;
			if (string.IsNullOrEmpty(token))
			{
				NotifyManagers("Token must not be empty!");
			}


            NotifyLoading(true);
			NotifyManagers("");
			OpenIdUtility.tokenLogin = true;
            SimvaApi<IStudentsApi>.LoginWithToken(token)
                .Then(simvaController =>
                {
                    this.auth = simvaController.AuthorizationInfo;
                    this.simvaController = simvaController;
                    return UpdateSchedule();
                })
                .Then(schedule =>
                {
                    LaunchActivity(schedule.Next);
                })
                .Catch(error =>
                {
                    NotifyLoading(false);
                    NotifyManagers(error.Message);
                })
                .Finally(()=>
                {
                    OpenIdUtility.tokenLogin = false;
                });
        }

        public IAsyncOperation<Schedule> UpdateSchedule()
        {
            var webRequest = simvaController.Api.GetSchedule(simvaController.SimvaConf.Study);
            webRequest.Then(result =>
            {
                this.schedule = result;
            });
            return webRequest;
        }


        public IAsyncOperation SaveActivityAndContinue(string activityId, string traces, bool completed)
        {
            NotifyLoading(true);

            var body = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(traces))
            {
                body.Add("tofile", true);
                body.Add("result", traces);
            }

            return API.Api.SetResult(activityId, API.AuthorizationInfo.Username, body)
                .Then(() =>
                {
                    NotifyLoading(false);
                    return Continue(activityId, completed);
                })
                .Catch(error =>
                {
                    NotifyLoading(false);
                    NotifyManagers(error.Message);
                });
        }

        public IAsyncOperation Continue(string activityId, bool completed)
        {
            NotifyLoading(true);
            return API.Api.SetCompletion(activityId, API.AuthorizationInfo.Username, completed)
                .Then(() =>
                {
                    return UpdateSchedule();
                })
                .Then(schedule =>
                {
                    NotifyLoading(false);
                    LaunchActivity(schedule.Next);
                })
                .Catch(error =>
                {
                    NotifyLoading(false);
                    NotifyManagers(error.Message);
                });
        }

        public Activity GetActivity(string activityId)
        {
            if (schedule != null)
            {
                return Schedule.Activities[activityId];
            }
            return null;
        }

        public void LaunchActivity(string activityId)
        {
            if (activityId == null)
            {
				SceneManager.LoadScene("Credits");
            }
            else
            {
                Activity activity = GetActivity(activityId);
				Debug.Log(activity.Name);

                if (activity != null)
                {
                    switch (activity.Type)
                    {
                        case "limesurvey":
							SceneManager.LoadScene("_Survey");
							break;
                        case "gameplay":
                        default:
							var trackerConfig = new TrackerConfig();

							trackerConfig.setStorageType(TrackerConfig.StorageType.LOCAL);
							trackerConfig.setTraceFormat(TrackerConfig.TraceFormat.XAPI);
							trackerConfig.setRawCopy(true);
							trackerConfig.setDebug(true);

							if (ActivityHasDetails(activity, "realtime", "trace_storage"))
							{
								// Realtime
								trackerConfig.setStorageType(TrackerConfig.StorageType.NET);
								trackerConfig.setHost(simvaController.SimvaConf.URL);
								trackerConfig.setBasePath("");
								trackerConfig.setLoginEndpoint("/users/login");
								trackerConfig.setStartEndpoint("/activities/{0}/result");
								trackerConfig.setTrackEndpoint("/activities/{0}/result");
								trackerConfig.setTrackingCode(activityId);
								trackerConfig.setUseBearerOnTrackEndpoint(true);
							}
							Debug.Log("TrackingCode: " + activity.Id + " settings " + trackerConfig.getTrackingCode());

							if (ActivityHasDetails(activity, "backup"))
							{
								// Local
								trackerConfig.setRawCopy(true);
							}
							PlayerPrefs.SetInt("online", 1);
							SimvaBridge = new SimvaBridge(API.ApiClient);
							StartTracker(trackerConfig, SimvaBridge);
							SceneManager.LoadScene("UserInfo");


							break;
                    }
                }
            }
        }
		public void StartTracker(TrackerConfig config, IBridge bridge = null)
		{
			var trackerConfig = config;
			string domain = "";
			int port = 80;
			bool secure = false;

			try
			{
				if (config.getHost() != "")
				{
					string[] splitted = config.getHost().Split('/');

					if (splitted.Length > 1)
					{
						string[] host_splitted = splitted[2].Split(':');
						if (host_splitted.Length > 0)
						{
							domain = host_splitted[0];
							port = (host_splitted.Length > 1) ? int.Parse(host_splitted[1]) : (splitted[0] == "https:" ? 443 : 80);
							secure = splitted[0] == "https:";
						}
					}
				}
				else
				{
					config.setHost("localhost");
				}
			}
			catch (System.Exception e)
			{
				Debug.Log("Tracker error: Host bad format");
			}

			TrackerAsset.TraceFormats format;
			switch (config.getTraceFormat())
			{
				case TrackerConfig.TraceFormat.XAPI:
					format = TrackerAsset.TraceFormats.xapi;
					break;
				default:
					format = TrackerAsset.TraceFormats.csv;
					break;
			}

			TrackerAsset.StorageTypes storage;
			switch (config.getStorageType())
			{
				case TrackerConfig.StorageType.NET:
					storage = TrackerAsset.StorageTypes.net;
					break;
				default:
					storage = TrackerAsset.StorageTypes.local;
					break;
			}

			TrackerAssetSettings tracker_settings = new TrackerAssetSettings()
			{
				Host = domain,
				TrackingCode = config.getTrackingCode(),
				BasePath = trackerConfig.getBasePath() ?? "/api",
				LoginEndpoint = trackerConfig.getLoginEndpoint() ?? "login",
				StartEndpoint = trackerConfig.getStartEndpoint() ?? "proxy/gleaner/collector/start/{0}",
				TrackEndpoint = trackerConfig.getTrackEndpoint() ?? "proxy/gleaner/collector/track",
				Port = port,
				Secure = secure,
				StorageType = storage,
				TraceFormat = format,
				BackupStorage = config.getRawCopy(),
				UseBearerOnTrackEndpoint = trackerConfig.getUseBearerOnTrackEndpoint()
			};

			TrackerAsset.Instance.StrictMode = false;
			TrackerAsset.Instance.Bridge = bridge ?? new UnityBridge();
			TrackerAsset.Instance.Settings = tracker_settings;
			TrackerAsset.Instance.StrictMode = false;
			TrackerAsset.Instance.Start();
			allowQuitting = false;
			Application.wantsToQuit += Application_wantsToQuit;
		}

		private bool ActivityHasDetails(Activity activity, params string[] details)
        {
            if (activity.Details == null)
            {
                return false;
            }

            return details.Any(d => IsTrue(activity.Details, d));
        }

        private static bool IsTrue(Dictionary<string, object> details, string key)
        {
            return details.ContainsKey(key) && details[key] is bool && (bool)details[key];
        }

        // NOTIFIERS

        public void AddResponseManager(SimvaResponseManager manager)
        {
            if (manager)
            {
                // To make sure we only have one instance of a notify per manager
                // We first remove (as it is ignored if not present)
                responseListeners -= manager.Notify;
                // Then we add it
                responseListeners += manager.Notify;
            }
        }

        public void RemoveResponseManager(SimvaResponseManager manager)
        {
            if (manager)
            {
                // If a delegate is not present the method gets ignored
                responseListeners -= manager.Notify;
            }
        }

        public void AddLoadingManager(SimvaLoadingManager manager)
        {
            if (manager)
            {
                // To make sure we only have one instance of a notify per manager
                // We first remove (as it is ignored if not present)
                loadingListeners -= manager.IsLoading;
                // Then we add it
                loadingListeners += manager.IsLoading;
            }
        }

        public void RemoveLoadingManager(SimvaLoadingManager manager)
        {
            if (manager)
            {
                // If a delegate is not present the method gets ignored
                loadingListeners -= manager.IsLoading;
            }
        }

        public void NotifyManagers(string message)
        {
            if (responseListeners != null)
            {
                responseListeners(message);
            }
        }

        public void NotifyLoading(bool state)
        {
            if (loadingListeners != null)
            {
                loadingListeners(state);
            }
        }

		private bool Application_wantsToQuit()
		{
			if (!allowQuitting)
			{
				// We start the thread for a final
				System.Diagnostics.ProcessThreadCollection currentThreads = System.Diagnostics.Process.GetCurrentProcess().Threads;

				foreach (System.Diagnostics.ProcessThread thread in currentThreads)
				{
					UnityEngine.Debug.Log("Thread: " + thread.Id + " - " + thread.StartTime);
				}
				TrackerAsset.Instance.Exit(() =>
				{
					UnityEngine.Debug.Log("Tracker successfully exited! Goodbye!");
					allowQuitting = true;
					Application.Quit();
				});
			}

			return allowQuitting;
		}

		// <summary>
		// DONT USE THIS METHOD. UNITY INTERNAL MONOBEHAVIOUR.
		/// </summary>
		bool flushing = false;
		public void Update()
		{
			if (flushing || allowQuitting) // Allowquitting controls that tracker is running
			{
				return;
			}

			float delta = Time.deltaTime;
			if (flushInterval >= 0)
			{
				nextFlush -= delta;
				if (nextFlush <= 0)
				{
					flushRequested = true;
				}
				while (nextFlush <= 0)
				{
					nextFlush += flushInterval;
				}
			}
			if (flushRequested)
			{
				flushRequested = false;
				flushing = true;
				Debug.Log("Flushing...");
				TrackerAsset.Instance.Flush(() =>
				{
					flushing = false;
					Debug.Log("Flush done!");
				});
			}
		}



		public class TrackerConfig
		{
			public enum TraceFormat { CSV, XAPI };
			public enum StorageType { LOCAL, NET };

			private bool rawCopy = true;
			private int flushInterval = 3;
			private TraceFormat traceFormat = TraceFormat.CSV;
			private StorageType storageType = StorageType.LOCAL;

			private string host = null;
			private string basePath = null;
			private string loginEndpoint = null;
			private string startEndpoint = null;
			private string trackEndpoint = null;

			private bool useBearerOnTrackEndpoint = false;

			private string trackingCode = null;

			private bool debug = false;

			//#################################################
			//#################### GETTERS ####################
			//#################################################
			#region getters
			public bool getRawCopy()
			{
				return this.rawCopy;
			}
			public int getFlushInterval()
			{
				return this.flushInterval;
			}
			public TraceFormat getTraceFormat()
			{
				return this.traceFormat;
			}
			public StorageType getStorageType()
			{
				return this.storageType;
			}
			public string getHost()
			{
				return this.host;
			}
			public string getBasePath()
			{
				return this.basePath;
			}
			public string getLoginEndpoint()
			{
				return this.loginEndpoint;
			}
			public string getStartEndpoint()
			{
				return this.startEndpoint;
			}
			public string getTrackEndpoint()
			{
				return this.trackEndpoint;
			}
			public bool getUseBearerOnTrackEndpoint()
			{
				return this.useBearerOnTrackEndpoint;
			}
			public string getTrackingCode()
			{
				return this.trackingCode;
			}
			public bool getDebug()
			{
				return this.debug;
			}
			#endregion getters
			//#################################################
			//#################### SETTERS ####################
			//#################################################
			#region setters
			public void setRawCopy(bool rawCopy)
			{
				this.rawCopy = rawCopy;
			}
			public void setFlushInterval(int flushInterval)
			{
				this.flushInterval = flushInterval;
			}
			public void setTraceFormat(TraceFormat traceFormat)
			{
				this.traceFormat = traceFormat;
			}
			public void setStorageType(StorageType storageType)
			{
				this.storageType = storageType;
			}
			public void setHost(string host)
			{
				this.host = host;
			}
			public void setBasePath(string basePath)
			{
				this.basePath = basePath;
			}
			public void setLoginEndpoint(string loginEndpoint)
			{
				this.loginEndpoint = loginEndpoint;
			}
			public void setStartEndpoint(string startEndpoint)
			{
				this.startEndpoint = startEndpoint;
			}
			public void setTrackEndpoint(string trackEndpoint)
			{
				this.trackEndpoint = trackEndpoint;
			}
			public void setUseBearerOnTrackEndpoint(bool useBearerOnTrackEndpoint)
			{
				this.useBearerOnTrackEndpoint = useBearerOnTrackEndpoint;
			}
			public void setTrackingCode(string trackingCode)
			{
				this.trackingCode = trackingCode;
			}
			public void setDebug(bool debug)
			{
				this.debug = debug;
			}
			#endregion setters
			//#################################################
			//#################################################
			//#################################################
		}
	}
}
