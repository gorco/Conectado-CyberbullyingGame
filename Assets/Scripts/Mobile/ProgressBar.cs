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
		Debug.LogWarning("Update Bar Valor " + name + " --->" + t.GetProperty(variableName).GetValue(GlobalState.Instance, null));
		Debug.LogWarning("Width Valor --->" + (int)t.GetProperty(variableName).GetValue(GlobalState.Instance, null) * maxRight / 100 );
		progressImage.sizeDelta = new Vector2((int)t.GetProperty(variableName).GetValue(GlobalState.Instance, null) * maxRight / 100 , progressImage.sizeDelta.y);
	}
}
