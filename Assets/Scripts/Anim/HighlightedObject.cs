using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HighlightedObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	private Sprite original;
	public Sprite highlighted;
	public string textLabel;
	public GameObject textMesh;

	// Use this for initialization
	void Start()
	{
		original = this.GetComponent<SpriteRenderer>().sprite;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		textMesh.transform.localPosition = this.transform.localPosition;

		textMesh.GetComponent<TextMesh>().text = textLabel;
		textMesh.SetActive(true);
		this.GetComponent<SpriteRenderer>().sprite = highlighted;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		textMesh.SetActive(false);
		this.GetComponent<SpriteRenderer>().sprite = original;
	}
}
