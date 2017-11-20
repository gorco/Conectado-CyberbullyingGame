using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToScene : EventManager
{

	public Eyelids eyelid;

	/// <summary>
	/// Receive the pick event
	/// </summary>
	/// <param name="ev"></param>
	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "change scene")
		{
			int scene = (int)ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);
			object timeObj = ev.getParameter(SequenceGenerator.EVENT_TIME_FIELD);
			float time = timeObj != null ? (float)timeObj : 1f;

			if (time > 0 && eyelid)
			{
				eyelid.goToSleep(time - 0.2f);
			}
			StartCoroutine(ChangeScene(time, scene));
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
	private IEnumerator ChangeScene(float time, int varValue)
	{
		yield return new WaitForSeconds(time);

		SceneManager.LoadScene(varValue);
	}
}

