using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BedroomDialogs : MonoBehaviour {

	public string fileName;

	private JSONObject json;

	private Sequence bed;
	private Sequence bag;
	private Sequence computer;
	private GameEvent gameEvent;

	// Use this for initialization
	void Start () {
		gameEvent = new GameEvent();

		StreamReader sr = new StreamReader(Application.dataPath + "/Texts/" + fileName);
		string fileContents = sr.ReadToEnd();
		sr.Close();

		Debug.Log(fileContents);
		json = JSONObject.Create(fileContents);

		this.bed = SequenceGenerator.createSimplyDialog("bed", json);
		this.bag = SequenceGenerator.createSimplyDialog("bag", json);
		this.computer = SequenceGenerator.createSimplyDialog("computer", json);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void startBedDialog()
	{
		this.gameEvent.Name = "start sequence";
		this.gameEvent.setParameter("sequence", this.bed);
		Game.main.enqueueEvent(this.gameEvent);
	}

	public void startBagDialog()
	{
		this.gameEvent.Name = "start sequence";
		this.gameEvent.setParameter("sequence", this.bag);
		Game.main.enqueueEvent(this.gameEvent);
	}

	public void startComputerDialog()
	{
		this.gameEvent.Name = "start sequence";
		this.gameEvent.setParameter("sequence", this.computer);
		Game.main.enqueueEvent(this.gameEvent);
	}
}
