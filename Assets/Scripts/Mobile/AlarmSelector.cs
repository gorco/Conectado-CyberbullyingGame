using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSelector : MonoBehaviour {

	public GameObject postPostpone;
	public GameObject turnOff;

	private float yPosition;
	private float xStartPosition;

	private float max;
	private float min;

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
		xStartPosition = this.transform.position.x;
		yPosition = gameObject.transform.position.y;

		max = postPostpone.transform.position.x;
		min = turnOff.transform.position.x;
		
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag()
	{
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		transform.position = new Vector3 (Mathf.Max(Mathf.Min(cursorPosition.x, max), min), yPosition, cursorPoint.z);
	}

	private void OnMouseUp()
	{
		//postpone
		if(this.transform.localPosition.x + this.GetComponent<Renderer>().bounds.size.x > postPostpone.transform.localPosition.x)
		{
			Debug.Log("Se me va a hacer tarde");
		} else if (this.transform.localPosition.x < turnOff.transform.localPosition.x + turnOff.GetComponent<Renderer>().bounds.size.x)
		{
			Debug.Log("Llegaré bien de tiempo");
		}
		this.transform.position = new Vector3(xStartPosition, transform.position.y, transform.position.z);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		
		
	}

}
