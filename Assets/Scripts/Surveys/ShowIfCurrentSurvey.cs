using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIfCurrentSurvey : MonoBehaviour {

	public GameObject[] objectsToShow;
	public string type = "";
	public string surveyName = "";

	// Use this for initialization
	void Start () {
		Debug.Log(surveyName + " - " + PlayerPrefs.HasKey(surveyName) + " - " + PlayerPrefs.GetString(surveyName)  + " - " + PlayerPrefs.GetString("CurrentSurvey") + " - " + type);
		if (surveyName == "" || (PlayerPrefs.HasKey(surveyName) && PlayerPrefs.GetString(surveyName) != ""))
		{
			if (PlayerPrefs.GetString("CurrentSurvey") != "pre" && PlayerPrefs.HasKey("username"))
			{
				foreach (GameObject o in objectsToShow)
				{
					o.gameObject.SetActive(true);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
