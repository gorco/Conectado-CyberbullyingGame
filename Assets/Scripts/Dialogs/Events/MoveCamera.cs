using Isometra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : EventManager
{
	public string keyEvent;
	public Camera sceneCamera;
	public float minScroll = 0;
	public float maxScroll;
	public float x;
	public float y;
	public bool throwMyDialog = false;

	/// <summary>
	/// Receive the pick event
	/// </summary>
	/// <param name="ev"></param>
	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "move camera" &&
			ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD).ToString().Replace("\"", "") == keyEvent)
		{
			this.moveCamera();
		}
	}

	public override void Tick()
	{
		//throw new NotImplementedException();
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// move the camera
	/// </summary>
	private void moveCamera()
	{
		CameraScroll cam = sceneCamera.GetComponent<CameraScroll>();
		cam.scroll_min = minScroll;
		cam.scroll_max = maxScroll;
		sceneCamera.transform.localPosition = new Vector3(x, y, sceneCamera.transform.localPosition.z);

		if (throwMyDialog)
		{
			ThrowDialog t = this.gameObject.GetComponent<ThrowDialog>();
			if (t)
			{
				t.ThrowDialogNow();
			}
		}
	}
}
