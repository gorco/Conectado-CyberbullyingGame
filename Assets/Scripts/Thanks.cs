using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thanks : MonoBehaviour {

	public Vector2 start;
	public Vector2 end;
	public GameObject rewindIcon;
	public GameObject rewindIcon2;

	public float speed;

	private bool creditsEnd = false;

	// Use this for initialization
	void Start () {
		transform.localPosition = start;
	}
	
	// Update is called once per frame
	void Update () {
		if (!creditsEnd)
		{
			transform.localPosition = Vector2.MoveTowards(transform.localPosition, end, Time.deltaTime * speed);
			if (transform.localPosition.y >= (end.y - 10))
			{
				creditsEnd = true;
			}
		} else if (Input.GetKey("mouse 0")) 
		{
			transform.localPosition = Vector2.MoveTowards(transform.localPosition, start, Time.deltaTime * speed * 2);
			if (transform.localPosition.y <= (start.y + 100))
			{
				rewindIcon.SetActive(false);
				rewindIcon2.SetActive(false);
				creditsEnd = false;
			} else
			{
				rewindIcon.SetActive(true);
				rewindIcon2.SetActive(false);
			}
		}
		else if (Input.GetKey("mouse 1"))
		{

			transform.localPosition = Vector2.MoveTowards(transform.localPosition, end, Time.deltaTime * speed * 2);
			rewindIcon2.SetActive(true);
			rewindIcon.SetActive(false);
		}
		else
		{
			rewindIcon.SetActive(false);
			rewindIcon2.SetActive(false);
		}
	}
}
