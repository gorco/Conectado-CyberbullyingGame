using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowDialog : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

	public string fieldName;

	// Use this for initialization
	void Start () {
		
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
		ThrowDialogNow();
	}
}
