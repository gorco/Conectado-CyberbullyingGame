using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameAndSetUserInfo : MonoBehaviour {

	public UnityEngine.UI.InputField userName;
	public UnityEngine.UI.Toggle male;
	public UnityEngine.UI.Toggle female;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame()
	{
		if(userName.text == "")
		{

			return;
		}

		if(!male.isOn && !female.isOn)
		{

			return;
		}

		GlobalState.UserName = userName.text;
		GlobalState.MaleSex = male.isOn;
		SceneManager.LoadScene(2);
		
	}
}
