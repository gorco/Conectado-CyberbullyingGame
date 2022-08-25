using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Xasu.HighLevel;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

public class LimeSurveyValidator : MonoBehaviour {

    public Text token, response;
	public string type;
	public  NameSaver nameSaver;

	private static int nextScene = 2;
	private static int initScene = 1;

    
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
            var url = PlayerPrefs.GetString("LimesurveyHost") + "surveys/validate?survey=" 
                + PlayerPrefs.GetString("LimesurveyPre") + ((token.Length > 0) ? "&token=" + token : "");

            var req = UnityWebRequest.Get(url);
            req.SendWebRequest().completed += (a) => OnValidateResponse(a, token);
		}
    }

    private void OnValidateResponse(AsyncOperation obj, string token)
    {
        var req = (UnityWebRequestAsyncOperation)obj;
        var uwr = req.webRequest;
        if (uwr.isHttpError)
        {
            var result = JObject.Parse(uwr.downloadHandler.text);
            if (result != null && result["message"] != null)
                response.text = result.Value<string>("message");
            else
                response.text = uwr.downloadHandler.text != "" ? uwr.downloadHandler.text : "Can't Connect";
        }
        else if (uwr.isNetworkError)
        {
            response.text = uwr.error;
        }
        else
        {
            nameSaver.SaveName();
            PlayerPrefs.SetString("name", token);
            PlayerPrefs.SetString("LimesurveyToken", token);
            PlayerPrefs.Save();
            if (PlayerPrefs.HasKey("LimesurveyPre"))
            {
                SceneManager.LoadScene("_Survey");
            }
            else
                SceneManager.LoadScene(initScene);
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
        var url = PlayerPrefs.GetString("LimesurveyHost") + "surveys/completed?survey=" 
            + survey + ((token.Length > 0) ? "&token=" + token : "");

        var req = UnityWebRequest.Get(url);
        req.SendWebRequest().completed += (a) => OnCompleteResponse(a, this.gameObject.GetComponent<SettingsApp>());
		Debug.Log(PlayerPrefs.GetString("LimesurveyHost") + "surveys/completed?survey=" + survey + ((token.Length > 0) ? "&token=" + token : ""));
		
	}

    private void OnCompleteResponse(AsyncOperation obj, SettingsApp settingsApp)
    {
        var req = (UnityWebRequestAsyncOperation)obj;
        var uwr = req.webRequest;
        if (uwr.isHttpError)
        {
            var result = JObject.Parse(uwr.downloadHandler.text);
            response.text = result.Value<string>("message");
        }
        else if (uwr.isNetworkError)
        {
            response.text = uwr.error;
        }
        else
        {
            string type = "pre";
            response.text = "";
            if (PlayerPrefs.HasKey("CurrentSurvey"))
                type = PlayerPrefs.GetString("CurrentSurvey");
            CompletableTracker.Instance.Completed(type + "Survey");

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
                settingsApp.ExitGameConfirmed();
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
                settingsApp.ExitGameConfirmed();
            }
        }
    }
}
