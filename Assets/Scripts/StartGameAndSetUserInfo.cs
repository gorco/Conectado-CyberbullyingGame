using RAGE.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameAndSetUserInfo : MonoBehaviour {

	public UnityEngine.UI.InputField userName;
	public UnityEngine.UI.InputField nick;
	public UnityEngine.UI.InputField userPass;
	public UnityEngine.UI.Toggle male;
	public UnityEngine.UI.Toggle female;

	public UnityEngine.UI.Text error;

	// Use this for initialization
	void Start () {
		error.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame()
	{
		if(userName.text == "")
		{
			error.text = "Hace falta tu nombre";
			error.enabled = true;
			return;
		}

		if (nick.text == "")
		{
			error.text = "Hace falta un nombre de usuario";
			error.enabled = true;
			return;
		}

		if (userPass.text == "")
		{
			error.text = "Hace falta una contraseña";
			error.enabled = true;
			return;
		}

		if (!male.isOn && !female.isOn)
		{
			error.text = "Hace falta que selecciones tu género";
			error.enabled = true;
			return;
		}

		//Init day
		GlobalState.Day = 0;
		GlobalState.Hour = 7;
		GlobalState.Minute = 0;
		GlobalState.Repeated = false;
		GlobalState.MessagesPending = false;

		//Save data
		GlobalState.UserName = userName.text;
		GlobalState.Nick = nick.text;
		GlobalState.UserPass = userPass.text;
		GlobalState.MaleSex = male.isOn;

		Tracker.T.setVar("gender", male.isOn ? "male" : "female");
		Tracker.T.setVar("pass", userPass.text);
		Tracker.T.accessible.Accessed("StartGame");

		//Friendship
		GlobalState.AlejandroFS = 50;
		GlobalState.GuillermoFS = 50;
		GlobalState.JoseFS = 50;
		GlobalState.AnaFS = 50;
		GlobalState.AlisonFS = 50;
		GlobalState.MariaFS = 50;
		GlobalState.TeacherFS = 50;
		GlobalState.ParentsFS = 50;

		//Quests
		GlobalState.MariaQuest = 0;
		GlobalState.AlisonQuest = 0;
		GlobalState.AnaQuest = 0;
		GlobalState.GuillermoQuest = 0;
		GlobalState.JoseQuest = 0;
		GlobalState.AlejandroQuest = 0;
		GlobalState.GumQuest = 0;
		GlobalState.BoardQuest = 0;
		GlobalState.ParentsMeetingQuest = 0;
		GlobalState.SharedPassQuest = 0;

		//Start game
		SceneManager.LoadScene(2);
		
	}
}
