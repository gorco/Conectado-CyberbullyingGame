using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIfSurveyExist : MonoBehaviour {

	public GameObject[] objectsToShow;
	public string surveyName;

	// Use this for initialization
	void Start()
	{
		if (PlayerPrefs.HasKey(surveyName))
		{
			foreach (GameObject o in objectsToShow)
			{
				o.SetActive(true);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
