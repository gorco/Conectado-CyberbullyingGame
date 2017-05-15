using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day3Part4State : IState
{
	[SerializeField]
	private bool chargingMobile = false;

	public bool ChargingMobile
	{
		get { return chargingMobile; }
		set { chargingMobile = value; }
	}
}


