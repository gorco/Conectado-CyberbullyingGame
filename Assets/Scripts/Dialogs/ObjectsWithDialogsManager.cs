using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class ObjectsWithDialogsManager : MonoBehaviour {

	private Dictionary<string, Sequence> sequenceDict;
	private GameEvent gameEvent;

	public string fileName;
	public IState variablesObject;

	// Use this for initialization
	void Start () {
		sequenceDict = new Dictionary<string, Sequence>();

		gameEvent = new GameEvent();
		this.gameEvent.Name = "start sequence";

		StreamReader sr = new StreamReader(Application.dataPath + "/Texts/" + fileName);
		string fileContents = sr.ReadToEnd();
		sr.Close();

		Debug.Log(fileContents);
		JSONObject json = JSONObject.Create(fileContents);

		foreach (Transform child in transform)
        {
			String name = child.name;
			name = name.First().ToString().ToLower() + name.Substring(1);
			if (json.HasField(name))
			{
				sequenceDict.Add(child.name, SequenceGenerator.createSimplyDialog(name, json, variablesObject));
			} else if (json.HasField(name.ToLower()))
            {
                sequenceDict.Add(child.name, SequenceGenerator.createSimplyDialog(name.ToLower(), json , variablesObject));
            }		 
        }		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startDialog(string objectName)
	{
		this.gameEvent.setParameter("sequence", sequenceDict[objectName]);
		Game.main.enqueueEvent(this.gameEvent);
	}
}
