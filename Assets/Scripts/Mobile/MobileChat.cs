using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileChat : MonoBehaviour {

	GameObject lastBubble;

	public GameObject otherBubblePrefab;
	public GameObject myBubblePrefab;

	public Text textTemplate;
	public float initY;
	public RectTransform content;
	public float offset;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void AddMessage(string text, string from)
	{
		GameObject bubble;
		if (from == null || from == "") {
			bubble = Instantiate(myBubblePrefab);
		} else
		{
			bubble = Instantiate(otherBubblePrefab);
		}
		TextBubble script = bubble.GetComponent<TextBubble>();
		script.maxTextSize = textTemplate;
		script.CreateBubble(text, from);
		RectTransform rectTransform = bubble.GetComponent<RectTransform>();
		rectTransform.parent = content;
		content.sizeDelta = new Vector2(content.sizeDelta.x, content.sizeDelta.y + rectTransform.sizeDelta.y + offset);
		if (lastBubble == null) {
			rectTransform.anchoredPosition = new Vector2(0, initY);
		} else
		{
			RectTransform lastTransform = lastBubble.GetComponent<RectTransform>();
			rectTransform.anchoredPosition = new Vector2(0, lastTransform.anchoredPosition.y - lastTransform.sizeDelta.y + offset);
		}
		lastBubble = bubble;
	}
}
