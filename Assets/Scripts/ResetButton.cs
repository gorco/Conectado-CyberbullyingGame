using Simva;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityFx.Async.Promises;

public class ResetButton : MonoBehaviour
{
	public GameObject resetButton;
	
    void Start()
	{
		resetButton.SetActive(false);
	}
	
    void Update()
    {
		if (SimvaManager.Instance && SimvaManager.Instance.IsActive && Input.GetKeyDown(KeyCode.Escape))
		{
			resetButton.SetActive(!resetButton.activeSelf);
		}
	}

	public void OnReset()
	{
		if (!SimvaManager.Instance.IsActive)
		{
			return;
		}

		resetButton.SetActive(false);

		var gameplay = SimvaManager.Instance.Schedule.Activities
			.Where(kv => kv.Value.Type == "gameplay")
			.Select(kv => kv.Key)
			.FirstOrDefault();

        SimvaManager.Instance.API.Api.SetCompletion(gameplay, SimvaManager.Instance.API.Authorization.Agent.name, false)
			.Then(() => 
			{
				SceneManager.LoadScene("Title");
			})
			.Catch(e =>
			{
				Debug.Log("Exception resetting: " + e);
			});
	}
}
