using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitWithGum : MonoBehaviour {

	public GameObject gum;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GumCollision()
	{
		gum.SetActive(true);
	}

	public bool gumIsActive()
	{
		return gum.activeInHierarchy;
	}
}
