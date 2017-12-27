using System;
using UnityEngine;
using UnityEngine.UI;

public class Nightmare4Manager : NightmareManager
{
	private bool throwFinish = false;

	public GameObject portal;
	public GameObject portalCollider;
	public GameObject shadow;
	private float xShadow;

	public Sprite[] sprites;
	public float scale;

	public float minX;
	public float minY;

	public GameObject deletePrefab;

	private bool showPortal;

	public float totalDreamTime = 20;
	private float remainingDreamTime;

	public GameObject dreamBackground;
	public int dupCount;

	public Vector2 size;

	private bool started = false;

	// Use this for initialization
	void Start()
	{
		dupCount = 0;
		remainingDreamTime = totalDreamTime;
		//initDeleteButton();

		xShadow = shadow.transform.localPosition.x;
		size = this.GetComponent<Collider2D>().bounds.size;

		if (GlobalState.Repeated || GlobalState.NotRepeatedDays)
		{
			showPortal = false;
			GlobalState.Repeated = false;
		}
		else
		{
			showPortal = true;
		}
		InitMobileGUI.InitMobileGUIObject(false);

	}

	// Update is called once per frame
	void Update()
	{
		if (shadow.transform.localPosition.x != xShadow)
		{
			remainingDreamTime -= Time.deltaTime;
			if (!started)
			{
				initDeleteButton();
				started = true;
			}
		}

		if (( remainingDreamTime < 0  || dupCount > 20 ) && !throwFinish)
		{
			if (showPortal)
			{
				portal.SetActive(true);
				portalCollider.SetActive(true);
			}
			else
			{
				this.GetComponent<ObjectsWithDialogsManager>().startDialog("finishNightmare");
			}
			throwFinish = true;
		}
		
	}

	public void initDeleteButton()
	{
		instantiateDeleteButton();
	}

	private void instantiateDeleteButton()
	{
		GameObject prefab = GameObject.Instantiate(deletePrefab, transform);
		prefab.transform.localScale = new Vector3(scale, scale, 1);
		prefab.GetComponent<SpriteRenderer>().sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];

		DeleteNightmareButton deleteButton = prefab.GetComponent<DeleteNightmareButton>();
		deleteButton.nightmare = this;

		prefab.transform.localPosition = new Vector3(minX + UnityEngine.Random.value * size.x,
			minY + UnityEngine.Random.value * size.y, 0f);

		Vector2 p = prefab.transform.localPosition;
	}

	public override float HowLong()
	{
		return 0;
	}
}

