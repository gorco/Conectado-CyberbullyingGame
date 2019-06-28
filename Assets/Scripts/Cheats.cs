using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cheats : MonoBehaviour {

	public InputField day;
	public InputField mariaFS;
	public InputField parentsFS;
	public InputField user;
	public InputField genre;
	public InputField gum;
	public InputField parentsMeeting;
	public InputField password;

	public InputField pass;
	public InputField namePlayer;
	public InputField nick;

    public InputField laguage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartDay()
	{
		GlobalState.UserName = user.text;
		GlobalState.MaleSex = genre.text == "0" ? true : false;
		GlobalState.Day = genre.text == "" ? 0 : Int32.Parse(day.text) - 1;
		GlobalState.MariaFS = genre.text == "" ? 50 : Int32.Parse(mariaFS.text);
		GlobalState.ParentsFS = genre.text == "" ? 50 : Int32.Parse(parentsFS.text);
		GlobalState.GumQuest = genre.text == "" ? 0 : Int32.Parse(gum.text);
		GlobalState.ParentsMeetingQuest = genre.text == "" ? 0 : Int32.Parse(parentsMeeting.text);
		GlobalState.SharedPassQuest = genre.text == "" ? 0 : Int32.Parse(password.text);

		GlobalState.UserName = namePlayer.text;
		GlobalState.Nick = nick.text;
		GlobalState.UserPass = pass.text;

        LanguageSelector.instance.SetLanguage(laguage.text);

		SceneManager.LoadScene(2);
	}
}
