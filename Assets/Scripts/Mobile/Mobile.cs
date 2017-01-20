using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile : MonoBehaviour {

	public AnimationCurve curve;

	public float animationSeconds = 1;

	public GameObject eyelids;

	private Vector2 hidePosition;
	public Vector2 showPosition;

	private Vector2 goal;
	private Vector2 start;

	private float delta;

	private bool animate = false;

	// Use this for initialization
	void Start()
	{
		hidePosition = this.GetComponent<Transform>().position;
	}

	// Update is called once per frame
	void Update()
	{
		if (animate)
		{
			this.GetComponent<Transform>().position = Vector3.Lerp(start, goal, curve.Evaluate((delta) / animationSeconds));
			delta += Time.deltaTime;
			if (delta >= animationSeconds)
				animate = false;
		}

	}

	public void takeMobile()
	{
		goal = showPosition;
		start = hidePosition;
		delta = 0;
		animate = true;
	}

	public void hideMobile()
	{
		goal = hidePosition;
		start = showPosition;
		delta = 0;
		animate = true;
	}
}
