using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SequenceGenerator  {

	private const string DIALOG_TYPE = "dialog";
	private const string FORK_TYPE = "fork";
	private const string OPTIONS_TYPE = "options";
	private const string NEXT_FIELD = "next";
	private const string TYPE_FIELD = "type";
	private const string FRAGMENTS_FIELD = "fragments";
	private const string TAG_FIELD = "tag";
	private const string MESSAGE_FIELD = "msn";
	private const string OPTIONS_FIELD = "options";

	public static Sequence createSimplyDialog(string key, JSONObject json)
	{
		Sequence seq = ScriptableObject.CreateInstance<Sequence>();
		JSONObject jsonObj = json.GetField(key);

		string nodeId = "root";

		createNode(seq, jsonObj, nodeId, key);

		return seq;
	}

	internal static void createNode(Sequence seq, JSONObject jsonObj, string nodeId, string key)
	{
		createNode(seq, jsonObj, nodeId, 0, key);
	}

	internal static void createNode(Sequence seq, JSONObject jsonObj, string nodeId, int childPos, string key)
	{
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
				createDialogNode(seq, jsonObj, nodeId, key);
				break;
			case FORK_TYPE:
				break;
			case OPTIONS_TYPE:
				createOptionsNode(seq, jsonObj, nodeId, key);
				break;
			default:
				Debug.LogError("The type " + typeNode + "doesn't exist in " + key + "->" + nodeId);
				break;
		}		
	}

	internal static void createDialogNode(Sequence seq, JSONObject jsonObj, string nodeId, string key)
	{
		createDialogNode(seq, jsonObj, nodeId, 0, key);
	}

	internal static void createDialogNode(Sequence seq, JSONObject jsonObj, string nodeId, int childPos, string key)
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
				fragments.Add(new Fragment(j.GetField(TAG_FIELD).ToString(), j.GetField(MESSAGE_FIELD).ToString()));
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
				seq.Root.Childs[childPos] = seq[nextNodeId];
			}
			else
			{
				seq[nodeId][childPos] = seq[nextNodeId];
			}
			createNode(seq, jsonObj, nextNodeId, key);
		}
	}

	internal static SequenceNode createForkNode()
	{
		return null;
	}

	internal static void createOptionsNode(Sequence seq, JSONObject jsonObj, string nodeId, string key)
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
			if (j.HasField(NEXT_FIELD) && j.HasField(MESSAGE_FIELD))
			{
				options.Add(new Option(j.GetField(MESSAGE_FIELD).ToString()));
			}
			else
			{
				Debug.LogError("The node " + key + "->" + nodeId + " need a " + NEXT_FIELD + " and " + MESSAGE_FIELD + " field");
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
				createNode(seq, jsonObj, nextNodeId, key);
				child++;
			}
			else
			{
				Debug.LogError("The node " + key + "->" + nodeId + " need a " + NEXT_FIELD + " and " + MESSAGE_FIELD + " field");
			}
		}
	}

}
