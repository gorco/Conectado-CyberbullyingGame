using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimationState : MonoBehaviour {

	public Animator anim;
	public string startState;
	public string[] states;
	public int threshold;
	public int minTimeBtw = 10;

	private int stateSelected;
	private int r;
	private float currentT;

	// Use this for initialization
	void Start () {
		anim.SetTrigger(startState);
		currentT = 0;
	}

	private void FixedUpdate()
	{
		r = Random.Range(0, 1000);
		stateSelected = Random.Range(0, 100)%states.Length;
		currentT += Time.deltaTime;

		if(currentT > minTimeBtw && r < threshold && anim.GetCurrentAnimatorStateInfo(0).IsName(startState))
		{
			anim.SetTrigger(states[stateSelected]);
			currentT = 0;
		}
	}
}
