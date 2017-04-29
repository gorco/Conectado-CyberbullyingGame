using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntranceEvent : EventManager {

	public GameObject character;
	public GameObject finalSprite;

	public float xGoal;
	public float movementDuration;

	private float dTime = 0;
	private int state = 0;
	private IGameEvent gEvent;

	private float xMovement;
	private float xStart;

	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "class entrance")
		{
			String value = (String)ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);
			gEvent = ev;
			AnimateCharacter();
		}
	}

	public override void Tick()
	{
	}

	void AnimateCharacter()
	{
		character.SetActive(true);
		state = 1;
	}

	// Use this for initialization
	void Start () {
		xStart = character.transform.localPosition.x;
		xMovement = xGoal - xStart;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 1)
		{
			//character.GetComponent<Image>().CrossFadeAlpha(1.0f, 2.0f, false);
			state = 2;
			dTime = 0;
		}
		else if (state == 2 && dTime <= movementDuration)
		{
			Vector2 position = character.transform.localPosition;
			character.transform.localPosition = new Vector2(xStart + xMovement*(dTime/movementDuration), position.y);
		}
		else if (state == 2)
		{
			if (finalSprite)
			{
				character.SetActive(false);
				finalSprite.SetActive(true);
			}
			
			Game.main.eventFinished(gEvent);
		}
		dTime += Time.deltaTime;
	}
}
