using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RAGE.Analytics;

public class NameSaver : MonoBehaviour {
    public Text t;
	public GameObject TrackerObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SaveName()
    {
		string path = Application.persistentDataPath;

		if (!path.EndsWith ("/")) {
			path += "/";
		}

		if(PlayerPrefs.HasKey("username") && PlayerPrefs.GetString("username") != t.text){
			if(System.IO.File.Exists(path + "tracesRaw.csv")){
				System.IO.File.AppendAllText (path + PlayerPrefs.GetString("username") + ".csv.backup", System.IO.File.ReadAllText(path + "tracesRaw.csv"));
				System.IO.File.Delete (path + "tracesRaw.csv");
			}
		}
        PlayerPrefs.SetString("username", t.text);
		PlayerPrefs.Save();
		//PlayerPrefs.DeleteAll();

		
		Tracker.T.username = PlayerPrefs.GetString("username");
		Tracker.T.password = PlayerPrefs.GetString("username");

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

		TrackerObject.SetActive(true);
		//End tracker data loading
	}
}
