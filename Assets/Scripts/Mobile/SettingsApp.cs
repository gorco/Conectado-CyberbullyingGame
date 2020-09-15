using AssetPackage;
using RAGE.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RAGE.Analytics;
using UnityEngine.UI;

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
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
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
		if (!Simva.SimvaExtension.Instance || Simva.SimvaExtension.Instance.OnGameFinished())
		{
			if (Application.platform != RuntimePlatform.WebGLPlayer && Application.isEditor == false)
			{
				Application.Quit();
				System.Diagnostics.Process.GetCurrentProcess().Kill();
			}
		}	
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

