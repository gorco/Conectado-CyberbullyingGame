using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComputer : MonoBehaviour {

	public ComputerManager computer;

	public string author;
	public string text;
	public string key;
	public string photo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Comment()
	{
		computer.AddPublicationComment(key, author, text);
	}

	public void Publish()
	{
		computer.NewPublication(author, text, key, photo);
	}

	public void AddFriendRequest()
	{
		computer.AddFriendRequest(author, text);
	}
}
