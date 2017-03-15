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

		GlobalState.UserName = userName.text;
		GlobalState.MaleSex = male.isOn;
		SceneManager.LoadScene(2);
		
	}
}
