using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SequenceGenerator  {

	public const string DIALOG_TYPE = "dialog";
	public const string FORK_TYPE = "fork";
	public const string OPTIONS_TYPE = "options";
	public const string EVENT_TYPE = "event";
	public const string NEXT_FIELD = "next";
	public const string TYPE_FIELD = "type";
	public const string FRAGMENTS_FIELD = "fragments";
	public const string TAG_FIELD = "tag";
	public const string MESSAGE_FIELD = "msn";
	public const string OPTIONS_FIELD = "options";
	public const string CONDITION_FIELD = "condition";
	public const string EVENT_FIELD = "event";
	public const string EVENT_NAME_FIELD = "name";
	public const string EVENT_VALUE_FIELD = "value";
	public const string EVENT_VARIABLE_FIELD = "var";
	public const string EVENT_STATE_FIELD = "state";
	public const string EVENT_KEY_FIELD = "key";

	public const string GLOBAL_STATE = "global";
	public const string LOCAL_STATE = "state";

	public static Sequence createSimplyDialog(string key, JSONObject json, IState variablesObject)
	{
		Sequence seq = ScriptableObject.CreateInstance<Sequence>();
		if (variablesObject && seq.GetObject(LOCAL_STATE) == null)
		{
			seq.SetObject(LOCAL_STATE, variablesObject);
		}
		if (seq.GetObject(GLOBAL_STATE) == null)
		{
			seq.SetObject(GLOBAL_STATE, GlobalState.Instance);
		}

		if (!json.HasField(key))
		{
			Debug.LogError("Not found the key " + key);
			return null;
		}

		JSONObject jsonObj = json.GetField(key);

		string nodeId = "root";

		createNode(seq, jsonObj, variablesObject, nodeId, key);

		return seq;
	}

	internal static Sequence createSimplyDialog(string key, JSONObject json)
	{
		return createSimplyDialog(key, json, null);
	}

	internal static void createNode(Sequence seq, JSONObject jsonObj, IState variables, string nodeId,  string key)
	{
		if(seq[nodeId]!=null && seq[nodeId].Content != null)
		{
			Debug.LogWarning("Bucle encontrado");
			return;
		}

		if (!jsonObj.HasField(nodeId))
		{
			Debug.LogError("The json of "+ key + " doesn't have node with id "+ nodeId);
		}

		JSONObject node = jsonObj.GetField(nodeId);
		if (!node.HasField(TYPE_FIELD))
		{
			Debug.LogError("The node " + key + "->" + nodeId + "need a type field");
		}

		string typeNode = node.GetField(TYPE_FIELD).ToString().Replace("\"", "");

		//Create the node
		switch (typeNode)
		{
			case DIALOG_TYPE:
				createDialogNode(seq, jsonObj, variables, nodeId, key);
				break;
			case FORK_TYPE:
				createForkNode(seq, jsonObj, variables, nodeId, key);
				break;
			case OPTIONS_TYPE:
				createOptionsNode(seq, jsonObj, variables, nodeId, key);
				break;
			case EVENT_TYPE:
				createEventNode(seq, jsonObj, variables, nodeId, key);
				break;
			default:
				Debug.LogError("The type " + typeNode + "doesn't exist in " + key + "->" + nodeId);
				break;
		}		
	}

	internal static void createDialogNode(Sequence seq, JSONObject jsonObj, IState variables,  string nodeId, string key)
	{
		JSONObject node = jsonObj.GetField(nodeId);

		if (!node.HasField(FRAGMENTS_FIELD))
		{
			Debug.LogError("The node " + key + "->" + nodeId + " need a "+FRAGMENTS_FIELD+" field");
			return;
		}
		List<JSONObject> fragmentList = node.GetField(FRAGMENTS_FIELD).list;

		List<Fragment> fragments = new List<Fragment>();

		foreach (JSONObject j in fragmentList)
		{
			if (j.HasField(TAG_FIELD) && j.HasField(MESSAGE_FIELD))
			{
				fragments.Add(new Fragment(j.GetField(TAG_FIELD).ToString().Replace("\"", ""), j.GetField(MESSAGE_FIELD).ToString().Replace("\"", "")));
			}
			else
			{
				Debug.LogError("The node " + key + "->" + nodeId + " need a " + TAG_FIELD + " and " + MESSAGE_FIELD + " field");
			}
		}

		SequenceNode newNode = seq[nodeId];
		newNode.Content = Dialog.Create(fragments);

		//Assign content if root
		if (nodeId == "root")
		{
			seq.Root = newNode;
		}

		//Create child
		if (node.HasField(NEXT_FIELD))
		{
			string nextNodeId = node.GetField(NEXT_FIELD).ToString().Replace("\"", "");
			if (nodeId == "root")
			{
				seq.Root.Childs[0] = seq[nextNodeId];
			}
			else
			{
				seq[nodeId][0] = seq[nextNodeId];
			}
			createNode(seq, jsonObj, variables, nextNodeId, key);
		}
	}

	internal static void createForkNode(Sequence seq, JSONObject jsonObj, IState variables, string nodeId, string key)
	{
		JSONObject node = jsonObj.GetField(nodeId);
		if (!node.HasField(OPTIONS_FIELD))
		{
			Debug.LogError("The node " + key + "->" + nodeId + " need a " + OPTIONS_FIELD + " field");
			return;
		}

		List<JSONObject> optionsList = node.GetField(OPTIONS_FIELD).list;

		List<Checkable> checkables = new List<Checkable>();

		foreach (JSONObject j in optionsList)
		{
			if (j.HasField(NEXT_FIELD) && j.HasField(CONDITION_FIELD))
			{
				checkables.Add(FormulaFork.Create(j.GetField(CONDITION_FIELD).ToString().Replace("\"", "")));
			}
			else
			{
				Debug.LogError("The node " + key + "->" + nodeId + " need a " + NEXT_FIELD + " and " + CONDITION_FIELD + " field");
			}
		}

		SequenceNode newNode = seq[nodeId];
		newNode.Content = MultiFork.Create(checkables);

		if (nodeId == "root")
		{
			seq.Root = newNode;
		}

		int child = 0;
		foreach (JSONObject j in optionsList)
		{
			if (j.HasField(NEXT_FIELD))
			{
				string nextNodeId = j.GetField(NEXT_FIELD).ToString().Replace("\"", "");
				if (nodeId == "root")
				{
					seq.Root.Childs[child] = seq[nextNodeId];
				}
				else
				{
					seq[nodeId][child] = seq[nextNodeId];
				}
				createNode(seq, jsonObj, variables, nextNodeId, key);
				child++;
			}
			else
			{
				Debug.LogError("The node " + key + "->" + nodeId + " need a " + NEXT_FIELD + " and " + MESSAGE_FIELD + " field");
			}
		}
	}

	internal static void createEventNode(Sequence seq, JSONObject jsonObj, IState variables, string nodeId, string key)
	{
		
		JSONObject node = jsonObj.GetField(nodeId);
		if (!node.HasField(EVENT_FIELD))
		{
			Debug.LogError("The node " + key + "->" + nodeId + " need a " + OPTIONS_FIELD + " field");
			return;
		}

		SequenceNode newNode = seq[nodeId];

		JSONObject eventNode = node.GetField(EVENT_FIELD);
		if (!eventNode.HasField(EVENT_NAME_FIELD))
		{
			Debug.LogError("The node " + key + "->" + nodeId + " event need a " + EVENT_NAME_FIELD + " field");
			return;
		}

		GameEvent aux = new GameEvent();
		aux.name = eventNode.GetField(EVENT_NAME_FIELD).ToString().Replace("\"", "");

		if (eventNode.HasField(EVENT_KEY_FIELD))
		{
			string keyEvent = eventNode.GetField(EVENT_KEY_FIELD).ToString().Replace("\"", "");
			aux.setParameter(EVENT_KEY_FIELD, keyEvent);

		}

		if (eventNode.HasField(EVENT_STATE_FIELD))
		{
			JSONObject state = eventNode.GetField(EVENT_STATE_FIELD);
			if (state.IsNumber)
			{
				aux.setParameter(EVENT_STATE_FIELD, Int32.Parse(state.ToString().Replace("\"", "")));
			}
			else
			{
				Debug.LogError("The field " + EVENT_STATE_FIELD + " in node " + key + "->" + nodeId + " has to be a number");
			}
		}

		if (eventNode.HasField(EVENT_VARIABLE_FIELD))
		{
			string variable = eventNode.GetField(EVENT_VARIABLE_FIELD).ToString().Replace("\"", "");
			if (!eventNode.HasField(EVENT_VALUE_FIELD))
			{
				Debug.LogError("The event "+ variable + " in node " + key + "->" + nodeId + " need a " + EVENT_VALUE_FIELD + " field");
				return;
			} else
			{
				JSONObject value = eventNode.GetField(EVENT_VALUE_FIELD);
				if (value.IsBool)
				{
					aux.setParameter(EVENT_VALUE_FIELD, (bool)value);
				} else if (value.IsNumber)
				{
					aux.setParameter(EVENT_VALUE_FIELD, Int32.Parse(value.ToString().Replace("\"", "")));
				} else 
				{
					aux.setParameter(EVENT_VALUE_FIELD, value);
				}
				aux.setParameter(EVENT_VARIABLE_FIELD, variable);
			}
		}

		newNode.Content = aux;

		//Assign content if root
		if (nodeId == "root")
		{
			seq.Root = newNode;
		}

		//Create child
		if (node.HasField(NEXT_FIELD))
		{
			string nextNodeId = node.GetField(NEXT_FIELD).ToString().Replace("\"", "");
			Debug.Log("Siguiente "+ key +" nodo a " + nodeId + "= " + nextNodeId);
			if (nodeId == "root")
			{
				seq.Root.Childs[0] = seq[nextNodeId];
			}
			else
			{
				seq[nodeId][0] = seq[nextNodeId];
			}
			createNode(seq, jsonObj, variables, nextNodeId, key);
		}
	}

	internal static void createOptionsNode(Sequence seq, JSONObject jsonObj, IState variables, string nodeId, string key)
	{
		JSONObject node = jsonObj.GetField(nodeId);
		if (!node.HasField(OPTIONS_FIELD))
		{
			Debug.LogError("The node " + key + "->" + nodeId + " need a " + OPTIONS_FIELD + " field");
			return;
		}

		List<JSONObject> optionsList = node.GetField(OPTIONS_FIELD).list;

		List<Option> options = new List<Option>();

		foreach (JSONObject j in optionsList)
		{
			if (j.HasField(MESSAGE_FIELD))
			{
				options.Add(new Option(j.GetField(MESSAGE_FIELD).ToString().Replace("\"", "")));
			}
			else
			{
				Debug.LogError("The node " + key + "->" + nodeId + " need a " + MESSAGE_FIELD + " field");
			}
		}

		SequenceNode newNode = seq[nodeId];
		newNode.Content = Options.Create(options);

		if (nodeId == "root")
		{
			seq.Root = newNode;
		}

		int child = 0;
		foreach (JSONObject j in optionsList)
		{
			if (j.HasField(NEXT_FIELD) && j.HasField(MESSAGE_FIELD))
			{
				string nextNodeId = j.GetField(NEXT_FIELD).ToString().Replace("\"", "");
				if (nodeId == "root")
				{
					seq.Root.Childs[child] = seq[nextNodeId];
				}
				else
				{
					seq[nodeId][child] = seq[nextNodeId];
				}
				createNode(seq, jsonObj, variables, nextNodeId, key);
				child++;
			}
		}
	}

}
