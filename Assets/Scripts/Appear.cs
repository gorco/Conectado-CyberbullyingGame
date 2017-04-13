using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour {

	public bool appearSoon;
	public int edgeHour;
	public int edgeMinutes;

	public Sprite lateSprite;
	public Vector2 latePosition;

	// Use this for initialization
	void Start()
	{
		bool late = GlobalState.NowIsLaterThan(edgeHour, edgeMinutes);
		if(appearSoon && late || !appearSoon && !late)
		{
			if (lateSprite)
			{
				this.GetComponent<SpriteRenderer>().sprite = lateSprite;
				this.transform.localPosition = latePosition;
			}
			else
			{
				this.gameObject.SetActive(false);
			}
		} 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
