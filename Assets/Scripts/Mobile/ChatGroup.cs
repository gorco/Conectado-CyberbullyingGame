using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatGroup : MonoBehaviour {

	private MobileChat mobileChat;
	public Text nameChat;
	public Image image;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InitChat(string chatName, Image chatImage, MobileChat mobile)
	{
		if (chatImage != null)
		{
			image = chatImage;
		}
		nameChat.text = chatName;
		mobileChat = mobile;
	}

	public void OpenChat()
	{
		mobileChat.LoadChat(nameChat.text);
	}
}
