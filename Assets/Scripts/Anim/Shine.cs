using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shine : MonoBehaviour
{

	public float maxSize;
	public float minSize;

	private float timer = 0;

	private bool grow;

	// Use this for initialization
	void Start()
	{
		//transform.localScale.
	}

	// Update is called once per frame
	void Update()
	{
		transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);

		if (grow)
		{
			transform.localScale += new Vector3(1, 1, 0) * Time.deltaTime;
		} else
		{ 
			transform.localScale -= new Vector3(1, 1, 0) * Time.deltaTime;
		}

	    if (maxSize < transform.localScale.x) { 
			grow = false;
		}

		if (minSize > transform.localScale.x)
		{
			grow = true;
		}

		timer += Time.deltaTime;

	}
}
