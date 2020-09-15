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
		if (Simva.SimvaExtension.Instance.IsActive && Input.GetKeyDown(KeyCode.Escape))
		{
			resetButton.SetActive(!resetButton.activeSelf);
		}
	}

	public void OnReset()
	{
		if (!Simva.SimvaExtension.Instance.IsActive)
		{
			return;
		}

		resetButton.SetActive(false);

		var gameplay = Simva.SimvaExtension.Instance.Schedule.Activities
			.Where(kv => kv.Value.Type == "gameplay")
			.Select(kv => kv.Key)
			.FirstOrDefault();

		Simva.SimvaExtension.Instance.API.Api.SetCompletion(gameplay, Simva.SimvaExtension.Instance.API.AuthorizationInfo.Username, false)
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
