using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day4Part3State : IState {

	public Camera sceneCamera;
	public float minScroll = 0;
	public float maxScroll;
	public float x;
	public float y;

	[SerializeField]
	private int mariaJoke = 0; // [0 - no joke] [1 - jodePending] [2 - jokeSuccess]

	public int MariaJoke
	{
		get { return mariaJoke; }
		set { mariaJoke = value; }
	}

	[SerializeField]
	private bool timeEnough = false; 

	public bool TimeEnough
	{
		get { return timeEnough; }
		set { timeEnough = value; }
	}

	// Use this for initialization
	void Start () {
		if(GlobalState.SharedPassQuest != 1)
		{
			base.mobile = InitMobileGUI.InitMobileGUIObject(false);
		} else
		{
			base.mobile = InitMobileGUI.InitMobileGUIObject(true);
			CameraScroll cam = sceneCamera.GetComponent<CameraScroll>();
			cam.scroll_min = minScroll;
			cam.scroll_max = maxScroll;
			sceneCamera.transform.localPosition = new Vector3(x, y, sceneCamera.transform.localPosition.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
