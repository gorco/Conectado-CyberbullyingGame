using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomState : MonoBehaviour {

	[SerializeField]
	private bool bagPicked;

	public bool BagPicked
	{
		get { return bagPicked; }
		set { bagPicked = value; }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
