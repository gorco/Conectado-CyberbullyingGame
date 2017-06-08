using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

	public string variableName;
	public RectTransform progressImage;
	public float maxRight;
	public Image progressBar;

	public Color initColor = Color.red;
	public Color endColor = Color.green;

	private Type t = GlobalState.Instance.GetType();

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void UpdateNow()
	{
		int value = (int)t.GetProperty(variableName).GetValue(GlobalState.Instance, null);
		//Debug.LogWarning("Update Bar Valor " + name + " --->" + t.GetProperty(variableName).GetValue(GlobalState.Instance, null));
		//Debug.LogWarning("Width Valor --->" + value * maxRight / 100 );
		progressImage.sizeDelta = new Vector2(value * maxRight / 100 , progressImage.sizeDelta.y);
		progressBar.color = Color.Lerp(initColor, endColor, value/100.0f);
	}
}
