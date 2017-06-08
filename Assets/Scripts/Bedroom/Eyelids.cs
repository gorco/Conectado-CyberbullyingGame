using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyelids : MonoBehaviour {

	private AnimationCurve curve;
	public GameObject block;

	public AnimationCurve curveWakeUp;
	public AnimationCurve curveGoToSleep;

	public UnityEngine.UI.Image topEyelid;
	public UnityEngine.UI.Image bottomEyelid;

	private float animationSeconds;

	private Vector2 topClosed;
	private Vector2 bottomClosed;
	private Vector2 topOpened;
	private Vector2 bottomOpened;

	private Vector2 start1;
	private Vector2 start2;

	private Vector2 goal1;
	private Vector2 goal2;

	private bool animate = false;

	private float delta;

	public bool startOpened = false;

	void Start () {
		topOpened = new Vector2(0, 1f);
		topClosed = new Vector2(0, 0.5f);

		bottomOpened = new Vector2(1, 0.0f);
		bottomClosed = new Vector2(1, 0.5f);

		if (startOpened)
		{
			topEyelid.rectTransform.anchorMin = topOpened;
			bottomEyelid.rectTransform.anchorMax = bottomOpened;

			topEyelid.rectTransform.sizeDelta = new Vector2(0, 0);
			bottomEyelid.rectTransform.sizeDelta = new Vector2(0, 0);
		}
	}
	
	void Update () {
		if (animate)
		{
			topEyelid.rectTransform.anchorMin = Vector3.Lerp(start1, goal1, curve.Evaluate(delta / animationSeconds));
			bottomEyelid.rectTransform.anchorMax = Vector3.Lerp(start2, goal2, curve.Evaluate(delta / animationSeconds));

			topEyelid.rectTransform.sizeDelta = new Vector2(0, 0);
			bottomEyelid.rectTransform.sizeDelta = new Vector2(0, 0);

			delta += Time.deltaTime;
			if (delta >= animationSeconds)
			{
				block.SetActive(false);
				animate = false;
			}
		}
	}

	public void wakeUp(float seconds)
	{
		curve = curveWakeUp;
		delta = 0;

		start1 = topClosed;
		start2 = bottomClosed;
		goal1 = topOpened;
		goal2 = bottomOpened;

		animate = true;
		animationSeconds = seconds;
	}

	public void goToSleep(float seconds)
	{
		block.SetActive(true);
		curve = curveGoToSleep;
		delta = 0;

		goal1 = topClosed;
		goal2 = bottomClosed;
		start1 = topOpened;
		start2 = bottomOpened;

		animate = true;
		animationSeconds = seconds;
	}

}
