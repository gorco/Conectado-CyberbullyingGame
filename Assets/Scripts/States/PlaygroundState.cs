using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaygroundState : IState
{

	[SerializeField]
	private bool bellSounded = false;

	public bool BellSounded
	{
		get { return bellSounded; }
		set { bellSounded = value; }
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

