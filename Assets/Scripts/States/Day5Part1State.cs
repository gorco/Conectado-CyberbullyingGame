using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5Part1State : IState
{

	[SerializeField]
	private bool bagPicked = false;

	public bool BagPicked
	{
		get { return bagPicked; }
		set { bagPicked = value; }
	}

	[SerializeField]
	private bool chargingMobile = false;

	public bool ChargingMobile
	{
		get { return chargingMobile; }
		set { chargingMobile = value; }
	}

	// Use this for initialization
	void Start()
	{
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
