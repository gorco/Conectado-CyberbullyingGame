using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateTest : MonoBehaviour {

	public int day;
	public int hour;
	public int minute;
	public bool repeated;
	public bool maleSex;
	public string userName;
	public string userPass;

	// Frienship
	public int mariaFS;
	public int alisonFS;
	public int anaFS;
	public int guillermoFS;
	public int joseFS;
	public int alejandroFS;

	//QuestValue
	public int mariaQuest;
	public int alisonQuest;
	public int anaQuest;
	public int guillermoQuest;
	public int joseQuest;
	public int alejandroQuest;
	public int gumQuest;
	public int boardQuest;
	public int parentsMeetingQuest;
	public int sharedPassQuest;

	// Use this for initialization
	void Awake () {

        //DEBUG ONLY
        //LanguageSelector.instance.GetCurrentLanguage() = "ES"

		GlobalState.Repeated = this.repeated;
		
		Debug.LogWarning("INIT GLOBAL STATE");

		GlobalState.Day = this.day;
		GlobalState.Hour = this.hour;
		GlobalState.Minute = this.minute;
		GlobalState.Repeated = this.repeated;
		GlobalState.MaleSex = this.maleSex;
		GlobalState.UserName = this.userName;
		GlobalState.UserPass = this.userPass;

		GlobalState.MariaFS = this.mariaFS;
		GlobalState.AlisonFS = this.alisonFS;
		GlobalState.AnaFS = this.anaFS;
		GlobalState.GuillermoFS = this.guillermoFS;
		GlobalState.JoseFS = this.joseFS;
		GlobalState.AlejandroFS = this.alejandroFS;

		GlobalState.MariaQuest = this.mariaQuest;
		GlobalState.AlisonQuest = this.alisonQuest;
		GlobalState.AnaQuest = this.anaQuest;
		GlobalState.GuillermoQuest = this.guillermoQuest;
		GlobalState.JoseQuest = this.joseQuest;
		GlobalState.AlejandroQuest = this.alejandroQuest;

		GlobalState.GumQuest = this.gumQuest;
		GlobalState.BoardQuest = this.boardQuest;
		GlobalState.ParentsMeetingQuest = this.parentsMeetingQuest; 
		GlobalState.SharedPassQuest = this.sharedPassQuest;

		Debug.LogWarning("FIN INIT GLOBAL STATE");

		



	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.day = GlobalState.Day;
		this.hour = GlobalState.Hour;
		this.minute = GlobalState.Minute;
		this.repeated = GlobalState.Repeated;
		this.maleSex = GlobalState.MaleSex;
		this.userName = GlobalState.UserName;

		this.mariaFS = GlobalState.MariaFS;
		this.alisonFS = GlobalState.AlisonFS;
		this.anaFS = GlobalState.AnaFS;
		this.guillermoFS = GlobalState.GuillermoFS;
		this.joseFS = GlobalState.JoseFS;
		this.alejandroFS = GlobalState.AlejandroFS;

		this.mariaQuest = GlobalState.MariaQuest;
		this.alisonQuest = GlobalState.AlisonQuest;
		this.anaQuest = GlobalState.AnaQuest;
		this.guillermoQuest = GlobalState.GuillermoQuest;
		this.joseQuest = GlobalState.JoseQuest;
		this.alejandroQuest = GlobalState.AlejandroQuest;
	}
}
