using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1Part3State : IState
{

	[SerializeField]
	private bool alisonDialog = false;
	[SerializeField]
	private bool earingFound = false;

	public bool AlisonDialog
	{
		get { return alisonDialog; }
		set { alisonDialog = value; }
	}

	public bool EaringFound
	{
		get { return earingFound; }
		set { earingFound = value; }
	}

	// Use this for initialization
	void Start()
	{
		InitMobileGUI.InitMobileGUIObject();

		
		GlobalState.UserName = "pepe";
		GlobalState.MaleSex = true;
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
	void Update()
	{

	}
}


