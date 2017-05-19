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
		Debug.LogWarning("INIT GLOBAL STATE");

		GlobalState.Day = this.day;
		GlobalState.Hour = this.hour;
		GlobalState.Minute = this.minute;
		GlobalState.Repeated = this.repeated;
		GlobalState.MaleSex = this.maleSex;
		GlobalState.UserName = this.userName;

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
		/*
		this.Day = GlobalState.Day;
		this.Hour = GlobalState.Hour;
		this.Minute = GlobalState.Minute;
		this.Repeated = GlobalState.Repeated;
		this.MaleSex = GlobalState.MaleSex;
		this.UserName = GlobalState.UserName;

		this.MariaFS = GlobalState.MariaFS;
		this.AlisonFS = GlobalState.AlisonFS;
		this.AnaFS = GlobalState.AnaFS;
		this.GuillermoFS = GlobalState.GuillermoFS;
		this.JoseFS = GlobalState.JoseFS;
		this.AlejandroFS = GlobalState.AlejandroFS;

		this.MariaQuest = GlobalState.MariaQuest;
		this.AlisonQuest = GlobalState.AlisonQuest;
		this.AnaQuest = GlobalState.AnaQuest;
		this.GuillermoQuest = GlobalState.GuillermoQuest;
		this.JoseQuest = GlobalState.JoseQuest;
		this.AlejandroQuest = GlobalState.AlejandroQuest;
		*/
	}
	
	// Update is called once per frame
	void Update () {

	}
}
