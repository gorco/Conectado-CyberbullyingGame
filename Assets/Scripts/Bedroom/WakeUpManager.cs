using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WakeUpManager : MonoBehaviour {

	public Mobile mobileObject;
	public Eyelids eyelidsObject;
	public CameraScroll cameraScroll;

	public float wakeUpSeconds = 6;
	public float sleepSeconds = 2;
	public float takeMobileSeconds = 2;
	public float hideMobileSeconds = 2;
	public float introTime = 5;

	private float offset = 1;

	private float dTime = 0;

	public UnityEngine.UI.Text introText;

	/// <summary>
	/// state = 0 --> Wake up
	/// state = 1 --> Take the mobile
	/// state = 2 --> Waiting
	/// state = 3 --> Hide the mobile
	/// state = 4 --> Go to sleep
	/// state = 5 --> Change Scene
	/// </summary>
	private int state;

	// Use this for initialization
	void Start () {
		cameraScroll.disableScroll(true);
		state = 0;
		Debug.Log(CalendarTime.Day + "  -  " + CalendarTime.Repeated);
		if (!CalendarTime.Repeated) {
			if (CalendarTime.Day == 0)
			{
				introText.enabled = true;
				offset = introTime;
			}
			CalendarTime.Hour = 7;
			CalendarTime.Minute = 30;
		} else {
			introText.enabled = false;
		}
		mobileObject.updateHour();

	}
	
	// Update is called once per frame
	void Update () {
		//Wake up (Move eyelids)
		if (state == 0 && dTime >= offset) {
			introText.enabled = false;
			state = 1;
			eyelidsObject.wakeUp (wakeUpSeconds);
			dTime = 0;
		}
		//Take the mobile
		else if (state == 1 && dTime >= wakeUpSeconds)
		{
			state = 2;
			mobileObject.takeMobile(takeMobileSeconds);
			dTime = 0;
		}
		//Wait a mobile interaction
		else if (state == 2 && dTime > takeMobileSeconds)
		{
			if (mobileObject.isSounding() && !cameraScroll.isDisabled())
			{
				cameraScroll.disableScroll(false);
			} else if (!mobileObject.isSounding()) {
				state = 3;
				dTime = 0;
			}
		}
		//Hide mobile
		else if (state == 3) {
			cameraScroll.disableScroll(true);
			state = 4;
			mobileObject.hideMobile(hideMobileSeconds);
			dTime = 0;
		}
		//Check if wake up or go to sleep
		else if (state == 4 && dTime >= hideMobileSeconds)
		{
			if (!mobileObject.isAlarmDelayed())
			{
				//TODO si es dia repetido levantarse de todas formas... decir que se llega tarde
				//change hour
				//TODO
				//Wake up, load the bedroom scene
				SceneManager.LoadScene(2);
			} else
			{
				eyelidsObject.goToSleep(sleepSeconds);
				CalendarTime.Hour = 8;
				CalendarTime.Minute = 5;
				mobileObject.updateHour();
			}
			state = 5;
			dTime = 0;
		}
		//Go to sleep
		else if (state == 5 && dTime > sleepSeconds)
		{
			CalendarTime.Repeated = true;
			//change hour
			//TODO
			//Go to sleep and load the same scene
			SceneManager.LoadScene(1);
		}
		dTime += Time.deltaTime;
	}
}
