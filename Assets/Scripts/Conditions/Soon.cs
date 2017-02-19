using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soon : MonoBehaviour {

	public bool hideSoon = true;
	public int hourLate;
	 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(CalendarTime.Hour < hourLate == hideSoon)
		{
			this.gameObject.SetActive(false);
		}
	}
}
