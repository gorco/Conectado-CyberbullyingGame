using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialFriend : MonoBehaviour {

	public Text nameUser;
	public Text state;

	public Sprite acceptedBG; 

	public GameObject accept;
	public GameObject deny;
	public GameObject block;
	public GameObject avatar;

	private ComputerManager parent;

	private bool accepted;

	// Use this for initialization
	void Start () {
		accepted = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InitFriendRequest(string name, string state, ComputerManager parent)
	{
		this.parent = parent;
		this.nameUser.text = name;
		this.state.text = state;
	}

	public void AcceptFriend()
	{
		Resolve(true);
		this.GetComponent<Image>().sprite = acceptedBG;
		block.SetActive(true);
		state.gameObject.SetActive(true);
	}

	public void DenyFriend()
	{
		Resolve(false);
	}

	private void Resolve(bool accepted)
	{
		parent.ResolveFriendRequest(accepted, this.nameUser.text, this);
		accept.SetActive(false);
		deny.SetActive(false);
	}
}
