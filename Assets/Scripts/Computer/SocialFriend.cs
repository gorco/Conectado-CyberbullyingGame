using System;
using System.Collections;
using System.Collections.Generic;
using Isometra.Sequences;
using UnityEngine;
using UnityEngine.UI;
using Isometra;

public class SocialFriend : MonoBehaviour {

	public Text nameUser;
	public Text state;

	public Sprite acceptedBG; 

	public GameObject accept;
	public GameObject deny;
	public GameObject block;
	public GameObject avatar;

	public Sprite[] avatarImages;
	public String[] namesAvatarImages;

	private ComputerManager parent;

	private bool accepted;

	private string variable = "";

	private Sequence acceptSeq;
	private Sequence denySeq;

	private GameEvent gameEvent;

	// Use this for initialization
	void Start () {
		accepted = false;
	}

	private void Awake()
	{
		this.gameEvent = new GameEvent();
		this.gameEvent.Name = "start sequence";
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void InitFriendRequest(string name, string state, ComputerManager parent)
	{
		this.parent = parent;
		this.nameUser.text = name;
		this.state.text = state;

		string avatarFile = "";
		if (name == "Guille" || name == "Guillermo" || name == "Guillaume")
		{
			avatarFile = "Guille";
		}
		else if (name == "Alex" || name == "Álex" || name == "Alejandro" || name == "Alexandre")
		{
			avatarFile = "Alex";
		}
		else if (name == "Ana" || name == "Anna")
		{
			avatarFile = "Ana";
		}
		else if (name == "Alice" || name == "Alison")
		{
			avatarFile = "Alison";
		}
		else if (name == "Jose" || name == "Joseph" || name == "José")
		{
			avatarFile = "Jose";
		}

		// SET AVATAR
		Image img = this.avatar.GetComponent<Image>();
		img.sprite = Resources.Load<Sprite>(avatarFile + "Avatar");
	}

	public void AcceptFriend()
	{
		ThrowDialog(acceptSeq);
		Resolve(true);
		this.GetComponent<Image>().sprite = acceptedBG;
		block.SetActive(true);
		state.gameObject.SetActive(true);
		if (variable != "")
		{
			Type t = GlobalState.Instance.GetType();
			t.GetProperty(variable).SetValue(GlobalState.Instance, true, null);
		}
	}

	public void DenyFriend()
	{
		ThrowDialog(denySeq);
		Resolve(false);
	}

	public string GetFriendName()
	{
		return nameUser.text;
	}

	private void Resolve(bool accepted)
	{
		parent.ResolveFriendRequest(accepted, this.nameUser.text, this);
		accept.SetActive(false);
		deny.SetActive(false);
	}

	internal void SetGlobalVariable(string globalKey)
	{
		this.variable = globalKey;
	}

	public void ThrowDialog(Sequence sequence)
	{
		if (sequence != null)
		{
			this.gameEvent.setParameter("sequence", sequence);
			Game.main.enqueueEvent(this.gameEvent);
		}
	}

	internal void SetButtonsSequences(Sequence accept, Sequence deny)
	{
		this.acceptSeq = accept;
		this.denySeq = deny;
	}
}
