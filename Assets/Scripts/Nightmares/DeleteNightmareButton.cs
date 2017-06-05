using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteNightmareButton : MonoBehaviour, IPointerEnterHandler
{
	public Nightmare4Manager nightmare;
	private Vector3 targetPosition;
	private float speed = 100.0f;
	private float threshold = 0.5f;
	private int pointerEnterCount;

	void Start()
	{
		pointerEnterCount = 0;
		targetPosition = transform.localPosition;
	}

	void Update()
	{
		Vector3 direction = targetPosition - transform.localPosition;
		if (direction.magnitude > threshold)
		{
			transform.localPosition = transform.localPosition + direction * speed * Time.deltaTime;
		}
		else
		{
			// Without this game object jumps around target and never settles
			transform.localPosition = targetPosition;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		pointerEnterCount++;
		if (pointerEnterCount == 5)
		{
			pointerEnterCount = 0;

			Shake camShake = nightmare.dreamBackground.GetComponent<Shake>();
			if (!camShake.isShaking())
			{
				camShake.DoShake();
			}

			nightmare.dupCount++;
			nightmare.initDeleteButton();
		}

		targetPosition = new Vector3(nightmare.minX + UnityEngine.Random.value * nightmare.size.x,
			nightmare.minY + UnityEngine.Random.value * nightmare.size.y, 0f);
	}

}