using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day4Part4State : IState {

	// Use this for initialization
	void Start () {
		if (GlobalState.SharedPassQuest == 0)
		{
			InitMobileGUI.InitMobileGUIObject(false);
		}
		else
		{
			InitMobileGUI.InitMobileGUIObject(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
