using Isometra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Isometra.Sequences;

public class GenerateChatSequence : EventManager
{
	public MobileChat mobile;
	public string fileName;
	private JSONObject jsonObj;

	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "chat sequence")
		{
			string chatName = ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD).ToString().Replace("\"", "");
			string jsonKey = ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD).ToString().Replace("\"", "");
			if(jsonKey == "empty")
			{
				RemoveSequence(chatName);
			} else
			{
				GenerateAndSetSequence(jsonKey, chatName);
			}
		}
	}

	public override void Tick()
	{
		throw new NotImplementedException();
	}

	// Use this for initialization
	void Start () {
		StreamReader sr = new StreamReader(Application.dataPath + "/Texts/" + fileName);
		string fileContents = sr.ReadToEnd();
		sr.Close();

		jsonObj = JSONObject.Create(fileContents);
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
}
