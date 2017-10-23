using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IState : MonoBehaviour {

	protected GameObject mobile;

	// Use this for initialization
	void Start() { 
		mobile = InitMobileGUI.InitMobileGUIObject();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Awake()
	{
		PlayerPrefs.SetInt("Day", GlobalState.Day);
		PlayerPrefs.SetInt("Hour", GlobalState.Hour);
		PlayerPrefs.SetInt("Minute", GlobalState.Minute);

		PlayerPrefs.SetString("Repeated", GlobalState.Repeated.ToString());
		PlayerPrefs.SetString("NotRepeatedDays", GlobalState.NotRepeatedDays.ToString());
		PlayerPrefs.SetString("MaleSex", GlobalState.MaleSex.ToString());
		PlayerPrefs.SetString("MessagesPending", GlobalState.MessagesPending.ToString());

		PlayerPrefs.SetString("UserName", GlobalState.UserName);
		PlayerPrefs.SetString("Nick", GlobalState.Nick);
		PlayerPrefs.SetString("UserPass", GlobalState.UserPass);

		PlayerPrefs.SetInt("MariaFS", GlobalState.MariaFS);
		PlayerPrefs.SetInt("AlisonFS", GlobalState.AlisonFS);
		PlayerPrefs.SetInt("AnaFS", GlobalState.AnaFS);
		PlayerPrefs.SetInt("GuillermoFS", GlobalState.GuillermoFS);
		PlayerPrefs.SetInt("JoseFS", GlobalState.JoseFS);
		PlayerPrefs.SetInt("AlejandroFS", GlobalState.AlejandroFS);
		PlayerPrefs.SetInt("ParentsFS", GlobalState.ParentsFS);
		PlayerPrefs.SetInt("TeacherFS", GlobalState.TeacherFS);
		PlayerPrefs.SetInt("MariaQuest", GlobalState.MariaQuest);
		PlayerPrefs.SetInt("AlisonQuest", GlobalState.AlisonQuest);
		PlayerPrefs.SetInt("AnaQuest", GlobalState.AnaQuest);
		PlayerPrefs.SetInt("JoseQuest", GlobalState.JoseQuest);
		PlayerPrefs.SetInt("GuillermoQuest", GlobalState.GuillermoQuest);
		PlayerPrefs.SetInt("AlejandroQuest", GlobalState.AlejandroQuest);
		PlayerPrefs.SetInt("GumQuest", GlobalState.GumQuest);
		PlayerPrefs.SetInt("BoardQuest", GlobalState.BoardQuest);
		PlayerPrefs.SetInt("ParentsMeetingQuest", GlobalState.ParentsMeetingQuest);
		PlayerPrefs.SetInt("SharedPassQuest", GlobalState.SharedPassQuest);
		PlayerPrefs.SetInt("Final", GlobalState.Final);

		PlayerPrefs.SetInt("Scene", SceneManager.GetActiveScene().buildIndex);

		PlayerPrefs.Save();
	}

	private void OnDestroy()
	{
		if(mobile)
			mobile.GetComponentInChildren<MobileChat>().hideMobile(0);
	}
}
