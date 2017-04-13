using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InnerDoor : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

	public float cameraX;
	public float cameraY;

	public float scrollMax;

	public Camera cam;

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

	public void OnPointerUp(PointerEventData eventData)
	{
		cam.transform.localPosition = new Vector3(cameraX, cameraY, cam.transform.localPosition.z);
		cam.GetComponent<CameraScroll>().scroll_max = scrollMax;
	}
}
