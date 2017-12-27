using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day3Part2State : IState {

	[SerializeField]
	private bool bellSounded = false;

	public bool BellSounded
	{
		get { return bellSounded; }
		set { bellSounded = value; }
	}

	[SerializeField]
	private bool alisonBoard = false;

	public bool AlisonBoard
	{
		get { return alisonBoard; }
		set { alisonBoard = value; }
	}

	// Use this for initialization
	void Start()
	{
		if (GlobalState.NowIsLaterThan(8, 30))
		{
			bellSounded = true;
			alisonBoard = true;
		}
		base.mobile = InitMobileGUI.InitMobileGUIObject();
	}

	// Update is called once per frame
	void Update()
	{

	}
}