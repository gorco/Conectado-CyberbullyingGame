using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sit : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{

	public NightmareManager nightmareManager;

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
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		float time = nightmareManager.HowLong();
		SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
		Color color = sprite.color;
		sprite.color = new Color(color.r,  color.g, color.b, Mathf.SmoothStep(1, 0, time));
		Invoke("SetInactive", time);
	}

	private void SetInactive()
	{
		this.gameObject.SetActive(false);
	}
}
