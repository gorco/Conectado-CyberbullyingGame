﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SettingsApp : MonoBehaviour {

	public GameObject confirmExitPanel;
	public bool forceExit = false;
	public UnityEngine.UI.Text infoPanel;

	public GameObject tracesSentInfo;
	public Text tracesSentMsg;
	public Toggle TeaToggle;
	public Toggle PostToggle;
	public Toggle PreToggle;
	public GameObject CloseAllButton;
	public Text token;

	public bool tracesSent = false;

	public string dataFile;

	// Use this for initialization
	void Start () {
		dataFile = Application.persistentDataPath;
		if (tracesSentInfo)
		{
			tracesSentInfo.SetActive(false);
		}
		tracesSent = false;

		if (!dataFile.EndsWith("/"))
		{
			dataFile += "/";
		}
		dataFile += "/tracesRaw.csv";

		if (confirmExitPanel)
		{
			confirmExitPanel.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && tracesSentInfo.activeInHierarchy)
		{
			CloseAllButton.SetActive(true);
		}
	}

	public void ExitButton()
	{
		if (confirmExitPanel)
		{
			confirmExitPanel.SetActive(true);
		} else
		{
			ExitGameConfirmed();
		}
	}

	public void ExitGameConfirmed()
	{
		//Tracker.T.RequestFlush();
		//Application.Quit();
		if (PlayerPrefs.HasKey("LimesurveyHost") && PlayerPrefs.GetString("LimesurveyHost") != "" && PlayerPrefs.GetString("LimesurveyToken") != "ADMIN" && PlayerPrefs.HasKey("LimesurveyPost"))
		{
			try
			{
				string path = Application.persistentDataPath;
				if (!path.EndsWith("/"))
				{
					path += "/";
				}
				Dictionary<string, string> headers = new Dictionary<string, string>();


				WWWForm data = new WWWForm();
				data.AddField("token", PlayerPrefs.GetString("LimesurveyToken"));
				data.AddBinaryData("traces", System.Text.Encoding.UTF8.GetBytes(System.IO.File.ReadAllText(dataFile)));

				data.headers.Remove ("Content-Type");// = "multipart/form-data";
				System.IO.File.AppendAllText(path + PlayerPrefs.GetString("LimesurveyToken") + ".csv", System.IO.File.ReadAllText(dataFile));

                UnityWebRequest net = UnityWebRequest.Post(PlayerPrefs.GetString("LimesurveyHost") + "classes/collector", data);

                var asyncHandler = net.SendWebRequest();
                asyncHandler.completed += SavedTracesEvent;

				/*
				if (PlayerPrefs.GetString("CurrentSurvey").Equals("post")) {
					PlayerPrefs.SetString("CurrentSurvey", "end");
				}*/
			}
			catch (Exception e)
			{
				Invoke("ChangeLevel", 0);
				Debug.LogError(e);
			}
		}
		else
		{
			if (Application.platform != RuntimePlatform.WebGLPlayer && Application.isEditor == false)
			{
				Application.Quit();
				System.Diagnostics.Process.GetCurrentProcess().Kill();
			}
		}	
	}

    private void SavedTracesEvent(AsyncOperation obj)
    {
        var uwrAsyncOp = (UnityWebRequestAsyncOperation)obj;
        var unityWebRequest = uwrAsyncOp.webRequest;

        if(unityWebRequest.isHttpError)
        {
            Error(unityWebRequest.downloadHandler.text);
        }
        else if (unityWebRequest.isNetworkError)
        {
            Error(unityWebRequest.error);
        }
        else
        {
            Result(unityWebRequest.downloadHandler.text);
        }
    }

    public void Result(string data)
    {
        tracesSent = true;
        ChangeLevel();
    }

    public void Error(string error)
    {
        if (this.infoPanel)
        {
            infoPanel.text = error;
        }
        Debug.Log(error);
    }

    void ChangeLevel()
	{
		if ((PreToggle || PostToggle || TeaToggle) && (PlayerPrefs.GetString("CurrentSurvey").Equals("end") || forceExit))
		{
			if (tracesSentInfo)
			{
				if (tracesSent == false)
				{
					tracesSentMsg.text = "Los datos no se han podido enviar. Avise al encargado del experimento antes de cerrar el juego.";
					tracesSentMsg.color = Color.red;
				}
				else
				{
					tracesSentMsg.text = "Los datos se han mandado correctamente. Puede cerrar el juego.";
					tracesSentMsg.color = Color.white;
				}
				token.text = "Información de "+PlayerPrefs.GetString("username");
				PreToggle.isOn = (PlayerPrefs.HasKey("PreTestEnd") && PlayerPrefs.GetInt("PreTestEnd") == 1);
				PostToggle.isOn = (PlayerPrefs.HasKey("PostTestEnd") && PlayerPrefs.GetInt("PostTestEnd") == 1);
				TeaToggle.isOn = (PlayerPrefs.HasKey("TeaTestEnd") && PlayerPrefs.GetInt("TeaTestEnd") == 1);
				tracesSentInfo.SetActive(true);
			}
			else
			{
                

                if (Application.platform != RuntimePlatform.WebGLPlayer && Application.isEditor == false)
				{
					CloseAll();
				}
			}

			
		} else if (PlayerPrefs.GetString("CurrentSurvey").Equals("tea") || PlayerPrefs.GetString("CurrentSurvey").Equals("post"))
		{
			SceneManager.LoadScene("_Survey");
		} else 
		{
			SceneManager.LoadScene(0);
		}
	}

	public void ExitGameCanceled()
	{
		confirmExitPanel.SetActive(false);
	}

	public void CloseAll()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer && Application.isEditor == false)
		{
			Application.Quit();
			System.Diagnostics.Process.GetCurrentProcess().Kill();
		}
	}
}

