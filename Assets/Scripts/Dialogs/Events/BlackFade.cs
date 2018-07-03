using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFade : EventManager {

	public GameObject black;
	public float blackDuration;

	private float dTime = 0;
	private int state = 0;
	private IGameEvent gEvent;

	public bool sync;

	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "black")
		{
			String value = (String)ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);
			gEvent = ev;
			ThrowBlackImage(value);
		}
	}

	public override void Tick()
	{
	}

	private void Update()
	{
		if (state == 1)
		{
			black.GetComponent<Image>().CrossFadeAlpha(1.0f, 2.0f, false);
			black.GetComponentInChildren<Text>().CrossFadeAlpha(1.0f, 2.0f, false);
			state = 2;
			dTime = 0;
		} else if (state == 2 && dTime >= blackDuration)
		{
			black.GetComponent<Image>().CrossFadeAlpha(0.0f, 2.0f, false);
			black.GetComponentInChildren<Text>().CrossFadeAlpha(0.0f, 2.0f, false);
			state = 3;
		} else if (state == 3)
		{
			Game.main.eventFinished(gEvent);
			state = 4;
			dTime = 0;
		} else if (state == 4 && dTime > 1.5f)
		{
			black.SetActive(false);
		}
		dTime += Time.deltaTime;
	}

	private void ThrowBlackImage(String value)
	{
		black.SetActive(true); 
		black.GetComponentInChildren<UnityEngine.UI.Text>().text = value;
		state = 1;
		
	}
}
