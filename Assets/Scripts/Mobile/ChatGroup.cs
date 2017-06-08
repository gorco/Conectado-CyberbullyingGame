using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatGroup : MonoBehaviour {

	private MobileChat mobileChat;
	public Text nameChat;
	public Image image;
	public GameObject adventisement;
	public Text numberNews;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddAdvertisement()
	{
		adventisement.SetActive(true);
		numberNews.text = ""+(Int32.Parse(numberNews.text) + 1);
	}

	public void InitChat(string chatName, Image chatImage, MobileChat mobile)
	{
		if (chatImage != null)
		{
			image = chatImage;
		}
		nameChat.text = chatName;
		numberNews.text = "0";
		adventisement.SetActive(false);
		mobileChat = mobile;
	}

	public void OpenChat()
	{
		mobileChat.Interacted("open_"+nameChat.text);
		mobileChat.LoadChat(nameChat.text);
		numberNews.text = "0";
		adventisement.SetActive(false);
	}

	public int GetAdvertisementNumber()
	{
		return Int32.Parse(numberNews.text);
	}
}
