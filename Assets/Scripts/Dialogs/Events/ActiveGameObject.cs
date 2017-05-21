using Isometra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGameObject : EventManager
{
	public float wait;
	public GameObject gObj;
	public GameObject[] secundaryObjects;

	/// <summary>
	/// Receive the pick event
	/// </summary>
	/// <param name="ev"></param>
	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name.Replace("\"", "") == "active object")
		{
			object timeObj = ev.getParameter(SequenceGenerator.EVENT_TIME_FIELD);
			float time = timeObj != null ? (float)timeObj : 0;

			StartCoroutine(ActiveObject(time));
		}
	}

	public override void Tick()
	{
		//throw new NotImplementedException();
	}

	private IEnumerator ActiveObject(float time)
	{
		yield return new WaitForSeconds(time);

		if (gObj != null)
		{
			gObj.SetActive(true);
		}
		foreach(GameObject g in secundaryObjects)
		{
			g.SetActive(true);
		}

		ThrowDialog dialog = gObj.GetComponent<ThrowDialog>();
		if(dialog != null)
		{
			dialog.ThrowDialogNow();
		}
	}
}