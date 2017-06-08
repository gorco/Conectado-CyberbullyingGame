using Isometra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

			object otherObj = ev.getParameter(SequenceGenerator.EVENT_OTHER_FIELD);
			string other = otherObj != null ? otherObj.ToString().Replace("\"", "") : "";

			StartCoroutine(ActiveObject(time, other));
		}
	}

	public override void Tick()
	{
		//throw new NotImplementedException();
	}

	private IEnumerator ActiveObject(float time, string other)
	{
		yield return new WaitForSeconds(time);

		if (gObj != null)
		{
			gObj.SetActive(true);
			ThrowDialog dialog = gObj.GetComponent<ThrowDialog>();
			if (dialog != null)
			{
				dialog.ThrowDialogNow();
			}
		}
		foreach(GameObject g in secundaryObjects)
		{
			g.SetActive(true);
		}

		Image img = gObj.GetComponent<Image>();
		if (img != null)
		{
			img.sprite = Resources.Load<Sprite>(other);
		}
	}
}