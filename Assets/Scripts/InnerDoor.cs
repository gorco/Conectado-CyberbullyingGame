using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDoor : MonoBehaviour {

	public float cameraX;
	public float cameraY;

	public float scrollMax;

	public Camera cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnMouseUp()
	{
		cam.transform.localPosition = new Vector3(cameraX, cameraY, cam.transform.localPosition.z);
		cam.GetComponent<CameraScroll>().scroll_max = scrollMax;
	}
}
