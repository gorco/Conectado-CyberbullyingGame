using Isometra;
using Isometra.Sequences;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare3Manager : NightmareManager
{
	private Nightmare3State talks;

	public GameObject shadow;
	private float time = 0;
	private bool throwFinish = false;
	private float xShadow;

	// Use this for initialization
	void Start () {
		talks = GetComponent<Nightmare3State>();
		InitMobileGUI.InitMobileGUIObject(false);
	}
	
	// Update is called once per frame
	void Update () {		
		if(talks.Talk >= 5)
		{
			time += Time.deltaTime;
		}

		if (talks.Talk >= 5 && time > 2 && !throwFinish)
		{
			throwFinish = true;
			this.GetComponent<ObjectsWithDialogsManager>().startDialog("finishNightmare");
		}
	}

	public override float HowLong()
	{
		time /= 2f;
		return time;
	}

	private float CheckDialogs()
	{
		return GetComponentsInChildren<ThrowDialog>(false).Length;
	}
}
