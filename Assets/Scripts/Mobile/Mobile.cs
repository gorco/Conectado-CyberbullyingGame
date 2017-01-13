using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile : MonoBehaviour {

	public AnimationCurve curve;

	public float animationSeconds = 1;

	private Vector2 start;
	private float delta;
	public Vector2 goal;

	public float delay;

	// Use this for initialization
	void Start()
	{
		delta = 0;
		start = this.GetComponent<Transform>().position;
	}

	// Update is called once per frame
	void Update()
	{
		if (delta > delay)
			this.GetComponent<Transform>().position = Vector3.Lerp(start, goal, curve.Evaluate((delta-delay) / animationSeconds));

		delta += Time.deltaTime;
	}
}
