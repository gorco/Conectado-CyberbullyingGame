using UnityEngine;
using System.Collections;

public class CameraScroll : MonoBehaviour
{
	public float init_scroll_range = 0.05f;
	public float scroll_max;
	public float scroll_min;
	public float desp;

	private bool scroll = true;

	private float initY;
	private float initZ; 

	// Use this for initialization
	void Start()
	{
		initY = this.transform.localPosition.y;
		initZ = this.transform.localPosition.z;
	}

	// Update is called once per frame
	void Update()
	{
		if (scroll)
		{
			if (Input.mousePosition.x > (Screen.width * (1f - init_scroll_range)))
			{
				Scroll(false);
			}
			else if (Input.mousePosition.x < (Screen.width * (init_scroll_range)))
			{
				Scroll(true);
			}
		}
	}

	void Scroll(bool position)
	{
		if (position)
		{
			if (this.transform.localPosition.x >= (scroll_min + desp))
				this.transform.localPosition = new Vector3(this.transform.localPosition.x - desp, initY, initZ);
		}
		else
		{
			if (this.transform.localPosition.x <= (scroll_max - desp))
				this.transform.localPosition = new Vector3(this.transform.localPosition.x + desp, initY, initZ);
		}
	}

	public void disableScroll(bool value)
	{
		this.scroll = !value;
	}

	public bool isDisabled()
	{
		return this.scroll;
	}
}