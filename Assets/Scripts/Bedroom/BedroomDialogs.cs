using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BedroomDialogs : MonoBehaviour {

	public string fileName;

	private JSONObject json;

	private Sequence bed;
	private Sequence bag;
	private GameEvent gameEvent;

	// Use this for initialization
	void Start () {
		gameEvent = new GameEvent();

		StreamReader sr = new StreamReader(Application.dataPath + "/Texts/" + fileName);
		string fileContents = sr.ReadToEnd();
		sr.Close();

		Debug.Log(fileContents);
		json = JSONObject.Create(fileContents);

		this.bed = createSimplyDialog("bed");
		this.bag = createSimplyDialog("bag");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private Sequence createSimplyDialog(string key)
	{
		Sequence seq = ScriptableObject.CreateInstance<Sequence>();
		JSONObject jsonObj = this.json.GetField(key);

		List<Fragment> fragments = new List<Fragment>();

		bool endNode = false;
		string nodeId = "root";
		while (!endNode)
		{
			JSONObject node = jsonObj.GetField(nodeId);
			string nextNode = "";
			foreach (JSONObject j in node.list)
			{
				fragments.Add(new Fragment(j.GetField("tag").ToString(), j.GetField("msn").ToString()));
				if (j.HasField("next"))
				{
					nextNode = j.GetField("next").ToString().Replace("\"","");
				}
			}
			if (nodeId == "root")
			{
				seq.Root = seq.createChild(Dialog.Create(fragments));
			}
			if (nextNode != "")
			{
				nodeId = nextNode;
			} else
			{
				endNode = true;
			}
		}
		return seq;
	}

	public void startBedDialog()
	{
		this.gameEvent.Name = "bed interaction";
		this.gameEvent.setParameter("sequence", this.bed);
		Game.main.enqueueEvent(this.gameEvent);
	}

	public void startBagDialog()
	{
		this.gameEvent.Name = "bag interaction";
		this.gameEvent.setParameter("sequence", this.bag);
		Game.main.enqueueEvent(this.gameEvent);
	}
}
