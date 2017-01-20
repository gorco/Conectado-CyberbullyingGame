using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceLauncher : EventManager {

	public Sequence sequence;

	// Use this for initialization
	void Start () {

		sequence = new Sequence();

		sequence.Root = sequence.createChild(new Dialog(new List<Fragment>()
		{
			new Fragment("Paco", "asdasdasdasdasd")
		}), 1);
		
		//var childinteresante = sequence.Root.Childs[0] = sequence.createChild(ge);

	}
	
	public void Clicked()
	{
		SequenceManager.LaunchSequence(sequence);
	}

	public override void ReceiveEvent(IGameEvent ev)
	{
		if(ev.Name == "throwball")
		{

		}
	}

	public override void Tick(){}
}
