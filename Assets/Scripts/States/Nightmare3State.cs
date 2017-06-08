using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare3State : IState
{
	[SerializeField]
	private int talk = 0;

	public int Talk
	{
		get { return talk; }
		set { talk++; }
	}

	private void Start()
	{
		talk = 0;
	}
}

