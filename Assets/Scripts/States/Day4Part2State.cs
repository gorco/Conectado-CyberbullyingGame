using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day4Part2State : IState
{

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
		base.mobile = InitMobileGUI.InitMobileGUIObject();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
