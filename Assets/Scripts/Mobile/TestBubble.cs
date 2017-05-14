using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBubble : MonoBehaviour {

	public string text;
	public string from;
	public string chatName;

	public string variable;
	
	public MobileChat chat;

	private Type t = GlobalState.Instance.GetType();

	// Use this for initialization
	void Start () {
		GlobalState.AlejandroFS = 50;
		GlobalState.GuillermoFS = 50;
		GlobalState.JoseFS = 50;
		GlobalState.AnaFS = 50;
		GlobalState.AlisonFS = 50;
		GlobalState.MariaFS = 50;
		GlobalState.TeacherFS = 50;
		GlobalState.ParentsFS = 50;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IncreaseFriendShip(int value)
	{
		int lastValue = (int)t.GetProperty(variable).GetValue(GlobalState.Instance, null);
		t.GetProperty(variable).SetValue(GlobalState.Instance, lastValue + value, null);
	}

	public void DecreaseFriendShip(int value)
	{
		int lastValue = (int)t.GetProperty(variable).GetValue(GlobalState.Instance, null);
		t.GetProperty(variable).SetValue(GlobalState.Instance, lastValue - value, null);
	}

	public void CreateChat()
	{
		chat.CreateChat(chatName);
	}

	public void WriteText()
	{
		chat.AddMessage(text, from, chatName);
	}
}
