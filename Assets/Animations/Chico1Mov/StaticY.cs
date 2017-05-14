using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticY : MonoBehaviour {

	public float y;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector2(transform.localPosition.x, y);
	}
}
