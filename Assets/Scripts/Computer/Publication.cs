using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Publication : MonoBehaviour {

	public Text author;
	public Text comment;
	public Text likesText;
	public Text commentsNumber;
	public Image photo;
	public Image avatar;

	private float commentN;

	private float likes;

	public RectTransform commentsContent;
	public GameObject commentPrefab;

	public Text textTemplate;
	private RectTransform rectTextTemplate;

	private ComputerManager computer;
	private string key;

	// Use this for initialization
	void Start () {
		likes = 0;
		commentN = 0;
		rectTextTemplate = textTemplate.GetComponent<RectTransform>(); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddLikes(float likes = 1)
	{
		this.likes += likes;
		likesText.text = ""+this.likes;
	}

	public void SetKeyAndComputerManager(string key, ComputerManager computer)
	{
		this.key = key;
		this.computer = computer;
	}

	public string GetPublicationKey()
	{
		return this.key;
	}

	public void SetAuthorAndComment(string author, string comment)
	{
		this.author.text = author;
		this.comment.text = comment;
	}

	public void SetAvatar(string avatarName)
	{
		this.avatar.sprite = Resources.Load<Sprite>(avatarName);
	}

	public void SetPhoto(string photoName)
	{
		this.photo.sprite = Resources.Load<Sprite>(photoName);
	}

	public void AddComment(string from, string text)
	{
		commentN++;
		commentsNumber.text = "" + commentN;
		float height = 0;
		float last = 0;
		foreach (SocialComment child in commentsContent.GetComponentsInChildren<SocialComment>(false))
		{
			RectTransform rctT = child.GetComponent<RectTransform>();
			height += rctT.sizeDelta.y;
			last = rctT.sizeDelta.y;
		}

		GameObject comment = Instantiate(commentPrefab, commentsContent, false);
		SocialComment sc = comment.GetComponent<SocialComment>();
		sc.textPrefab = textTemplate;
		sc.SetAuthorAndComment(from, text);
		comment.transform.localScale = new Vector3(1, 1, 1);
		commentsContent.sizeDelta = new Vector2(commentsContent.sizeDelta.x, commentsContent.sizeDelta.y + last);
		comment.transform.localPosition = new Vector2(comment.transform.localPosition.x, comment.transform.localPosition.y - height);
		computer.AddTmpComment(comment);
	}

	public void ThrowCommentDialog()
	{
		computer.ThrowPublicationDialog(this.key);
	}

	private void FixedUpdate()
	{
		CommentsLayout();
	}

	internal void CommentsLayout()
	{
		float height = 0;
		foreach (SocialComment child in commentsContent.GetComponentsInChildren<SocialComment>(false))
		{
			RectTransform rctT = child.GetComponent<RectTransform>();
			child.transform.localPosition = new Vector2(child.transform.localPosition.x, - height);
			height += rctT.sizeDelta.y;
		}
	}

	internal void RemoveComments(List<GameObject> tmpComments)
	{
		foreach(SocialComment sc in commentsContent.GetComponentsInChildren<SocialComment>(true))
		{
			if (tmpComments.Contains(sc.gameObject))
			{
				tmpComments.Remove(sc.gameObject);
				Destroy(sc.gameObject);
			}
		}
	}
}
