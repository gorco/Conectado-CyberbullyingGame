using RAGE.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameAndSetUserInfo : MonoBehaviour {

	public UnityEngine.UI.InputField userName;
	public UnityEngine.UI.InputField nick;
	public UnityEngine.UI.InputField userPass;
	public UnityEngine.UI.Toggle male;
	public UnityEngine.UI.Toggle female;
	public UnityEngine.UI.Toggle repeatedDays;

	public UnityEngine.UI.Text error;

	private bool sendTrace = true;

	private SaveState saver;

	private MobileChat mobileChat;
	private ComputerManager computer;

	public GameObject daysPanel;

	// Use this for initialization
	void Start () {
		if (mobileChat == null)
		{
			mobileChat = FindObjectsOfType<MobileChat>()[0];
		}
		if (computer == null)
		{
			computer = FindObjectsOfType<ComputerManager>()[0];
		}
		saver = new SaveState(mobileChat, computer);
		if (!System.IO.File.Exists("host.cfg")) {
			PlayerPrefs.SetInt("online", 0);
			sendTrace = false;
		}
		error.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private bool CheckName()
	{
		if (userName.text.Length > 10)
		{
			error.text = LanguageSelector.instance.GetName("nameLengthError");  //"Tu nombre no puede tener más de 10 caracteres";
			error.enabled = true;
			return false;
		}

		if (userName.text == "")
		{
			error.text = error.text = LanguageSelector.instance.GetName("nameError"); //"Hace falta tu nombre";
			error.enabled = true;
			return false;
		}

		return true;
	}

	private bool CheckUser()
	{
		if (nick.text.Contains(" "))
		{
			error.text = LanguageSelector.instance.GetName("spaceError");  //"El Usuario y la Contraseña no pueden contener espacios";
			error.enabled = true;
			return false;
		}

		if (nick.text.Length > 16)
		{
			error.text = LanguageSelector.instance.GetName("userpasswordLengthError");  //"Usuario y Contraseña no pueden contener más de 16 caracteres";
			error.enabled = true;
			return false;
		}

		if (nick.text == "")
		{
			error.text = error.text = LanguageSelector.instance.GetName("userError"); //"Hace falta un nombre de usuario";
			error.enabled = true;
			return false;
		}

		return true;

	}

	private bool CheckPass()
	{
		if (userPass.text.Contains(" "))
		{
			error.text = LanguageSelector.instance.GetName("spaceError");  //"El Usuario y la Contraseña no pueden contener espacios";
			error.enabled = true;
			return false;
		}

		if (userPass.text.Length > 16)
		{
			error.text = LanguageSelector.instance.GetName("userpasswordLengthError");  //"Usuario y Contraseña no pueden contener más de 16 caracteres";
			error.enabled = true;
			return false;
		}

		if (userPass.text == "")
		{
			error.text = error.text = LanguageSelector.instance.GetName("passwordError"); //"Hace falta una contraseña";
			error.enabled = true;
			return false;
		}

		return true;
	}

	private bool CheckGenre()
	{
		if (!male.isOn && !female.isOn)
		{
			error.text = LanguageSelector.instance.GetName("genderError");//"Hace falta que selecciones tu género";
			error.enabled = true;
			return false;
		}

		return true;
	}

	public void ContinueDay()
	{
		if (CheckUser())
		{
			int scene = saver.LoadDayByUsername(nick.text);
			if (scene >= 0)
			{
				if (sendTrace)
				{
					Tracker.T.setVar("gender", male.isOn ? "male" : "female");
					Tracker.T.setVar("pass", userPass.text);
					Tracker.T.accessible.Accessed("ContinueGame");
				}
				SceneManager.LoadScene(scene);
			} else
			{
				error.text = error.text = LanguageSelector.instance.GetName("loadError"); //"No se ha encontrado partida empezada para ese usuario";
				error.enabled = true;
			}
		} 
	}

	public void HideDaysPanel()
	{
		daysPanel.SetActive(false);
	}

	public void ShowDaysPanel()
	{
		if (!CheckName())
		{
			return;
		}

		if (!CheckPass())
		{
			return;
		}

		if (!CheckUser())
		{
			return;
		}

		if (!CheckGenre())
		{
			return;
		}
		error.text = "";
		daysPanel.SetActive(true);
	}

	public void ChooseDay(string day)
	{
		GlobalState.NotRepeatedDays = !repeatedDays.isOn;

		GlobalState.Repeated = false;
		GlobalState.MessagesPending = false;

		//Save data
		GlobalState.UserName = userName.text;
		GlobalState.Nick = nick.text;
		GlobalState.UserPass = userPass.text;
		GlobalState.MaleSex = male.isOn;

		string path = Application.dataPath + "/Resources/DefaultDays/StartDay" + day + "File.save";
		int scene = saver.LoadDay(path, nick.text);
		if (scene >= 0)
		{
			if (sendTrace)
			{
				Tracker.T.setVar("gender", male.isOn ? "male" : "female");
				Tracker.T.setVar("pass", userPass.text);
				Tracker.T.accessible.Accessed("StartGameAtDay" + day);
			}
			SceneManager.LoadScene(scene);
		}

	}

	public void StartGame()
	{
		if (userName.text == "" && userPass.text == "")
		{
			if(nick.text.StartsWith("test."))
			{
				String[] values = nick.text.Split('.');
				SceneManager.LoadScene(values[1]);
				GlobalState.Day = (int.Parse(values[2]));
				GlobalState.Hour = (int.Parse(values[3]));
				GlobalState.Minute = (int.Parse(values[4]));
				GlobalState.Repeated = false;

				GlobalState.UserName = "test";
				GlobalState.Nick = "test";
				GlobalState.UserPass = "test";
			}
		} else
		{
			
			if (!CheckName())
			{
				return;
			}

			if (!CheckPass())
			{
				return;
			}

			if (!CheckUser())
			{
				return;
			}

			if (!CheckGenre())
			{
				return;
			}

			GlobalState.NotRepeatedDays = !repeatedDays.isOn;
            //LanguageSelector.instance.GetCurrentLanguage() = "en_UK";

			//Init day
			GlobalState.Day = 0;
			GlobalState.Hour = 7;
			GlobalState.Minute = 0;
			GlobalState.Repeated = false;
			GlobalState.MessagesPending = false;

			//Save data
			GlobalState.UserName = userName.text;
			GlobalState.Nick = nick.text;
			GlobalState.UserPass = userPass.text;
			GlobalState.MaleSex = male.isOn;

			if (sendTrace)
			{
				Tracker.T.setVar("gender", male.isOn ? "male" : "female");
				Tracker.T.setVar("pass", userPass.text);
				Tracker.T.accessible.Accessed("StartGame");
			}

			//Friendship
			GlobalState.AlejandroFS = 50;
			GlobalState.GuillermoFS = 50;
			GlobalState.JoseFS = 50;
			GlobalState.AnaFS = 50;
			GlobalState.AlisonFS = 50;
			GlobalState.MariaFS = 50;
			GlobalState.TeacherFS = 50;
			GlobalState.ParentsFS = 50;

			//Quests
			GlobalState.MariaQuest = 0;
			GlobalState.AlisonQuest = 0;
			GlobalState.AnaQuest = 0;
			GlobalState.GuillermoQuest = 0;
			GlobalState.JoseQuest = 0;
			GlobalState.AlejandroQuest = 0;
			GlobalState.GumQuest = 0;
			GlobalState.BoardQuest = 0;
			GlobalState.ParentsMeetingQuest = 0;
			GlobalState.SharedPassQuest = 0;

			//Start game
			SceneManager.LoadScene("StartDay1");
		}
	}
}
