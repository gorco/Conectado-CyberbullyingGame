using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSelector : MonoBehaviour {

	private float yPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private Vector3 screenPoint;
	private Vector3 offset;

	void OnMouseDown()
	{
		yPosition = gameObject.transform.position.y;
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag()
	{
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		transform.position = new Vector3 (cursorPosition.x, yPosition, cursorPoint.z);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Estoy tocando");
	}

}
