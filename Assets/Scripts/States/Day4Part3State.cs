using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day4Part3State : IState {

	public Camera sceneCamera;
	public float minScroll = 0;
	public float maxScroll;
	public float x;
	public float y;

	// Use this for initialization
	void Start () {
		if(GlobalState.SharedPassQuest == 0)
		{
			InitMobileGUI.InitMobileGUIObject(false);
		} else
		{
			InitMobileGUI.InitMobileGUIObject(true);
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
