using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile : MonoBehaviour {

	public AnimationCurve curve;

	private Vector2 hidePosition;
	public Vector2 showPosition;

	public TextMesh hour;
	public TextMesh alarm;
	public TextMesh day;

	private Vector2 goal;
	private Vector2 start;

	private float delta;

	private bool animate = false;
	private float animationSeconds;

	private bool sound = true;
	private bool delayed;

	// Use this for initialization
	void Start()
	{
		hidePosition = this.GetComponent<Transform>().position;
	}

	// Update is called once per frame
	void Update()
	{
		if (animate)
		{
			this.GetComponent<Transform>().position = Vector3.Lerp(start, goal, curve.Evaluate((delta) / animationSeconds));
			delta += Time.deltaTime;
			if (delta >= animationSeconds)
				animate = false;
		}

	}

	public void takeMobile(float seconds)
	{
		goal = showPosition;
		start = hidePosition;
		animationSeconds = seconds;
		delta = 0;
		animate = true;
	}

	public void hideMobile(float seconds)
	{

		goal = hidePosition;
		start = showPosition;
		animationSeconds = seconds;
		delta = 0;
		animate = true;
	}

	internal void stopAlarm(bool delayed)
	{
		this.delayed = delayed;
		sound = false;
	}

	public bool isSounding()
	{
		return sound;
	}

	public bool isAlarmDelayed()
	{
		return delayed;
	}

	public void setHour(int hour, int minute)
	{
		this.hour.text = "";
		if (hour < 10)
		{
			this.hour.text = "0";
		}
		this.hour.text += hour + ":";
		if(minute < 10)
		{
			this.hour.text += "0";
		}
		this.hour.text += minute;
	}

	public void updateHour()
	{
		setHour(CalendarTime.Hour, CalendarTime.Minute);
	}

}
