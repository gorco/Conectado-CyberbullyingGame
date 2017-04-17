using Isometra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MobileMessage : EventManager {

	public MobileChat mobile;

	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "mobile message")
		{
			string chatName = ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD).ToString().Replace("\"", "");
			string message = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD).ToString().Replace("\"", "");
			string from = ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD).ToString().Replace("\"", "");
			mobile.AddMessage(chatName, from, chatName);
		}
	}

	public override void Tick()
	{
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
