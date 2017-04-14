using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBubble : MonoBehaviour {

	public string text;
	public string from;

	public MobileChat chat;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateChat()
	{
		chat.CreateChat(text);
	}

	public void WriteText()
	{
		chat.AddMessage(text, from);
	}
}
