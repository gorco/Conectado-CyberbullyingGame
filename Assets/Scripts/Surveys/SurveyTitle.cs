using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyTitle : MonoBehaviour {
	
	private void Start ()
	{
		Text title = this.gameObject.GetComponent<Text>();
		var schedule = Simva.SimvaManager.Instance.Schedule;
		var activity = schedule.Activities[schedule.Next];
		title.text = activity.Name;
	}
}
