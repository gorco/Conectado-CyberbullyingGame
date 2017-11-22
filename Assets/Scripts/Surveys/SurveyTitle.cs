using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyTitle : MonoBehaviour {

	public string[] titles;
	public string[] types;

	// Use this for initialization
	void Start () {
		Text title = this.gameObject.GetComponent<Text>();
		int i = 0;
		title.text = titles[0];
		foreach(string s in types)
		{
			if (s.Equals(PlayerPrefs.GetString("CurrentSurvey")))
			{
				title.text = titles[i];
				break;
			}
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
