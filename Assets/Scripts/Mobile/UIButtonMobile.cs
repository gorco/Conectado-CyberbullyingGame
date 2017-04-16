using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonMobile : MonoBehaviour {

	public Text advertisementText;
	public GameObject advertisement;
	public MobileChat mobile;
	public bool initActive;

	// Use this for initialization
	void Start () {
		this.gameObject.SetActive(initActive);
	}
	
	// Update is called once per frame
	void Update () {
		int adv = mobile.GetAdvertisementNumber();
		if (adv > 0)
		{
			advertisement.SetActive(true);
			advertisementText.text = adv.ToString();
		} else
		{
			advertisement.SetActive(false);
		}
	}
}
