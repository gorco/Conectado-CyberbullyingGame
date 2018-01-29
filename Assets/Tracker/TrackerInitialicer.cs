using RAGE.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerInitialicer : MonoBehaviour {

	/*private void Awake()
	{
		//PlayerPrefs.DeleteAll();
		if (PlayerPrefs.HasKey("username"))
		{
			Tracker.T.username = PlayerPrefs.GetString("username");
			Tracker.T.password = PlayerPrefs.GetString("username");
		}

		SimpleJSON.JSONNode hostfile = new SimpleJSON.JSONClass();

#if !(UNITY_WEBPLAYER || UNITY_WEBGL)
		if (!System.IO.File.Exists("host.cfg"))
		{
			hostfile.Add("host", new SimpleJSON.JSONData("http://192.168.175.117:3000/api/proxy/gleaner/collector/"));
			hostfile.Add("trackingCode", new SimpleJSON.JSONData("57d81d5585b094006eab04d6ndecvjlvjss8aor"));
			System.IO.File.WriteAllText("host.cfg", hostfile.ToString());
		}
		else
			hostfile = SimpleJSON.JSON.Parse(System.IO.File.ReadAllText("host.cfg"));
#endif

		Tracker.T.host = hostfile["host"];
		Tracker.T.trackingCode = hostfile["trackingCode"];
		//End tracker data loading
	}

	// Use this for initialization
	void Start () {
		//Tracker.T.alternative.Selected("InitTracker", "start", true, RAGE.Analytics.Formats.AlternativeTracker.Alternative.Question);

	}
	
	// Update is called once per frame
	void Update () {
		
	}*/
}
