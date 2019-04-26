using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlarmSelector : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

	public GameObject delayButton;
	public GameObject turnOffButton;

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


	private void OnCollisionEnter2D(Collision2D collision)
	{
		
		
	}

	public void OnPointerClick(PointerEventData eventData)
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		xStartPosition = transform.position.x;
		yPosition = transform.position.y;

		min = delayButton.transform.position.x;
		max = turnOffButton.transform.position.x;

		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		//delay alarm
		if (this.transform.localPosition.x + this.GetComponent<Renderer>().bounds.size.x > turnOffButton.transform.localPosition.x)
		{
			TurnOff();
		}
		//turn off alarm
		else if (this.transform.localPosition.x < delayButton.transform.localPosition.x + delayButton.GetComponent<Renderer>().bounds.size.x)
		{
			Delay();
		}
		this.transform.position = new Vector3(xStartPosition, transform.position.y, transform.position.z);
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		transform.position = new Vector3(Mathf.Max(Mathf.Min(cursorPosition.x, max), min), yPosition, cursorPoint.z);
	}

	public void Delay()
	{
		this.GetComponentInParent<Mobile>().stopAlarm(false);
	}

	public void TurnOff()
	{
		this.GetComponentInParent<Mobile>().stopAlarm(true);
	}
}
