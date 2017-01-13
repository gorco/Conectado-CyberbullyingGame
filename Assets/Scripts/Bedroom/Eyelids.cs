using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyelids : MonoBehaviour {

	public AnimationCurve curve;
	public UnityEngine.UI.Image topEyelid;
	public UnityEngine.UI.Image bottomEyelid;

	public float animationSeconds = 1;

	private Vector2 start1;
	private Vector2 start2;
	private float delta;

	private Vector2 goal1;
	private Vector2 goal2;

	// Use this for initialization
	void Start () {
		start1 = topEyelid.rectTransform.anchoredPosition;
		start2 = bottomEyelid.rectTransform.anchoredPosition;
		goal1 = topEyelid.rectTransform.anchoredPosition + new Vector2(0, topEyelid.rectTransform.sizeDelta.y);
		goal2 = bottomEyelid.rectTransform.anchoredPosition - new Vector2(0, bottomEyelid.rectTransform.sizeDelta.y);
	}
	
	// Update is called once per frame
	void Update () {
		topEyelid.rectTransform.anchoredPosition = Vector3.Lerp(start1, goal1, curve.Evaluate(delta / animationSeconds));
		bottomEyelid.rectTransform.anchoredPosition = Vector3.Lerp(start2, goal2, curve.Evaluate(delta / animationSeconds));
		delta += Time.deltaTime;
	}
}
