using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetApp : MonoBehaviour {

	private ProgressBar[] progressBars;
	public GameObject barsParent;
	// Use this for initialization
	void Start () {
		progressBars = barsParent.transform.GetComponentsInChildren<ProgressBar>(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateBars()
	{
		if (progressBars == null)
		{
			progressBars = barsParent.transform.GetComponentsInChildren<ProgressBar>(true);
		}
		foreach( ProgressBar bar in progressBars)
		{
			bar.UpdateNow();
		}
	}
}
