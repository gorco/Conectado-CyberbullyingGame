using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerManager : MonoBehaviour {

	public GameObject loginView;
	public GameObject publicationView;
	public GameObject friendsView;

	public Text userInput;
	public Text passInput;

	public RectTransform photosContent;
	public RectTransform friendsContent;

	public GameObject publicationPrefab;
	public GameObject friendPrefab;

	private Dictionary<string, Publication> publications;
	private List<SocialFriend> friends;

	private string pass;

	public Text notePass;
	public Text noteUser;

	public GameObject error;

	public GameObject newFriends;
	public Text friendsCount;
	public Text friendsPending;

	private float friendsRequests;
	private float friendsNum;

	// Use this for initialization
	void Start () {
		newFriends.SetActive(false);
		friendsRequests = 0;
		friends = new List<SocialFriend>();
		publications = new Dictionary<string, Publication>();
		error.SetActive(false);
		ShowLogin();
		GlobalState.UserName = "Toni";
		noteUser.text = GlobalState.UserName.ToLower();
		string sub1 = noteUser.text.Substring(0, GlobalState.UserName.Length / 2);
		string sub2 = noteUser.text.Substring(GlobalState.UserName.Length / 2, GlobalState.UserName.Length / 2);
		pass = sub1 + Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 3).ToLower() + sub2;
		notePass.text = pass;
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
		publications[keyPublication].AddComment(author, comment);
	}

	public void ResolveFriendRequest(bool accepted, string name, SocialFriend friend)
	{
		friendsRequests--;
		friendsPending.text = ""+friendsRequests;
		if (accepted)
		{
			friendsNum++;
			friendsCount.text = "" + friendsNum;
		}
		else
		{
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

	public void AddFriendRequest(string name, string state)
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
		friend.transform.localScale = new Vector3(1, 1, 1);
		friendsContent.sizeDelta = new Vector2(friendsContent.sizeDelta.x, friendsContent.sizeDelta.y + hight);

		friends.Add(sc);
	}

	public void NewPublication(string author, string title, string key)
	{
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
		photo.transform.localScale = new Vector3(1, 1, 1);
		photosContent.sizeDelta = new Vector2(photosContent.sizeDelta.x, photosContent.sizeDelta.y + height + 50);

		publications.Add(key, pb);
	}

	public void ShowLogin()
	{
		loginView.SetActive(true);
		publicationView.SetActive(false);
		friendsView.SetActive(false);
	}

	public void ShowPhotos()
	{
		loginView.SetActive(false);
		publicationView.SetActive(true);
		friendsView.SetActive(false);
	}

	public void ShowFriends()
	{
		loginView.SetActive(false);
		publicationView.SetActive(false);
		friendsView.SetActive(true);
	}

	public void CheckLogin()
	{
		userInput.text = "";
		passInput.text = "";
		Debug.Log(userInput.text.ToLower());
		Debug.Log(GlobalState.UserName.ToLower());
		Debug.Log(passInput.text);
		Debug.Log(pass);
		if (userInput.text.ToLower() == GlobalState.UserName.ToLower() && passInput.text == pass)
		{
			error.SetActive(false);
			ShowPhotos();
		} else
		{
			error.SetActive(true);
		}
	}
}
