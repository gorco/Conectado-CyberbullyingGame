using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameAndSetUserInfo : MonoBehaviour {

	public UnityEngine.UI.InputField userName;
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
			error.text = "Hace falta un nombre de jugador";
			error.enabled = true;
			return;
		}

		if(!male.isOn && !female.isOn)
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
		GlobalState.MaleSex = male.isOn;

		//Friendship
		GlobalState.AlejandroFS = 50;
		GlobalState.GuillermoFS = 50;
		GlobalState.JoseFS = 50;
		GlobalState.AnaFS = 50;
		GlobalState.AlisonFS = 50;
		GlobalState.MariaFS = 50;
		GlobalState.TeacherFS = 50;
		GlobalState.ParentsFS = 50;

		//Start game
		SceneManager.LoadScene(2);
		
	}
}
