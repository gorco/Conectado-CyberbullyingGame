using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RAGE.Net;
using RAGE.Analytics;

public class LimeSurveyValidator : MonoBehaviour {

    Net connection;
    public Text token, response;
	public string type;
	public  NameSaver nameSaver;

	private static int nextScene = 1;
	private static int initScene = 0;

	void Start()
	{
		connection = new Net(this);

	}
    
    void Update () {
	
	}

    public void validate()
    {
        string token = "";
		string master_token_online = PlayerPrefs.GetString("MasterTokenOnline");
		string master_token_offline = PlayerPrefs.GetString("MasterTokenOffline");

		if (this.token != null)
            token = this.token.text;
        else if (PlayerPrefs.HasKey("LimesurveyToken"))
            token = PlayerPrefs.GetString("LimesurveyToken");

		PlayerPrefs.SetInt ("online", 1);

		if (token == master_token_online || token == master_token_offline) {
			PlayerPrefs.SetString ("LimesurveyToken", "ADMIN");
			PlayerPrefs.SetString ("name", "ADMIN");

			if (token == master_token_offline) {
				PlayerPrefs.SetInt ("online", 0);
			}else
				PlayerPrefs.SetInt ("online", 1);
			nameSaver.SaveName();
			SceneManager.LoadScene (nextScene);
		}
		if (PlayerPrefs.GetString("LimesurveyHost") == "")
		{
			Debug.LogError("No LimeSurveyServer");
			PlayerPrefs.SetString("name", token);
			PlayerPrefs.SetString("LimesurveyToken", token);
			PlayerPrefs.Save();
			nameSaver.SaveName();
			SceneManager.LoadScene(nextScene);
		}
		else
		{
			connection.GET(PlayerPrefs.GetString("LimesurveyHost") + "surveys/validate?survey=" + PlayerPrefs.GetString("LimesurveyPre") + ((token.Length > 0) ? "&token=" + token : ""), new ValidateListener(nameSaver, response, token));
		}
    }

    public void completed()
    {
        string token = "";
        if (this.token != null)
            token = this.token.text;
        else if (PlayerPrefs.HasKey("LimesurveyToken"))
            token = PlayerPrefs.GetString("LimesurveyToken");

		string survey = PlayerPrefs.GetString ("LimesurveyPre");
		type = type == "" ? "pre" : type;

		if (PlayerPrefs.HasKey ("CurrentSurvey"))
			type = PlayerPrefs.GetString ("CurrentSurvey");

		if(type == "pre")
			survey = PlayerPrefs.GetString ("LimesurveyPre");
		else if(type == "post")
			survey = PlayerPrefs.GetString ("LimesurveyPost");
		else if (type == "tea")
			survey = PlayerPrefs.GetString("LimesurveyTea");

		Debug.Log(type);
		connection.GET(PlayerPrefs.GetString("LimesurveyHost") + "surveys/completed?survey=" + survey + ((token.Length > 0) ? "&token=" + token : ""), new CompleteListener(response, token, this.gameObject.GetComponent<SettingsApp>()));
		Debug.Log(PlayerPrefs.GetString("LimesurveyHost") + "surveys/completed?survey=" + survey + ((token.Length > 0) ? "&token=" + token : ""));
		
	}

    public class ValidateListener : Net.IRequestListener
    {
        Text response;
        string token;
		private NameSaver nameSaver;

        public ValidateListener(NameSaver saver, Text response, string token)
        {
            this.response = response;
            this.token = token;
			this.nameSaver = saver;
        }

        public void Error(string error)
        {
            SimpleJSON.JSONNode result = SimpleJSON.JSON.Parse(error);
			if (result != null && result ["message"] != null)
				response.text = result["message"];
			else
				response.text = error != ""? error : "Can't Connect";
        }

        public void Result(string data)
        {
			nameSaver.SaveName();
			PlayerPrefs.SetString("name", token);
            PlayerPrefs.SetString("LimesurveyToken", token);
            PlayerPrefs.Save();
			if (PlayerPrefs.HasKey ("LimesurveyPre")) {
				SceneManager.LoadScene ("_Survey");
			}else
                SceneManager.LoadScene(initScene);
        }
    }

	public class CompleteListener : Net.IRequestListener
    {
        Text response;
        string token;
		SettingsApp exit;

        public CompleteListener(Text response, string token)
        {
            this.response = response;
            this.token = token;
        }

		public CompleteListener(Text response, string token, SettingsApp exit)
		{
			this.response = response;
			this.token = token;
			this.exit = exit;
		}

		public void Error(string error)
        {
            SimpleJSON.JSONNode result = SimpleJSON.JSON.Parse(error);
			response.text = result["message"];
        }

        public void Result(string data)
        {
			string type = "pre";
			response.text = "";
			if (PlayerPrefs.HasKey ("CurrentSurvey"))
				type = PlayerPrefs.GetString ("CurrentSurvey");
			Tracker.T.completable.Completed(type+"Survey");

			if (type == "pre")
			{
				PlayerPrefs.SetInt("PreTestEnd", 1);
				PlayerPrefs.SetString("CurrentSurvey", "post");
				PlayerPrefs.Save();
				SceneManager.LoadScene(nextScene);
			}
			else if (type == "post")
			{
				PlayerPrefs.SetInt("PostTestEnd", 1);
				PlayerPrefs.SetString("CurrentSurvey", "end");
				exit.ExitGameConfirmed();
			}
			else if (type == "tea")
			{
				PlayerPrefs.SetInt("TeaTestEnd", 1);
				PlayerPrefs.SetString("CurrentSurvey", "post");
				PlayerPrefs.Save();
				SceneManager.LoadScene("_Survey");
			}
			else if (type == "end")
			{
				exit.ExitGameConfirmed();
			}
		}
    }
}
