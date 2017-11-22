using UnityEngine;
using System.Collections;
using RAGE.Analytics;

public class WebOpener : MonoBehaviour {

	public string type = "";

	void Start () {
	}
	void Update () {
	}

    public void OpenWeb(string web)
    {
        Application.OpenURL(web);
    }

    public void OpenSurvey()
    {
		string survey = "";
		
		
		survey = PlayerPrefs.GetString("LimesurveyPre");
		type = "pre";

		if (PlayerPrefs.HasKey("CurrentSurvey"))
			type = PlayerPrefs.GetString("CurrentSurvey");

		if (type == "pre")
			survey = PlayerPrefs.GetString("LimesurveyPre");
		else if (type == "post")
			survey = PlayerPrefs.GetString("LimesurveyPost");
		else if (type == "tea")
			survey = PlayerPrefs.GetString("LimesurveyTea");
		
		
		
		string url = PlayerPrefs.GetString("LimesurveyHost") + "surveys/survey/" + survey + "?token=" + PlayerPrefs.GetString("LimesurveyToken");
		if (!url.Contains("http://") && !url.Contains("https://"))
			url = "http://" + url;

		Tracker.T.accessible.Accessed(type+"Survey");
		Application.OpenURL(url);
    }
}
