using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OffComputer : MonoBehaviour
{
	public ComputerManager computer;
	public string keyValue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OffPC()
	{
		GameEvent gameEvent = new GameEvent();
		gameEvent.Name = "move camera";
		gameEvent.setParameter("key", keyValue);
		Game.main.enqueueEvent(gameEvent);

		computer.OffComputer();
	}

}
