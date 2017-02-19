using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeSprite : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

	public Sprite newSprite;
	public bool switchObject = true;

	public Vector3 offset;

	public BoxCollider2D originalCollider;
	public BoxCollider2D newCollider;

	private bool original = true;

	// Use this for initialization
	void Start () {
		originalCollider.enabled = true;
		newCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeSprite()
	{
		Sprite sp = newSprite;
		if (switchObject)
		{
			originalCollider.enabled = !original;
			newCollider.enabled = original;

			newSprite = this.GetComponent<SpriteRenderer>().sprite;
			this.GetComponent<SpriteRenderer>().sprite = sp;

			int mult = original ? 1 : -1;
			this.transform.localPosition = this.transform.localPosition + mult * offset;
		}
		else if (original)
		{
			this.GetComponent<SpriteRenderer>().sprite = newSprite;
			originalCollider.enabled = !original;
			newCollider.enabled = original;
		}
		original = !original;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("ON POINTER CLICK");
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("ON POINTER DOWN");

	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Debug.Log("ON POINTER UP");

		changeSprite();
	}
}
