using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVariable : EventManager
{
	public IState state;

	/// <summary>
	/// Receive the change friendship event
	/// </summary>
	/// <param name="ev"></param>
	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "change variable")
		{
			object vAux = ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD);
			string var = null;
			if (vAux != null)
			{
				var = ((String)vAux).Replace("\"", "");
			}

			var value = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);
			this.ChangeVarValue(var, value);
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
	/// Change the variable value 
	/// </summary>
	/// <param name="varName"></param>
	/// <param name="varValue"></param>
	private void ChangeVarValue(string varName, System.Object varValue)
	{
		if (varName != null && varValue != null)
		{
			if (varName == "hour" && varValue.ToString().Contains(":")) 
			{
				string[] hour = varValue.ToString().Split(':');
				GlobalState.Hour = Int32.Parse(hour[0]);
				GlobalState.Minute = Int32.Parse(hour[1]);
				return;
			}

			Type t = null;
			if(state != null)
			{
				t = state.GetType();
			}

			if (t != null && t.GetProperty(varName) != null)
			{
				t.GetProperty(varName).SetValue(state, varValue, null);
			}
			else
			{
				GlobalState gState = GlobalState.Instance;
				t = gState.GetType();
				try
				{
					t.GetProperty(varName).SetValue(gState, varValue, null);
				} catch (Exception e)
				{
					Debug.Log("Error with the variable: "+varName +"\n"+e);
				}
			}
		}
	}
}
