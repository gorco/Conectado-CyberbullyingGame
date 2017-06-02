using RAGE.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowDialog : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

	public string fieldName;
	public bool throwAtStart = false;

	// Use this for initialization
	void Start () {
		if (throwAtStart)
		{
			ThrowDialogNow();
		}
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
		GetComponentInParent<ObjectsWithDialogsManager>().startDialog(fieldName);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Tracker.T.setVar("GameDay", GlobalState.Day);
		Tracker.T.setVar("GameHour", GlobalState.Hour+":"+GlobalState.Minute);
		Tracker.T.setVar("IsRepeatedDay", GlobalState.Repeated.ToString());
		Tracker.T.setVar("MobileMessages", GlobalState.MessagesPending.ToString());
		Tracker.T.trackedGameObject.Interacted(fieldName);
		ThrowDialogNow();
	}
}
