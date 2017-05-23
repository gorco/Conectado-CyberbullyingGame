using Isometra;
using Isometra.Sequences;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPublication : EventManager
{

	public ComputerManager computer;
	private JSONObject jsonObj;
	public TextAsset jsonFile;

	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "computer publication")
		{
			object otherObj = ev.getParameter(SequenceGenerator.EVENT_OTHER_FIELD);
			string other = otherObj != null ? otherObj.ToString().Replace("\"", "") : "";
			if (other == "save")
			{
				computer.SaveComputerState();
				return;
			}
			if (other == "load")
			{
				computer.LoadComputerState();
				return;
			}

			string publicationKey = ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD).ToString().Replace("\"", "");
			string message = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD).ToString().Replace("\"", "");
			string author = ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD).ToString().Replace("\"", "");

			float time = (float)ev.getParameter(SequenceGenerator.EVENT_TIME_FIELD);
			StartCoroutine(ExecuteAfterTime(time, author, message, publicationKey, other));
		}
	}

	public override void Tick()
	{
	}

	// Use this for initialization
	void Start()
	{
		if (jsonFile != null)
		{
			string fileContents = jsonFile.text;
			jsonObj = JSONObject.Create(fileContents);

			try
			{
				Sequence seq = SequenceGenerator.createSimplyDialog("default", jsonObj);
				computer.SetDefaultPublicationButtonSequence(seq);
			}
			catch (Exception e)
			{
				Debug.LogError("Error in " + jsonFile.name + " file. The error is: " + e.Message);
			}
			
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void GenerateAndSetSequence(string jsonKey, string publicationKey)
	{
		try
		{
			Sequence seq = SequenceGenerator.createSimplyDialog(jsonKey, jsonObj);
			computer.SetPublicationButtonSequence(seq);
		}
		catch(Exception e)
		{
			Debug.LogError("Error in " + jsonFile.name + " file. The error is: " + e.Message);
		}
		
	}

	IEnumerator ExecuteAfterTime(float time, string author, string message, string key, string other)
	{
		yield return new WaitForSeconds(time);

		computer.NewPublication(author, message, key, other);
	}
}
