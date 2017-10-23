using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day4Part1State : IState
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

	private static int mariaFS;
	private static int anaFS;
	private static int joseFS;
	private static int alisonFS;
	private static int alejandroFS;
	private static int guillermoFS;
	private static int parentFS;

	private static int parentsMeetingQuest;
	void Start()
	{
		if (GlobalState.Repeated)
		{
			GlobalState.MariaFS = mariaFS;
			GlobalState.AnaFS = anaFS;
			GlobalState.JoseFS = joseFS;
			GlobalState.AlisonFS = alisonFS;
			GlobalState.AlejandroFS = alejandroFS;
			GlobalState.GuillermoFS = guillermoFS;
			GlobalState.ParentsFS = parentFS;

			GlobalState.SharedPassQuest = 0;
			GlobalState.ParentsMeetingQuest = parentsMeetingQuest;
		}
		else
		{
			mariaFS = GlobalState.MariaFS;
			anaFS = GlobalState.AnaFS;
			joseFS = GlobalState.JoseFS;
			alisonFS = GlobalState.AlisonFS;
			alejandroFS = GlobalState.AlejandroFS;
			guillermoFS = GlobalState.GuillermoFS;
			parentFS = GlobalState.ParentsFS;

			parentsMeetingQuest = GlobalState.ParentsMeetingQuest;
		}

		base.mobile = InitMobileGUI.InitMobileGUIObject();
	}
}
