using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GumObject : MonoBehaviour, IPointerUpHandler, IPointerClickHandler, IPointerDownHandler
{

	Rigidbody2D rigid;
	public float speed = 1;

	// Use this for initialization
	void Start () {

		rigid = GetComponent<Rigidbody2D>();

		rigid.AddForce(new Vector2(0, -100* UnityEngine.Random.Range(0.5f, speed)));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Collision")
		{
			collision.GetComponent<SitWithGum>().GumCollision();
			Destroy(this.gameObject);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Destroy(this.gameObject);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}
}
