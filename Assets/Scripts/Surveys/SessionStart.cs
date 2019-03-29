using RAGE.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionStart : MonoBehaviour {

	Net connection;
	public Text sessionKey, response;
	private String trackingCode = "";
	public GameObject startButton;
	public GameObject Input;
	public Text SessionLabel;
	public GameObject TokenObj;

	private bool check = true;

	// Use this for initialization
	void Start () {
		connection = new Net(this);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ValidateSession()
	{
		string trackings = "";
		if (PlayerPrefs.HasKey("ActivitiesTracking"))
			trackings = PlayerPrefs.GetString("ActivitiesTracking");
		if (trackings == "" || trackings == null)
		{
			NextStep();
		}
		else
		{
			Debug.Log(trackings);
			trackings = trackings.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace(" ", "");
			string[] result = trackings.Substring(1, trackings.Length - 2).Split(',');
			foreach (string tr in result)
			{
				if (sessionKey.text.ToLower() == tr.Substring(tr.Length - 6, 6))
				{
					trackingCode = tr;
					break;
				}
			}
			if (trackingCode != "" || sessionKey.text == "none")
			{
				PlayerPrefs.SetString("trackingCode", trackingCode);
				NextStep();
			}
			else
			{
				response.text = "Clave de Sesión Invalida";
			}
		}
		
	}

	public void NextStep()
	{
		response.text = "";
		startButton.SetActive(true);
		Input.SetActive(false);
		SessionLabel.text = trackingCode;
		TokenObj.SetActive(true);
	}
}


