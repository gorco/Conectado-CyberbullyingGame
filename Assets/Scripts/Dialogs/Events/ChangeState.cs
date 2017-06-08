using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : EventManager
{

	public IState state;
	public GameObject objectToChange;
	public Sprite[] sprites;
	public bool changePositionWhenChange = false;
	public Vector2[] newPos;
	public string keyEvent;

	/// <summary>
	/// Receive the pick event
	/// </summary>
	/// <param name="ev"></param>
	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "change state" && (keyEvent == null || keyEvent == "" ||
			((String)ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD)).Replace("\"", "")  == keyEvent))
		{
			object vAux = ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD);
			string var = null;
			if (vAux != null)
			{
				var = ((String)vAux).Replace("\"", "");
			}

			var value = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);

			object sAux = ev.getParameter(SequenceGenerator.EVENT_STATE_FIELD);
			int state = 0;
			if (sAux != null)
			{
				state = (int)sAux;
			}	

			this.ChangeStateObject(var, value, state);
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
	/// Change the GameObject sprite and/or change a variable value 
	/// </summary>
	/// <param name="varName"></param>
	/// <param name="varValue"></param>
	public void ChangeStateObject(string varName, System.Object varValue, int pos)
	{
		if(varName != null && varValue != null)
		{
			Type t = state.GetType();
			t.GetProperty(varName).SetValue(state, varValue, null);
		}

		GameObject that = this.gameObject;
		if (objectToChange)
		{
			that = objectToChange;
		}
		if (sprites.Length > 0)
		{
			that.GetComponent<SpriteRenderer>().sprite = sprites[pos];
		}
		if (changePositionWhenChange)
		{
			that.transform.localPosition = newPos[pos];
		}
	}
}

