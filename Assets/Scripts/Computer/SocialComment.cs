using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialComment : MonoBehaviour {

	public Text author;
	public Text text;
	private RectTransform textTransform;

	[SerializeField]
	private Vector2 templateSizeDelta;
	public Text maxTextSize;

	private RectTransform myRect;

	public Text textPrefab;

	// Use this for initialization
	void Start () {
		textTransform = text.GetComponent<RectTransform>();
		myRect = this.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		myRect.localScale = new Vector2(1, 1);
		if (maxTextSize != null)
		{
			templateSizeDelta = maxTextSize.rectTransform.sizeDelta;
		}

		if (textTransform.sizeDelta.y < templateSizeDelta.y)
		{
			float difference = templateSizeDelta.y - textTransform.sizeDelta.y;
			textTransform.sizeDelta = templateSizeDelta;
			myRect.sizeDelta = new Vector2(0, myRect.sizeDelta.y + difference + author.GetComponent<RectTransform>().sizeDelta.y);
		}
	}

	public void SetAuthorAndComment(string author, string text)
	{
		maxTextSize = Instantiate(textPrefab, this.transform);
		maxTextSize.text = text;
		this.author.text = author;
		this.text.text = text;
	}
}
