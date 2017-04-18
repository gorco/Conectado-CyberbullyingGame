using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1Part2Init : MonoBehaviour {

	public Day1Part2State state;

	// Use this for initialization
	void Start () {
		//GlobalState.UserName = "pepe";
		//GlobalState.MaleSex = true;
		if(GlobalState.NowIsLaterThan(8, 0))
		{
			state.BellSounded = true;
		}		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
