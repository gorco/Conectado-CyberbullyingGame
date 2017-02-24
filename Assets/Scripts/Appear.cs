using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour {

	public bool appearSoon;
	public int edgeHour;
	public int edgeMinutes;

	// Use this for initialization
	void Start()
	{
		bool late = CalendarTime.isLate(edgeHour, edgeMinutes);
		if(appearSoon && !late)
		{
			this.gameObject.SetActive(false);
		} else if (!appearSoon && late)
		{
			this.gameObject.SetActive(false);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
