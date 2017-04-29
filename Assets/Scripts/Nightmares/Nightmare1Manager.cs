using Isometra;
using Isometra.Sequences;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare1Manager : MonoBehaviour {

	private float time = 3;
	private bool throwFinish = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(CheckSits() == 0 && !throwFinish)
		{
			throwFinish = true;
			this.GetComponent<ObjectsWithDialogsManager>().startDialog("finishNightmare");
		}
	}

	public float HowLong()
	{
		time /= 2f;
		return time;
	}

	private float CheckSits()
	{
		return GetComponentsInChildren<Sit>(false).Length;
	}
}
