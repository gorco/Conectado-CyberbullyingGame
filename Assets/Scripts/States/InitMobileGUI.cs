using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InitMobileGUI {

	public static void InitMobileGUIObject(bool active = true)
	{
		GameObject mobileCanvas = GameObject.Find("MobileCanvas");
		UIButtonMobile button = mobileCanvas.GetComponentInChildren<UIButtonMobile>(true);
		button.gameObject.SetActive(active);
	}
}
