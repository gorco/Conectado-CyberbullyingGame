using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1Part2State : IState
{

	[SerializeField]
	private bool bellSounded = false;

	[SerializeField]
	private bool alisonDialog = false;

	public bool BellSounded
	{
		get { return bellSounded; }
		set { bellSounded = value; }
	}

	public bool AlisonDialog
	{
		get { return alisonDialog; }
		set { alisonDialog = value; }
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

