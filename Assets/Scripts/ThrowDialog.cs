using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDialog : MonoBehaviour {

	public string fieldName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnMouseUp()
	{
		Debug.Log("OnMouseUp");
		GetComponentInParent<ObjectsWithDialogsManager>().startDialog(this.name);
	}
}
