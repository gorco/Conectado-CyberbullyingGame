using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Mobile : MonoBehaviour {

	public AnimationCurve curve;

	private Vector2 hidePosition;
	public Vector2 showPosition;

	public Text hour;
	public Text alarm;
	public Text day;



	public GameObject wakeUpPanel;
	public BoxCollider2D selector;

	public string fileName;
//	public TextAsset jsonFile;

	private Vector2 goal;
	private Vector2 start;

	private float delta;

	private bool animate = false;
	private float animationSeconds;

	private bool sound = true;
	private bool delayed;

 //   private TextAsset daysName;
	//private string[] days;
	// Use this for initialization
	void Start()
	{


        //daysName = (TextAsset)Resources.Load("Assets/Texts/" +
        //LanguageSelector.instance.GetCurrentLanguage() + "/mobileProperties.txt", typeof(TextAsset));

        //days = daysName.text.Split(',');
        hidePosition = this.GetComponent<Transform>().localPosition;
		wakeUpPanel.SetActive(false);

		/*
		StreamReader sr = new StreamReader(Application.dataPath + "/Texts/" + textFile);
		string fileContents = sr.ReadToEnd();
		sr.Close();*/
		//string fileContents = jsonFile.text;

		wakeUpPanel.GetComponentInChildren<Text>().text = LanguageSelector.instance.GetName("sleepMsn");
	}

	// Update is called once per frame
	void Update()
	{
		if (animate)
		{
			this.GetComponent<Transform>().localPosition = Vector3.Lerp(start, goal, curve.Evaluate((delta) / animationSeconds));
			delta += Time.deltaTime;
			if (delta >= animationSeconds)
			{
				animate = false;
				selector.enabled = true;
			}
		}

	}

	public void takeMobile(float seconds)
	{
		goal = showPosition;
		start = hidePosition;
		animationSeconds = seconds;
		delta = 0;
		animate = true;
	}

	public void hideMobile(float seconds)
	{

		goal = hidePosition;
		start = showPosition;
		animationSeconds = seconds;
		delta = 0;
		animate = true;
	}

	internal void stopAlarm(bool stop)
	{
		if(!stop && (GlobalState.Hour >= 8 || GlobalState.Day == 4))
		{
			wakeUpPanel.GetComponentInChildren<Text>().text = LanguageSelector.instance.GetName("sleepMsn");
			wakeUpPanel.SetActive(true);
		} else
		{
			this.delayed = !stop;
			sound = false;
			wakeUpPanel.SetActive(false);
		}
	}

	public bool isSounding()
	{
		return sound;
	}

	public bool isAlarmDelayed()
	{
		return delayed;
	}

	public void setHour(int hour, int minute)
	{
		this.hour.text = "";
		if (hour < 10)
		{
			this.hour.text = "0";
		}
		this.hour.text += hour + ":";
		if(minute < 10)
		{
			this.hour.text += "0";
		}
		this.hour.text += minute;
	}

	public void updateHour()
	{
        alarm.text = LanguageSelector.instance.GetName("alarm");
		setHour(GlobalState.Hour, GlobalState.Minute);
		this.day.text = LanguageSelector.instance.GetName(GlobalState.Day.ToString()) +
            ", " + LanguageSelector.instance.GetName("day") + " " + (GlobalState.Day+1).ToString();
	}

//	private void OnValidate()
//	{
//#if UNITY_EDITOR
//		if (jsonFile == null && !string.IsNullOrEmpty(fileName))
//		{
//			jsonFile = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Texts/" + fileName);
//			Debug.Log("JSON FILE Setted: " + fileName);
//		}
//#endif
//	}

}
