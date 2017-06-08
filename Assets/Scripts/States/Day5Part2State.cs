using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5Part2State : IState {

	[SerializeField]
	private bool bellSounded = false;

	public bool BellSounded
	{
		get { return bellSounded; }
		set { bellSounded = value; }
	}

	// Use this for initialization
	void Start()
	{
		if (GlobalState.NowIsLaterThan(8, 30))
		{
			bellSounded = true;
		}
		if (GlobalState.SharedPassQuest == 0)
		{
			base.mobile = InitMobileGUI.InitMobileGUIObject(false);
		}
		else
		{
			base.mobile = InitMobileGUI.InitMobileGUIObject(true);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}