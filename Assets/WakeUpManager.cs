using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpManager : MonoBehaviour {

	public Mobile mobileObject;
	public Eyelids eyelidsObject;

	public UnityEngine.UI.Text introText;

	// Use this for initialization
	void Start () {
		introText.enabled = CalendarTime.Day == 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
