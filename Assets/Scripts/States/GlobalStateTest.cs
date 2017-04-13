using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateTest : MonoBehaviour {

	public int Day;
	public int Hour;
	public int Minute;
	public bool Repeated;
	public bool MaleSex;
	public string UserName;

	// Frienship
	public int MariaFS;
	public int AlisonFS;
	public int AnaFS;
	public int GuillermoFS;
	public int JoseFS;
	public int AlejandroFS;

	//QuestValue
	public int MariaQuest;
	public int AlisonQuest;
	public int AnaQuest;
	public int GuillermoQuest;
	public int JoseQuest;
	public int AlejandroQuest;

	// Use this for initialization
	void Start () {
		GlobalState.Day = Day;
		GlobalState.Hour = Hour;
		GlobalState.Minute = Minute;
		GlobalState.Repeated = Repeated;
		GlobalState.MaleSex = MaleSex;
		GlobalState.UserName = UserName;

		GlobalState.MariaFS = MariaFS;
		GlobalState.AlisonFS = AlisonFS;
		GlobalState.AnaFS = AnaFS;
		GlobalState.GuillermoFS = GuillermoFS;
		GlobalState.JoseFS = JoseFS;
		GlobalState.AlejandroFS = AlejandroFS;

		GlobalState.MariaQuest = MariaQuest;
		GlobalState.AlisonQuest = AlisonQuest;
		GlobalState.AnaQuest = AnaQuest;
		GlobalState.GuillermoQuest = GuillermoQuest;
		GlobalState.JoseQuest = JoseQuest;
		GlobalState.AlejandroQuest = AlejandroQuest;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
