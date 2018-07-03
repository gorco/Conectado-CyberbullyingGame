using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickableObject : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	/*
	private Vector3 originPosition;
	private Quaternion originRotation;

	public bool shakeON = false;

	public float shake_decay = 0.002f;
	public float shake_intensity = .1f;

	private float temp_shake_intensity = 0;

	public enum GlowingPhase { GETTING_BIGGER, MOVING, GETTING_SMALLER, WAITING };
	public enum GlowingStyle { FLASH, FADE_AND_FLASH, FADE };

	public GlowingStyle style = GlowingStyle.FLASH;
	private GlowingPhase phase = GlowingPhase.WAITING;

	private bool continueShine;
	private bool shaking;

	Material shader;

	float size = 0f;
	float pos = 0f;
	float fade = 0f;

	public float speed = 1f;
	private float time_since_last_glow = 0f;

	private float flashduration = 2f;
	*/
	// Use this for initialization
	void Start()
	{
		/*
		continueShine = false;
		shader = this.GetComponent<Renderer>().material;
		if (style == GlowingStyle.FLASH)
			shader.SetColor("_Color", new Color(1, 1, 1, 1));
			*/

	}

	void Update()
	{
		/*
		if (shaking)
		{
			if (temp_shake_intensity > 0) {
				transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
				transform.rotation = new Quaternion(
					originRotation.x + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
					originRotation.y + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
					originRotation.z + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
					originRotation.w + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f);
				temp_shake_intensity -= shake_decay;
			}
			else
			{
				shaking = false;
			}
		}

		switch (phase)
		{
			case GlowingPhase.GETTING_BIGGER:
				size += Time.deltaTime * flashduration;

				if (style == GlowingStyle.FADE_AND_FLASH || style == GlowingStyle.FADE)
				{
					fade += Time.deltaTime / 2;
				}

				if (size >= 0.5f)
				{
					size = 0.5f;
					phase = GlowingPhase.MOVING;
				}
				break;
			case GlowingPhase.MOVING:
				pos += Time.deltaTime * flashduration;
				if (pos >= 1f)
				{
					pos = 1f;
					phase = GlowingPhase.GETTING_SMALLER;
				}
				break;
			case GlowingPhase.GETTING_SMALLER:
				size -= Time.deltaTime * flashduration;

				if (style == GlowingStyle.FADE_AND_FLASH || style == GlowingStyle.FADE)
				{
					fade -= Time.deltaTime / 2;
				}

				if (size <= 0f)
				{
					size = 0f;
					phase = GlowingPhase.WAITING;
				}
				break;
			case GlowingPhase.WAITING:
				pos = 0f;
				size = 0f;
				if (style == GlowingStyle.FADE_AND_FLASH || style == GlowingStyle.FADE)
				{
					fade = 0f;
				}
				
				/*
				time_since_last_glow += Time.deltaTime;
				if (time_since_last_glow > speed)
				{
					time_since_last_glow = 0f;
					if(continueShine)
						phase = GlowingPhase.GETTING_BIGGER;
				}

				break;
		}

		shader.SetFloat("_ShineLocation", pos);
		shader.SetFloat("_ShineWidth", size);

		if (style == GlowingStyle.FADE_AND_FLASH || style == GlowingStyle.FADE)
		{
			shader.SetColor("_Color", new Color(1, 1, 1, fade));
		}
		*/
	}


	private void Shake()
	{
		/*
		shaking = true;
		originPosition = transform.position;
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;
		*/

	}

	public void OnPointerClick(PointerEventData eventData)
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		/*
		if (!shaking && shakeON)
		{
			Shake();
		}
		phase = GlowingPhase.GETTING_BIGGER;
		continueShine = true;*/
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//continueShine = false;
	}
		
}
