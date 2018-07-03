
using UnityEngine;

public class Nightmare2Manager : NightmareManager
{
	private bool throwFinish = false;
	public GameObject portal;
	public GameObject portalCollider;
	public GameObject shadow;
	public float xShadow;

	public GameObject gumPrefab;

	private bool showPortal;

	private SitWithGum[] sits;
	private Collider2D spawn;

	public float spawnTime = 2;
	public float speed = 1;
	private float time = 0;

	// Use this for initialization
	void Start()
	{
		sits = GetComponentsInChildren<SitWithGum>( true );
		spawn = GetComponent<Collider2D>();

		xShadow = shadow.transform.localPosition.x;

		if (GlobalState.Repeated || GlobalState.NotRepeatedDays)
		{
			showPortal = false;
			GlobalState.Repeated = false;
		} else
		{
			showPortal = true;
		}
		InitMobileGUI.InitMobileGUIObject(false);

	}
	
	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;
		if (throwFinish)
		{
			foreach(GumObject gObj in this.transform.GetComponentsInChildren<GumObject>())
			{
				Object.Destroy(gObj.gameObject);
			}
		}

		if (CheckSits() == 0 && !throwFinish)
		{
			this.Invoke("ThrowFinisNightmareDialog", 2);
			throwFinish = true;
		} else if(time > spawnTime && xShadow != shadow.transform.localPosition.x)
		{
			var position = new Vector2(spawn.bounds.min.x + Random.value * spawn.bounds.size.x, spawn.bounds.min.y + Random.value * spawn.bounds.size.y);
			var g = Instantiate(gumPrefab, position, Quaternion.identity, this.transform);
			g.GetComponent<GumObject>().speed = speed;
			float sc = Random.Range(0.8f, 1.5f);
			g.transform.localScale = new Vector2(sc, sc);
			time = 0;
			if (speed < 4f) 
				speed *= 1.1f;
			if(spawnTime > 0.3f)
				spawnTime *= 0.9f;
		}
	}

	private void ThrowFinisNightmareDialog()
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
	}

	private float CheckSits()
	{
		int n = 0;
		foreach (SitWithGum s in sits)
		{
			if (s.gumIsActive())
				n++;
		}
		return sits.Length - n;
	}

	public override float HowLong()
	{
		time /= 2f;
		return time;
	}
}

