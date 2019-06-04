using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToScene : EventManager
{

	public Eyelids eyelid;
	public GameObject loading;

	private float cTime = 0;

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
			if (loading != null)
			{
				loading.SetActive(true);
			}

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
		cTime += Time.deltaTime;
	}

	/// <summary>
	/// Change to the scene with the carValue
	/// </summary>
	/// <param name="varValue"></param>
	private IEnumerator ChangeScene(float time, int varValue)
	{
		if (loading)
		{
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(varValue +1);
			cTime = 0;
			// Wait until the asynchronous scene fully loads
			while (cTime < time && !asyncLoad.isDone)
			{
				yield return null;
			}
		}
		else
		{
			yield return new WaitForSeconds(time);

			SceneManager.LoadScene(varValue +1);
		}
	}
}

