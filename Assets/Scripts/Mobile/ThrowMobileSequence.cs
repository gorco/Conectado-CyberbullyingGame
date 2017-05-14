using Isometra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ThrowMobileSequence : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

	public MobileChat mobile;
	public Text currentChat;

	private GameEvent gameEvent;

	// Use this for initialization
	void Start () {
		gameEvent = new GameEvent();
		this.gameEvent.Name = "start sequence";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerClick(PointerEventData eventData)
	{

	}

	public void OnPointerDown(PointerEventData eventData)
	{

	}

	public void ThrowDialogNow()
	{
		this.gameEvent.setParameter("sequence", mobile.GetChatSequence(currentChat.text));
		Game.main.enqueueEvent(this.gameEvent);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		ThrowDialogNow();
	}
}
