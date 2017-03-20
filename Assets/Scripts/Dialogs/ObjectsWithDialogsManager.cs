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
			String saveName = child.GetComponent<ThrowDialog>().fieldName;
			String name = saveName.First().ToString().ToLower() + saveName.Substring(1);
			Debug.Log(saveName + " ----- " + name);
			if (json.HasField(name))
			{
				sequenceDict.Add(saveName, SequenceGenerator.createSimplyDialog(name, json, variablesObject));
			} else if (json.HasField(name.ToLower()))
            {
                sequenceDict.Add(saveName, SequenceGenerator.createSimplyDialog(name.ToLower(), json , variablesObject));
            } else
			{
				Debug.LogWarning("Dialog with key " + name + " doesn't exist in file " + fileName);
			}		 
        }

		/* TO TEST WITHOUT START AT THE FIRST SCENE*/
		GlobalState.UserName = "Juan";
		GlobalState.MaleSex = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startDialog(string objectName)
	{
		if (!sequenceDict.ContainsKey(objectName))
		{
			Debug.LogError("The sequence with key " + objectName + " doesn't exit");
			return;
		}

		this.gameEvent.setParameter("sequence", sequenceDict[objectName]);
		Game.main.enqueueEvent(this.gameEvent);
	}
}
