using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToScene : EventManager
{
	/// <summary>
	/// Receive the pick event
	/// </summary>
	/// <param name="ev"></param>
	public override void ReceiveEvent(IGameEvent ev)
	{
		Debug.Log("EVENTO " + ev.Name);
		if (ev.Name.Replace("\"", "") == "change scene")
		{
			int scene = (int)ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);

			this.ChangeScene(scene);
		}
	}

	public override void Tick()
	{
		//throw new NotImplementedException();
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// Change to the scene with the carValue
	/// </summary>
	/// <param name="varValue"></param>
	private void ChangeScene(int varValue)
	{
		SceneManager.LoadScene(varValue);
	}
}

