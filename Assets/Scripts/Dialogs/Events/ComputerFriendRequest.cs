using Isometra;
using Isometra.Sequences;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerFriendRequest : EventManager
{

	public ComputerManager computer;
	private JSONObject jsonObj;
	public TextAsset jsonFile;

	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "computer friend")
		{
			string name = ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD).ToString().Replace("\"", "");
			string state = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD).ToString().Replace("\"", "");
			object globalKeyObj = ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD);
			string globalKey = globalKeyObj != null ? globalKeyObj.ToString().Replace("\"", "") : "";

			object otherObj = ev.getParameter(SequenceGenerator.EVENT_OTHER_FIELD);
			string other = otherObj != null ? otherObj.ToString().Replace("\"", "") : "";

			float time = (float)ev.getParameter(SequenceGenerator.EVENT_TIME_FIELD);
			StartCoroutine(ExecuteAfterTime(time, name, state, globalKey, other));
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
			/*
			try
			{
				Sequence seq = SequenceGenerator.createSimplyDialog("default", jsonObj);
				computer.SetDefaultPublicationButtonSequence(seq);
			}
			catch (Exception e)
			{
				Debug.LogError("Error in " + jsonFile.name + " file. The error is: " + e.Message);
			}
			*/
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator ExecuteAfterTime(float time, string name, string state, string globalKey, string other)
	{
		yield return new WaitForSeconds(time);

		Sequence accept = null;
		Sequence deny = null;

		if (jsonObj)
		{
			if (other != "" && other.Contains(",")) ;
			{
				string[] keys = other.Split(',');
				try
				{
					if (keys.Length > 0 && keys[0]!="")
					{
						accept = SequenceGenerator.createSimplyDialog(keys[0], jsonObj);
					}
					if (keys.Length > 1 && keys[1] != "")
					{
						deny = SequenceGenerator.createSimplyDialog(keys[1], jsonObj);
					}
				} catch (Exception e)
				{
					Debug.LogError("Error in " + jsonFile.name + " file. The error is: " + e.Message);
				}
				
			}
		}
		computer.AddFriendRequest(name, state, globalKey, accept, deny);
	}
}
