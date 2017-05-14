using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InnerDoor : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

	public Sprite enterSprite;
	private Sprite exitSprite;

	private SpriteRenderer current;

	// Use this for initialization
	void Start () {
		current = GetComponent<SpriteRenderer>();
		exitSprite = current.sprite;
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
		current.sprite = enterSprite;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		current.sprite = exitSprite;
	}
}
