using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour {

	public GameObject notification;
	public UnityEngine.UI.Text numNot;
	public AnimationCurve curve;
	public GameObject piecesPrefab;

	private float delta = 0;
	private int num = 0;
	private GameObject pieces;

	// Use this for initialization
	void Start () {
		notification.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		num = (int)Mathf.Ceil(curve.Evaluate(delta/15)*110)-1;
		delta += Time.deltaTime;
	}

	private void FixedUpdate()
	{
		if (num > 0)
			notification.SetActive(true);
		numNot.text = num.ToString();
		if (num > 99)
		{

			Destroy(pieces);

			pieces = Instantiate(piecesPrefab, this.gameObject.transform);
			pieces.transform.localScale = Vector2.one;
			pieces.transform.localPosition = new Vector2(0, 0);
			notification.SetActive(false);
			pieces.SetActive(true);
			delta = -5;
			foreach (Rigidbody2D r in pieces.GetComponentsInChildren<Rigidbody2D>(true))
			{
				float x = Random.Range(-300, 300);
				float y = Random.Range(-300, 300);
				r.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
			}
		
		}
	}
}
