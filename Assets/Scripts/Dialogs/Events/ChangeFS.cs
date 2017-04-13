using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFS : EventManager
{
	public IState state;

	/// <summary>
	/// Receive the change friendship event
	/// </summary>
	/// <param name="ev"></param>
	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "change friendship")
		{
			object vAux = ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD);
			string var = null;
			if (vAux != null)
			{
				var = ((String)vAux).Replace("\"", "");
			}

			var value = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);

			this.ChangeFSValue(var, value);
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
	private void ChangeFSValue(string varName, System.Object varValue)
	{
		if (varName != null && varValue != null)
		{
			Type t = state.GetType();
			var oldValue = t.GetProperty(varName).GetValue(state, null);
			var newValue = (int)varValue + (int)oldValue;
			t.GetProperty(varName).SetValue(state, newValue, null);
		}
	}
}
