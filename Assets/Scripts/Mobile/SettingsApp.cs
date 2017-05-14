using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsApp : MonoBehaviour {

	public GameObject confirmExitPanel;

	// Use this for initialization
	void Start () {
		confirmExitPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ExitButton()
	{
		confirmExitPanel.SetActive(true);
	}

	public void ExitGameConfirmed()
	{
		//Application.Quit();
		if (Application.isWebPlayer == false && Application.isEditor == false){
			System.Diagnostics.Process.GetCurrentProcess().Kill();
		}
	}

	public void ExitGameCanceled()
	{
		confirmExitPanel.SetActive(false);
	}
}
