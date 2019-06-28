using Isometra;
using Isometra.Sequences;
using RAGE.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ComputerManager : EventManager {

	public GameObject screen;
	public GameObject screenSqueleton;

	public GameObject loginView;
	public GameObject publicationView;
	public ScrollRect publicationScroll;
	public GameObject friendsView;

	public Text publicUserName;
	public Image userAvatar;
	public Sprite avatarM;
	public Sprite avatarH;

	public Text userInput;
	public Text passInput;

	public RectTransform photosContent;
	public RectTransform friendsContent;

	public GameObject publicationPrefab;
	public GameObject friendPrefab;

	private Dictionary<string, Publication> publications;
	private List<SocialFriend> friends;

	private List<GameObject> tmpPublications;
	private List<GameObject> tmpComments;

	private string pass = "";

	public Text notePass;
	public Text noteUser;

	public GameObject error;

	public GameObject newFriends;
	public Text friendsCount;
	public Text friendsPending;

	private float friendsRequests;
	private float friendsNum;

	private Sequence defaultPublicationSequence;
	private Sequence publicationSequence;
	private Dictionary<string, Sequence> commentsSequences;

	private GameEvent gameEvent;

	private bool sendTraces = false;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("online") == 1)
		{
			sendTraces = true;
		}
		OffComputer();
		newFriends.SetActive(false);
		friendsRequests = 0;
		friends = new List<SocialFriend>();
		tmpPublications = new List<GameObject>();
		tmpComments = new List<GameObject>();
		publications = new Dictionary<string, Publication>();
		error.SetActive(false);
		ShowLogin();
	}

	private void Awake()
	{
		commentsSequences = new Dictionary<string, Sequence>();

		this.gameEvent = new GameEvent();
		this.gameEvent.Name = "start sequence";
	}
	// Update is called once per frame
	void Update () {
		if(friendsRequests > 0)
		{
			newFriends.SetActive(true);
		} else
		{
			newFriends.SetActive(false);
		}
	}

	public void AddPublicationComment(string keyPublication, string author, string comment)
	{
		Debug.LogWarning("añadir comentario a la publicación con KEY -> " + keyPublication);
		publications[keyPublication].AddComment(author, comment);
	}

	public void AddTmpComment(GameObject socialComment)
	{
		tmpComments.Add(socialComment);
	}

	public void ResolveFriendRequest(bool accepted, string name, SocialFriend friend)
	{
		friendsRequests--;
		friendsPending.text = ""+friendsRequests;
		if (accepted)
		{
			Interacted("Accepted_" + name + "_request");
			friendsNum++;
			friendsCount.text = "" + friendsNum;
		}
		else
		{
			Interacted("Deny_" + name + "_request");
			float y = friend.gameObject.GetComponent<RectTransform>().anchoredPosition.y;
			foreach(SocialFriend sf in friends)
			{
				RectTransform sfRect = sf.gameObject.GetComponent<RectTransform>();
				if(sfRect.anchoredPosition.y < y)
				{
					sfRect.anchoredPosition = new Vector2(sfRect.anchoredPosition.x, sfRect.anchoredPosition.y + sfRect.sizeDelta.y);
				}
			}
			friendsContent.sizeDelta = new Vector2(friendsContent.sizeDelta.x, friendsContent.sizeDelta.y - friend.gameObject.GetComponent<RectTransform>().sizeDelta.y);
			friends.Remove(friend);
			Destroy(friend.gameObject);
		}
	}

	public void AddFriendRequest(string name, string state, string globalKey = "", Sequence accept = null, Sequence deny = null)
	{
		friendsRequests++;
		friendsPending.text = "" + friendsRequests;

		float hight = 0;
		foreach (SocialFriend child in friendsContent.GetComponentsInChildren<SocialFriend>(false))
		{
			RectTransform rctT = child.GetComponent<RectTransform>();
			hight = rctT.sizeDelta.y;
			rctT.anchoredPosition = new Vector2(0, rctT.localPosition.y - hight);
		}

		GameObject friend = Instantiate(friendPrefab, friendsContent, false);
		SocialFriend sc = friend.GetComponent<SocialFriend>();
		sc.InitFriendRequest(name, state, this);
		sc.SetButtonsSequences(accept, deny);
		if (globalKey != "")
		{
			sc.SetGlobalVariable(globalKey);
		}
		friend.transform.localScale = new Vector3(1, 1, 1);
		friendsContent.sizeDelta = new Vector2(friendsContent.sizeDelta.x, friendsContent.sizeDelta.y + hight);

		friends.Add(sc);
	}

	public void NewPublication(string author, string title, string key, string photoPath = "")
	{
		bool auth = false;
		foreach(SocialFriend sc in friends)
		{
			if(sc.GetFriendName() == author)
			{
				auth = true;
				break;
			}
		}/*
		if (!auth)
		{
			Debug.LogWarning(author + "is not a friend, ignoring the new publication...");
			return;
		}*/
		float height = 0;
		foreach(Publication child in photosContent.GetComponentsInChildren<Publication>(false))
		{
			RectTransform rctT = child.GetComponent<RectTransform>();
			height = rctT.sizeDelta.y;
			rctT.anchoredPosition = new Vector2(0, rctT.localPosition.y - rctT.sizeDelta.y);
			
		}

		GameObject photo = Instantiate(publicationPrefab, photosContent, false);
		Publication pb = photo.GetComponent<Publication>();
		pb.SetAuthorAndComment(author, title);
		pb.SetKeyAndComputerManager(key, this);
		if (photoPath != "")
		{
			pb.SetPhoto(photoPath);
		}

		var avatarFile = author;
		if(author == "Tú" || author == "You")
		{
			avatarFile = GlobalState.MaleSex ? "Boy" : "Girl";
		}
		pb.SetAvatar(avatarFile + "Avatar");

		photo.transform.localScale = new Vector3(1, 1, 1);
		photosContent.sizeDelta = new Vector2(photosContent.sizeDelta.x, photosContent.sizeDelta.y + height + 50);

		Debug.LogWarning("añadir publicación con KEY -> "+ key + " From the user" + author);
		publications.Add(key, pb);
		tmpPublications.Add(photo);
	}

	public void ShowLogin()
	{
		Interacted("ShowComputerLogin");
		loginView.SetActive(true);
		publicationView.SetActive(false);
		friendsView.SetActive(false);
	}

	public void ShowPhotos()
	{
		Interacted("ShowComputerPublications");
		ScrollChatToTop();
		loginView.SetActive(false);
		publicationView.SetActive(true);
		friendsView.SetActive(false);
		ForceLayout();
	}

	public void ForceLayout()
	{
		foreach (Publication child in photosContent.GetComponentsInChildren<Publication>(false))
		{
			child.CommentsLayout();
		}		
	}

	public void ShowFriends()
	{
		Interacted("ShowComputerFriends");
		loginView.SetActive(false);
		publicationView.SetActive(false);
		friendsView.SetActive(true);
	}

	public void OffComputer()
	{
		Interacted("offComputer");
		screenSqueleton.SetActive(false);
		screen.SetActive(false);
	}

	public void OnComputer()
	{
		if (pass == "")
		{
			noteUser.text = GlobalState.Nick;
			notePass.text = GlobalState.UserPass;

			publicUserName.text = GlobalState.Nick;
			userAvatar.sprite = GlobalState.MaleSex ? avatarH : avatarM;
		}
		Interacted("onComputer");
		ShowLogin();
		screen.SetActive(true);
		screenSqueleton.SetActive(true);
	}

	public void ScrollChatToTop()
	{
		publicationScroll.normalizedPosition = new Vector2(0, 1);
	}

	public void CheckLogin()
	{
		userInput.text = "";
		passInput.text = "";
		if (userInput.text.ToLower() == GlobalState.Nick.ToLower() && passInput.text == GlobalState.UserPass)
		{
			error.SetActive(false);
			ShowPhotos();
		} else
		{
			error.SetActive(true);
			Debug.LogWarning("user " + GlobalState.Nick + " and pass " + GlobalState.UserPass);
		}
	}

	public override void ReceiveEvent(IGameEvent ev)
	{
		if(ev.Name.Replace("\"", "") == "computerON")
		{
			OnComputer();
		}
	}

	public void SetDefaultPublicationButtonSequence(Sequence sequence)
	{
		defaultPublicationSequence = sequence;
	}

	public void SetPublicationButtonSequence(Sequence sequence)
	{
		publicationSequence = sequence;
	}

	public void AddDefaultPublicationCommentSequence(Sequence sequence)
	{
		commentsSequences.Add("default", sequence);
	}

	public void AddPublicationCommentSequence(string publicationKey, Sequence sequence)
	{
		commentsSequences.Add(publicationKey, sequence);
	}

	public void RemovePublicationCommentSequence(string publicationKey)
	{
		commentsSequences.Remove(publicationKey);
	}

	public void ThrowMyPublication()
	{
		if (publicationSequence)
		{
			this.gameEvent.setParameter("sequence", publicationSequence);
			
		} else
		{
			this.gameEvent.setParameter("sequence", defaultPublicationSequence);
		}
		Game.main.enqueueEvent(this.gameEvent);
	}

	public void ThrowPublicationDialog(string publicationKey)
	{
		if (commentsSequences.ContainsKey(publicationKey))
		{
			this.gameEvent.setParameter("sequence", commentsSequences[publicationKey]);
			Game.main.enqueueEvent(this.gameEvent);
		} else if (commentsSequences.ContainsKey("default"))
		{
			this.gameEvent.setParameter("sequence", commentsSequences["default"]);
			Game.main.enqueueEvent(this.gameEvent);
		}
		
	}

	public override void Tick()
	{
	}

	public void SaveComputerState()
	{
		tmpComments.Clear();
		tmpPublications.Clear();
	}

	public void LoadComputerState()
	{
		foreach(Publication pub in photosContent.GetComponentsInChildren<Publication>(true))
		{
			pub.RemoveComments(tmpComments);
		}

		foreach (GameObject pub in tmpPublications)
		{
			Publication p = pub.GetComponent<Publication>();
			publications.Remove(p.GetPublicationKey());
			Destroy(pub);
		}
	}

	public void Interacted(string id)
	{
		if (sendTraces)
		{
			try
			{
				Tracker.T.trackedGameObject.Interacted(id, GameObjectTracker.TrackedGameObject.Item);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
		}
	}
}
