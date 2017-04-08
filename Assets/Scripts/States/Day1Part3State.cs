using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1Part3State : IState
{

	[SerializeField]
	private bool alisonDialog = false;
	private bool earingFound = false;

	public bool AlisonDialog
	{
		get { return alisonDialog; }
		set { alisonDialog = value; }
	}

	public bool EaringFound
	{
		get { return earingFound; }
		set { earingFound = value; }
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}


