using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1Part3State : IState
{

	[SerializeField]
	private bool alisonDialog = false;
	[SerializeField]
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
}


