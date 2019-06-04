using Isometra;
using Isometra.Sequences;
using NCalc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ComputerSocialComment : EventManager
{

    public ComputerManager computer;
    private JSONObject jsonObj;
    public string fileName;
    TextAsset jsonFile;

    public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "computer comment")
		{
			string publicationKey = ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD).ToString().Replace("\"", "");
			string message = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD).ToString().Replace("\"", "");
			string author = ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD).ToString().Replace("\"", "");

			float time = (float)ev.getParameter(SequenceGenerator.EVENT_TIME_FIELD);
			object otherObj = ev.getParameter(SequenceGenerator.EVENT_OTHER_FIELD);
			string other = otherObj != null ? otherObj.ToString().Replace("\"", "") : "";

			StartCoroutine(ExecuteAfterTime(time, publicationKey, author, message, other));
		}
	}

	public override void Tick()
	{
	}

	// Use this for initialization
	void Start()
	{
        jsonFile = Resources.Load<TextAsset>("Localization/" + LanguageSelector.instance.GetCurrentLanguage() + "/" + fileName);

        if (jsonFile != null)
		{
			string fileContents = jsonFile.text;
			jsonObj = JSONObject.Create(fileContents);

			try
			{
				Sequence seq = SequenceGenerator.createSimplyDialog("default", jsonObj);
				computer.AddDefaultPublicationCommentSequence(seq);
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
			computer.AddPublicationCommentSequence(publicationKey, seq);
		} catch (Exception e)
		{
			Debug.LogError("Error in " + jsonFile.name + " file. The error is: " + e.Message);
		}
	}

	private void RemoveSequence(string publicationKey)
	{
		
		computer.RemovePublicationCommentSequence(publicationKey);
		
	}

	IEnumerator ExecuteAfterTime(float time, string publicationKey, string author, string message, string other)
	{
		yield return new WaitForSeconds(time);

		if (jsonObj)
		{
			if (other == "empty")
			{
				RemoveSequence(publicationKey);
			}
			else if (other != "")
			{
				GenerateAndSetSequence(other, publicationKey);
			}
		}

		computer.AddPublicationComment(publicationKey, author, message);
	}
}
