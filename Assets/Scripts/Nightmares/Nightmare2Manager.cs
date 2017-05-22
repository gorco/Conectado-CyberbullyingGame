using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare2Manager : NightmareManager
{

	private float time = 3;
	private bool throwFinish = false;
	public GameObject portal;
	public GameObject portalCollider;
	private bool showPortal;

	// Use this for initialization
	void Start()
	{
		if (GlobalState.Repeated)
		{
			showPortal = false;
			GlobalState.Repeated = false;
		} else
		{
			showPortal = true;
		}
		InitMobileGUI.InitMobileGUIObject(false);
	}
	
	// Update is called once per frame
	void Update()
	{
		if (CheckSits() == 0 && !throwFinish)
		{
			if (showPortal)
			{
				portal.SetActive(true);
				portalCollider.SetActive(true);
			} else
			{
				this.GetComponent<ObjectsWithDialogsManager>().startDialog("finishNightmare");
			}
			throwFinish = true;
		}
	}

	private float CheckSits()
	{
		return GetComponentsInChildren<Sit>(false).Length;
	}

	public override float HowLong()
	{
		time /= 2f;
		return time;
	}
}

