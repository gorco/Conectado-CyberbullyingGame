using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5Part3State : IState
{
	[SerializeField]
	private bool alex = false;

	public bool Alex
	{
		get { return alex; }
		set { alex = value; }
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