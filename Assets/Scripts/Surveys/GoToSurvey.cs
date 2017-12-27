using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSurvey : MonoBehaviour {

	public string type = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GoToSurveyMethod()
	{
		PlayerPrefs.SetString("CurrentSurvey", type);
		PlayerPrefs.Save();
		SceneManager.LoadScene("_Survey");
	}
}
