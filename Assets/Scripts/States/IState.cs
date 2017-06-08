using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IState : MonoBehaviour {

	protected GameObject mobile;

	// Use this for initialization
	void Start() { 
		mobile = InitMobileGUI.InitMobileGUIObject();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnDestroy()
	{
		if(mobile)
			mobile.GetComponentInChildren<MobileChat>().hideMobile(0);
	}
}
