using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2Part1State : IState {

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

	private int mariaFS;
	private int anaFS;
	private int joseFS;
	private int alisonFS;
	private int alejandroFS;
	private int guillermoFS;
	private int parentFS;

	// Use this for initialization
	void Start () {

		InitMobileGUI.InitMobileGUIObject();

		if (GlobalState.Repeated)
		{
			GlobalState.MariaFS = mariaFS;
			GlobalState.AnaFS = anaFS;
			GlobalState.JoseFS = joseFS;
			GlobalState.AlisonFS = alisonFS;
			GlobalState.AlejandroFS = alejandroFS;
			GlobalState.GuillermoFS = guillermoFS;
			GlobalState.ParentsFS = parentFS;
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
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
