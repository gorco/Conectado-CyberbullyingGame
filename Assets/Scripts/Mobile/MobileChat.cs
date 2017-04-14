using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileChat : MonoBehaviour {

	GameObject lastBubble;
	GameObject lastChat;

	public GameObject chatList;

	public GameObject otherBubblePrefab;
	public GameObject myBubblePrefab;
	public GameObject chatPrefab;
	public ScrollRect chatScroll;

	public Text chatTitle;
	public Text textTemplate;
	public float firstBubbleY;
	public float firstChatY;
	public RectTransform bubbleContent;
	public RectTransform chatListContent;
	public float initContentSize = 200;
	public float offset;

	private string currentChat;

	private bool outPocket;

	Dictionary<string, List<GameObject>> chats;

	private bool animate;
	private float delta;
	private float animationSeconds;
	private Vector2 goal;
	private Vector2 start;
	private Vector3 hidePosition;
	public Vector3 showPosition;
	public AnimationCurve curve;


	// Use this for initialization
	void Start () {
		outPocket = false;
		hidePosition = this.GetComponent<Transform>().localPosition;
		chats = new Dictionary<string, List<GameObject>>();
		bubbleContent.sizeDelta = new Vector2(bubbleContent.sizeDelta.x, initContentSize);
	}
	
	public void AddMessage(string text, string from)
	{
		GameObject bubble;
		if (from == null || from == "") {
			bubble = Instantiate(myBubblePrefab, bubbleContent);
		} else
		{
			bubble = Instantiate(otherBubblePrefab);
		}
		TextBubble script = bubble.GetComponent<TextBubble>();
		script.textPrefab = textTemplate;
		script.CreateBubble(text, from);
		AddBubble(bubble);
	}

	private void AddBubble(GameObject bubble)
	{
		RectTransform rectTransform = bubble.GetComponent<RectTransform>();
		RectTransform lastTransform = null;
		if (lastBubble == null)
		{
			rectTransform.anchoredPosition = new Vector2(0, firstBubbleY);
		}
		else
		{
			lastTransform = lastBubble.GetComponent<RectTransform>();
			rectTransform.anchoredPosition = new Vector2(0, lastTransform.localPosition.y - lastTransform.sizeDelta.y - offset);
		}
		bubbleContent.sizeDelta = new Vector2(bubbleContent.sizeDelta.x, bubbleContent.sizeDelta.y + (lastTransform != null ? lastTransform.sizeDelta.y  : 0) + offset);

		chats[currentChat].Add(bubble);
		lastBubble = bubble;
		ScrollChatToBottom();
	}

	public void RemoveCurrentChat()
	{
		RemoveChat("");
	}

	public void RemoveChat(string chatKey)
	{
		if(chatKey == "")
		{
			chatKey = currentChat;
		}
		foreach(GameObject g in chats[chatKey])
		{
			Destroy(g);
		}
		chats.Remove(chatKey);
	}

	public void HideChat(string chat)
	{
		chatList.SetActive(true);

		if (chat == "")
		{
			chat = currentChat;
		}
		if (!chats.ContainsKey(chat))
		{
			chats.Add(chat, new List<GameObject>());
		}
		List<GameObject> list = chats[chat];
		foreach (GameObject g in list)
		{
			g.SetActive(false);
		}
		currentChat = "";
	}

	public void CreateChat(string name)
	{
		GameObject chat;
		if (name != null || name != "")
		{
			chat = Instantiate(chatPrefab, chatListContent);
			ChatGroup script = chat.GetComponent<ChatGroup>();
			script.InitChat(name, null, this);
			AddChat(chat);
		}	
	}

	public void AddChat(GameObject chat)
	{
		RectTransform rectTransform = chat.GetComponent<RectTransform>();
		RectTransform lastTransform = null;
		if (lastChat == null)
		{
			rectTransform.anchoredPosition = new Vector2(0, firstChatY);
		}
		else 
		{
			lastTransform = lastChat.GetComponent<RectTransform>();
			rectTransform.anchoredPosition = new Vector2(0, lastTransform.localPosition.y - lastTransform.sizeDelta.y);
		}
		//bubbleContent.sizeDelta = new Vector2(bubbleContent.sizeDelta.x, bubbleContent.sizeDelta.y + (lastTransform != null ? lastTransform.sizeDelta.y : 0) + offset);

		//chats[currentChat].Add(bubble);
		lastChat = chat;
		//ScrollChatToBottom();
	}

	public void LoadChat(string chat)
	{
		chatTitle.text = chat;
		currentChat = chat;
		bubbleContent.sizeDelta = new Vector2(bubbleContent.sizeDelta.x, initContentSize);
		RectTransform lastTransform = null;
		if (chats.ContainsKey(chat))
		{
			List<GameObject> list = chats[chat];
			foreach (GameObject g in list)
			{
				RectTransform rectTransform = g.GetComponent<RectTransform>();
				lastTransform = lastBubble.GetComponent<RectTransform>();
				rectTransform.parent = bubbleContent;
				bubbleContent.sizeDelta = new Vector2(bubbleContent.sizeDelta.x, bubbleContent.sizeDelta.y + rectTransform.sizeDelta.y + offset);
				g.SetActive(true);
				bubbleContent.sizeDelta = new Vector2(bubbleContent.sizeDelta.x, bubbleContent.sizeDelta.y + (lastTransform != null ? lastTransform.sizeDelta.y : 0) + offset);
				lastBubble = g;
			}
		} else
		{
			lastBubble = null;
			chats.Add(chat, new List<GameObject>());
		}
		ScrollChatToBottom();
		chatList.SetActive(false);
	}

	public void ScrollChatToTop()
	{
		chatScroll.normalizedPosition = new Vector2(0, 1);
	}

	public void ScrollChatToBottom()
	{
		chatScroll.normalizedPosition = new Vector2(0, 0);
	}

	void Update()
	{
		if (animate)
		{
			this.GetComponent<Transform>().localPosition = Vector3.Lerp(start, goal, curve.Evaluate((delta) / animationSeconds));
			delta += Time.deltaTime;
			if (delta >= animationSeconds)
			{
				animate = false;
			}
		}

	}

	public void SwitchMobilePosition(float seconds)
	{
		if(!outPocket && !animate)
		{
			takeMobile(seconds);
		} else if (outPocket && !animate)
		{
			hideMobile(seconds);
		}
	}

	public void takeMobile(float seconds)
	{
		goal = showPosition;
		start = hidePosition;
		animationSeconds = seconds;
		delta = 0;
		animate = true;
		outPocket = true;
	}

	public void hideMobile(float seconds)
	{

		goal = hidePosition;
		start = showPosition;
		animationSeconds = seconds;
		delta = 0;
		animate = true;
		outPocket = false;
	}
}
