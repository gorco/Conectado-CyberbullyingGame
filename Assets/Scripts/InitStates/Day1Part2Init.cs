using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1Part2Init : MonoBehaviour {

	public Day1Part2State state;

	// Use this for initialization
	void Start () {
		
		GlobalState.UserName = "pepe";
		GlobalState.MaleSex = true;
		
		if(GlobalState.NowIsLaterThan(8, 0))
		{
			state.BellSounded = true;
		}
		InitMobileGUI.InitMobileGUIObject();
		
		GlobalState.AlejandroFS = 50;
		GlobalState.GuillermoFS = 50;
		GlobalState.JoseFS = 50;
		GlobalState.AnaFS = 50;
		GlobalState.AlisonFS = 50;
		GlobalState.MariaFS = 50;
		GlobalState.TeacherFS = 50;
		GlobalState.ParentsFS = 50;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
