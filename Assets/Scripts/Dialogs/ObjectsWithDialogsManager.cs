using Isometra;
using Isometra.Sequences;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;


public class ObjectsWithDialogsManager : MonoBehaviour {

	private Dictionary<string, Sequence> sequenceDict;
	private GameEvent gameEvent;

	public string fileName;
	TextAsset jsonFile;
	public IState variablesObject;


	private void Awake()
	{
		sequenceDict = new Dictionary<string, Sequence>();

		gameEvent = new GameEvent();
		this.gameEvent.Name = "start sequence";

        //char[] MyChar = { '.', 'j', 's', 'o', 'n' };
        //string newName = fileName.TrimEnd(MyChar);
        string newName = fileName.Replace(".json", "");
        fileName = newName;
        
        if(LanguageSelector.instance.GetCurrentLanguage() == null)
        {
            Debug.LogError("ERROR: Global Language not set up propertly. Switching to en_UK.");
            LanguageSelector.instance.SetLanguage("en_UK");
            jsonFile = Resources.Load<TextAsset>("Localization/" + "en_UK" + "/Dictionaries" + fileName);
        }
        else
            jsonFile = Resources.Load<TextAsset>("Localization/" + LanguageSelector.instance.GetCurrentLanguage() + "/" + fileName);

        string fileContents = jsonFile.text;
        JSONObject json = JSONObject.Create(fileContents);
		foreach (Transform child in transform)
		{
			if (!child.GetComponent<ThrowDialog>())
			{
				Debug.LogWarning("The object with the name " + child.gameObject.name + " doesn't have ThrowDialog component");
			}
			else
			{
				ThrowDialog dialog = child.GetComponent<ThrowDialog>();
				String saveName = dialog.fieldName;
				String name = saveName.First().ToString().ToLower() + saveName.Substring(1);
				if (!sequenceDict.ContainsKey(saveName))
				{
					try
					{
						if (json.HasField(name))
						{
							sequenceDict.Add(saveName, SequenceGenerator.createSimplyDialog(name, json, variablesObject));
						}
						else if (json.HasField(name.ToLower()))
						{
							sequenceDict.Add(saveName, SequenceGenerator.createSimplyDialog(name.ToLower(), json, variablesObject));
						}
						else
						{
							Debug.LogWarning("Dialog with key " + name + " doesn't exist in file " + fileName);
						}
					} catch (Exception e)
					{
						Debug.LogError("Error in " + jsonFile.name + " file. The error is: " + e.Message);
					}
				}

			}
		}

	}
	// Update is called once per frame
	void Update () {
		
	}

	public void startDialog(string objectName)
	{
		if (!sequenceDict.ContainsKey(objectName))
		{
			Debug.LogError("The sequence with key " + objectName + " doesn't exit (Object "+this.gameObject.name+")");
			return;
		}

		this.gameEvent.setParameter("sequence", sequenceDict[objectName]);
		Game.main.enqueueEvent(this.gameEvent);
	}
	private void OnValidate()
	{
#if UNITY_EDITOR
		if (jsonFile == null && !string.IsNullOrEmpty(fileName))
		{
			jsonFile = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>("Localization/" + LanguageSelector.instance.GetCurrentLanguage() + "/" + fileName);
			Debug.Log("JSON FILE Setted: " + fileName);
		}
#endif
	}
}
