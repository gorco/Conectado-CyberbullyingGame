using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostConfiguration : MonoBehaviour {

	string host = "";
	string survey_pre = "", survey_post = "", survey_tea = "", master_token_online = "", master_token_offline = "";
    private string proxy_protocol;
	private string activities_tracking;

	public SessionStart sessionObj;

	void Start()
	{
		SimpleJSON.JSONNode hostfile = new SimpleJSON.JSONClass();

#if !(UNITY_WEBPLAYER || UNITY_WEBGL)
		if (!System.IO.File.Exists("host.cfg"))
			hostfile.Add("limesurvey_host", "localhost:4000");
		else
			hostfile = SimpleJSON.JSON.Parse(System.IO.File.ReadAllText("host.cfg"));
#endif
		try
		{
			host = hostfile["limesurvey_host"];
			survey_pre = hostfile["limesurvey_pre"];
			survey_post = hostfile["limesurvey_post"];
			survey_tea = hostfile["limesurvey_tea"];
			master_token_online = hostfile["master_token_online"];
			master_token_offline = hostfile["master_token_offline"];

			activities_tracking = hostfile["activities_tracking"];
		}
		catch (Exception ex) { }

		PlayerPrefs.SetString("LimesurveyHost", host);
		if (survey_pre != "")
		{
			PlayerPrefs.SetString("LimesurveyPre", survey_pre);
			PlayerPrefs.SetString("CurrentSurvey", "pre");
		}
		if (survey_post != "")
			PlayerPrefs.SetString("LimesurveyPost", survey_post);
		else
			PlayerPrefs.DeleteKey("LimesurveyPost");

		if (survey_tea != "")
			PlayerPrefs.SetString("LimesurveyTea", survey_tea);
		else
			PlayerPrefs.DeleteKey("LimesurveyTea");

		if (master_token_online != "")
			PlayerPrefs.SetString("MasterTokenOnline", master_token_online);
		else
			PlayerPrefs.DeleteKey("MasterTokenOnline");

		if (master_token_offline != "")
			PlayerPrefs.SetString("MasterTokenOffline", master_token_offline);
		else
			PlayerPrefs.DeleteKey("MasterTokenOffline");

		PlayerPrefs.SetString("ActivitiesTracking", activities_tracking);
		if(activities_tracking == "" || activities_tracking == null) { 
			sessionObj.ValidateSession();
		}

		PlayerPrefs.Save();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
