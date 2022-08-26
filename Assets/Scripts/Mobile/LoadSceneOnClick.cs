using Simva;
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
        if(level == 31)
        {
            var simvaPlugin = GameObject.FindObjectOfType<Simva.SimvaPlugin>();
            if (simvaPlugin)
            {
                DestroyImmediate(simvaPlugin.gameObject);
                SimvaManager.Instance.Bridge = null;
            }
        }
        SceneManager.LoadScene(level);
	}

    public void LoadSceneIfCnfg(int level)
	{
		if (System.IO.File.Exists("host.cfg"))
		{
			LoadScene(level);
		}
	}

	public void LoadSceneIfNotCnfg(int level)
	{
		if (!System.IO.File.Exists("host.cfg"))
		{
			LoadScene(level);
		}
	}

}
