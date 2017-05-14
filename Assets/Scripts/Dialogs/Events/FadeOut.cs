using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : EventManager
{
	public float duration;

	public string keyEvent;

	private bool fade = false;
	private SpriteRenderer[] sprites;
	private float dTime = 0;

	/// <summary>
	/// Receive the pick event
	/// </summary>
	/// <param name="ev"></param>
	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "fade out" && (keyEvent == null || keyEvent == "" ||
			((String)ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD)).Replace("\"", "") == keyEvent))
		{
			this.FadeOutObject();
		}
	}

	public override void Tick()
	{
		//throw new NotImplementedException();
	}

	// Use this for initialization
	void Start()
	{
		sprites = this.gameObject.GetComponentsInChildren<SpriteRenderer>(true);
	}

	// Update is called once per frame
	void Update()
	{
		if (fade)
		{
			float t = dTime / duration;
			foreach (SpriteRenderer sprite in sprites)
			{
				Color c = sprite.color;
				sprite.color = new Color(c.r, c.g, c.b, Mathf.SmoothStep(1, 0, t));
			}
			dTime += Time.deltaTime;
		}
	}

	/// <summary>
	/// Fade out 
	/// </summary>
	private void FadeOutObject()
	{
		ThrowDialog t = this.gameObject.GetComponent<ThrowDialog>();
		if (t)
		{
			t.enabled = false;
		}
		Invoke("OffCollider", duration);
		fade = true;
	}

	private void OffCollider()
	{
		this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}
}
