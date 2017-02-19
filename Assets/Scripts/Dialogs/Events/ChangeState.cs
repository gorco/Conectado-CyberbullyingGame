using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : EventManager
{

	public IState state;
	public GameObject objectToChange;
	public Sprite[] sprites;

	/// <summary>
	/// Receive the pick event
	/// </summary>
	/// <param name="ev"></param>
	public override void ReceiveEvent(IGameEvent ev)
	{
		Debug.Log("EVENTO " + ev.Name);
		if (ev.Name.Replace("\"", "") == "change state")
		{
			string var = ((String)ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD)).Replace("\"", "");

			var value = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);

			int state = (int)ev.getParameter(SequenceGenerator.EVENT_STATE_FIELD);

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
	private void ChangeStateObject(string varName, System.Object varValue, int pos)
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
		that.GetComponent<SpriteRenderer>().sprite = sprites[pos];
	}
}

