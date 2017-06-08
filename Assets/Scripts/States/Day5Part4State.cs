using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5Part4State : IState
{
	[SerializeField]
	private bool chargingMobile = false;

	public bool ChargingMobile
	{
		get { return chargingMobile; }
		set { chargingMobile = value; }
	}

	void Start()
	{
		if (GlobalState.SharedPassQuest == 0)
		{
			InitMobileGUI.InitMobileGUIObject(false);
		}
		else
		{
			InitMobileGUI.InitMobileGUIObject(true);
		}
	}
}


