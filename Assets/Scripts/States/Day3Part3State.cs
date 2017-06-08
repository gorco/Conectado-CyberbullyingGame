using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day3Part3State : IState
{
	[SerializeField]
	private bool guilleSpoke = false;

	public bool GuilleSpoke
	{
		get { return guilleSpoke; }
		set { guilleSpoke = value; }
	}
}