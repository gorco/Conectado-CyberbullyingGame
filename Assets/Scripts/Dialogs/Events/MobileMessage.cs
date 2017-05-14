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
			object otherObj = ev.getParameter(SequenceGenerator.EVENT_OTHER_FIELD);
			string other = otherObj != null ? otherObj.ToString().Replace("\"", "") : "";

			if(other == "save")
			{
				mobile.SaveMobileState();
				return;
			}

			if (other == "load")
			{
				mobile.LoadMobileState();
				return;
			}

			string chatName = ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD).ToString().Replace("\"", "");
			string message = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD).ToString().Replace("\"", "");
			string from = ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD).ToString().Replace("\"", "");

			float time = (float)ev.getParameter(SequenceGenerator.EVENT_TIME_FIELD);
			

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
		try
		{
			Sequence seq = SequenceGenerator.createSimplyDialog("default", jsonObj);
			mobile.AddChatSequence("default", seq);
		}
		catch (Exception e)
		{
			Debug.LogError("Error in " + jsonFile.name + " file. The error is: " + e.Message);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void GenerateAndSetSequence(string jsonKey, string chatName)
	{
		try
		{
			Sequence seq = SequenceGenerator.createSimplyDialog(jsonKey, jsonObj);
			mobile.AddChatSequence(chatName, seq);
		} catch (Exception e)
		{
			Debug.LogError("Error in " + jsonFile.name + " file. The error is: " + e.Message);
		}
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
