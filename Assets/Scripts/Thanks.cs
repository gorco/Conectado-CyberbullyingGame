using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thanks : MonoBehaviour {

	public Vector2 start;
	public Vector2 end;

	public float speed;

	public GameObject homeButton;
	public GameObject exitButton;

	// Use this for initialization
	void Start () {
		transform.localPosition = start;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = Vector2.MoveTowards(transform.localPosition, end, Time.deltaTime * speed);
	}
}
