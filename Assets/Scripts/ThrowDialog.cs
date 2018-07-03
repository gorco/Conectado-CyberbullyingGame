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

	private bool sendTrace = false;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("online") == 1)
		{
			sendTrace = true;
		}
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
		//Debug.LogError("Dialog Started");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (sendTrace)
		{
			try
			{
			
					Tracker.T.setVar("GameDay", GlobalState.Day);
					Tracker.T.setVar("GameHour", GlobalState.Hour + ":" + GlobalState.Minute);
					Tracker.T.setVar("IsRepeatedDay", GlobalState.Repeated.ToString());
					Tracker.T.setVar("MobileMessages", GlobalState.MessagesPending.ToString());
					Tracker.T.trackedGameObject.Interacted(fieldName);
			
			} catch (Exception e)
			{
				Debug.LogWarning(e);
			}
		}
		ThrowDialogNow();
	}
}
