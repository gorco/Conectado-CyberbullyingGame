using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using Simva;
using UnityFx.Async.Promises;

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
        if (!SimvaManager.Instance.IsActive || ((Simva.SimvaPlugin)SimvaManager.Instance.Bridge).WantsToQuit())
        {
            CloseAll();
        }
    }

	public void ReturnHomeAfterEnd()
    {
        if (!SimvaManager.Instance.IsActive || ((Simva.SimvaPlugin)SimvaManager.Instance.Bridge).WantsToQuit())
        {
            SceneManager.LoadScene("Title");
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

