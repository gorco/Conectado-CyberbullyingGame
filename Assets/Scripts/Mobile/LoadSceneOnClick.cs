using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	public void LoadScene (int level)
	{
		SceneManager.LoadScene(level);
	}

	public void LoadSceneIfCnfg(int level)
	{
		if (System.IO.File.Exists("host.cfg"))
		{
			SceneManager.LoadScene(level);
		}
	}

	public void LoadSceneIfNotCnfg(int level)
	{
		if (!System.IO.File.Exists("host.cfg"))
		{
			SceneManager.LoadScene(level);
		}
	}

}
