using Isometra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Isometra.Sequences;
using System.IO;

public class MobileMessage : EventManager {

	public MobileChat mobile;
	private JSONObject jsonObj;
	public TextAsset jsonFile;

	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "mobile message")
		{
			string chatName = ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD).ToString().Replace("\"", "");
			string message = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD).ToString().Replace("\"", "");
			string from = ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD).ToString().Replace("\"", "");

			float time = (float)ev.getParameter(SequenceGenerator.EVENT_TIME_FIELD);
			object otherObj = ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD);
			string other = otherObj != null ? otherObj.ToString().Replace("\"", "") : "";

			StartCoroutine(ExecuteAfterTime(time, message, from, chatName, other));
		}
	}

	public override void Tick()
	{
	}

	// Use this for initialization
	void Start () {
		if (jsonFile != null)
		{
			string fileContents = jsonFile.text;
			jsonObj = JSONObject.Create(fileContents);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void GenerateAndSetSequence(string jsonKey, string chatName)
	{
		Sequence seq = SequenceGenerator.createSimplyDialog(jsonKey, jsonObj);
		mobile.AddChatSequence(chatName, seq);
	}

	private void RemoveSequence(string chatName)
	{
		mobile.ClearChatSequence(chatName);
	}

	IEnumerator ExecuteAfterTime(float time, string message, string from, string chatName, string other)
	{
		yield return new WaitForSeconds(time);

		if (jsonObj)
		{
			if (other == "empty")
			{
				RemoveSequence(chatName);
			}
			else if (other != "")
			{
				GenerateAndSetSequence(other, chatName);
			}
		}
		mobile.AddMessage(message, from, chatName);
	}
}
