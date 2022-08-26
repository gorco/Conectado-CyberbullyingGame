using Xasu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Xasu.HighLevel;

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
			    var interactedPromise = GameObjectTracker.Instance.Interacted(fieldName);
                interactedPromise.WithResultExtensions(new Dictionary<string, object>
                {
                    { "conectado://GameDay", GlobalState.Day },
                    { "conectado://GameHour", GlobalState.Hour + ":" + GlobalState.Minute },
                    { "conectado://IsRepeatedDay", GlobalState.Repeated.ToString() },
                    { "conectado://MobileMessages", GlobalState.MessagesPending.ToString() }
                });
			
			} catch (Exception e)
			{
				Debug.LogWarning(e);
			}
		}
		ThrowDialogNow();
	}
}
