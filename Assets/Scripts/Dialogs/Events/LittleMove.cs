using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMove : EventManager
{
	public Vector2 newPos;
	public Vector2 newSize;

	public string keyEvent;

	private bool done = false;

	/// <summary>
	/// Receive the pick event
	/// </summary>
	/// <param name="ev"></param>
	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "little move" && (keyEvent == null || keyEvent == "" ||
			((String)ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD)).Replace("\"", "") == keyEvent))
		{
			this.ChangePosition();
		}
	}

	public override void Tick()
	{
		//throw new NotImplementedException();
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// Change the GameObject position 
	/// </summary>
	private void ChangePosition()
	{
		if (!done) { 
			this.transform.localPosition = newPos;
			this.transform.localScale = newSize;
			done = true;
		}
	}
}
