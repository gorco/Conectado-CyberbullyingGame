using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Isometra;
using System;
using Isometra.Sequences;
using UnityEngine.SceneManagement;
using RAGE.Analytics;

public class TrackerEventManager : EventManager {

	public override void ReceiveEvent(IGameEvent ev)
	{
		if (PlayerPrefs.GetInt("online") == 1)
		{
			switch (ev.Name)
			{
				case "event finished":
					var finished = ev.getParameter("event") as IGameEvent;
					switch (finished.Name)
					{
						case "show dialog options":
							var questionID = finished.getParameter("questionID") as string;
							//var optionsQuestion = finished.getParameter("message") as string;
							var optionList = finished.getParameter("options") as List<Option>;
							var optionchosen = (int)ev.getParameter("option");
							var response = optionList[optionchosen];

							Tracker.T.alternative.Selected(questionID, response.Text, AlternativeTracker.Alternative.Dialog);
							break;
						// OTHERS
					}
					break;

				case "change friendship":
					object vAux = ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD);
					string friend = null;
					if (vAux != null)
					{
						friend = ((String)vAux).Replace("\"", "");
					}

					int value = (int)ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);

					if (friend != null)
					{
						Tracker.T.alternative.Unlocked(friend, value.ToString());
						Tracker.T.completable.Progressed(friend, value);
					}
					break;

				case "change scene":
					int scene = SceneManager.GetActiveScene().buildIndex;
					string sceneName = GetSceneName(scene);
					AddStateExtensions();
					Tracker.T.setProgress(scene / 27f);
					Tracker.T.completable.Completed("scene" + scene);
					Tracker.T.completable.Completed(sceneName, CompletableTracker.Completable.StoryNode);
					break;

				case "change variable":
					object vVar = ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD);
					string var = null;
					if (vVar != null)
					{
						var = ((String)vVar).Replace("\"", "");
					}

					var valueVar = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);

					Tracker.T.setVar(var, valueVar.ToString());
					break;

				case "move camera":
					string key = ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD).ToString().Replace("\"", "");
					AddStateExtensions();
					Tracker.T.accessible.Accessed(key);
					break;

				case "pick":
					string pickVar = ((String)ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD)).Replace("\"", "");
					var pickValue = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);
					Tracker.T.setVar(pickVar, pickValue.ToString());
					break;
			}
		}
	}

	private string GetSceneName(int scene)
	{
		string s = "";
		switch (scene)
		{
			case 0: s = "Title"; break;
			case 1: s = "UserInfo"; break;
			case 2: s = "StartDay"; break;
			case 3: s = "Day1MorningHome"; break;
			case 4: s = "Day1MorningSchool"; break;
			case 5: s = "Day1BreakSchool"; break;
			case 6: s = "Day1AfternoonHome"; break;
			case 7: s = "Nightmare1"; break;
			case 8: s = "Day2MorningHome"; break;
			case 9: s = "Day2MorningSchool"; break;
			case 10: s = "Day2BreakSchool"; break;
			case 11: s = "Day2AfternoonHome"; break;
			case 12: s = "Nightmare2"; break;
			case 13: s = "Day3MorningHome"; break;
			case 14: s = "Day3MorningSchool"; break;
			case 15: s = "Day3BreakSchool"; break;
			case 16: s = "Day3AfternoonHome"; break;
			case 17: s = "Nightmare3"; break;
			case 18: s = "Day4MorningHome"; break;
			case 19: s = "Day4MorningSchool"; break;
			case 20: s = "Day4BreakSchool"; break;
			case 21: s = "Day4AfternoonHome"; break;
			case 22: s = "Nightmare4"; break;
			case 23: s = "Day5MorningHome"; break;
			case 24: s = "Day5MorningSchool"; break;
			case 25: s = "Day5BreakSchool"; break;
			case 26: s = "Day5AfternoonHome"; break;
			case 27: s = "End"; break;
			case 28: s = "Credits"; break;
		}
		return s;
	}

	public void AddStateExtensions()
	{
		Tracker.T.setVar("Final", GlobalState.Final);
		Tracker.T.setVar("GameDay", GlobalState.Day);
		Tracker.T.setVar("GameHour", GlobalState.Hour + ":" + (GlobalState.Minute < 10 ? "0"+GlobalState.Minute.ToString() : GlobalState.Minute.ToString()));

		Tracker.T.setVar("MariaFriendship", GlobalState.MariaFS);
		Tracker.T.setVar("AlisonFriendship", GlobalState.AlisonFS);
		Tracker.T.setVar("AnaFriendship", GlobalState.AnaFS);
		Tracker.T.setVar("GuillermoFriendship", GlobalState.GuillermoFS);
		Tracker.T.setVar("JoseFriendship", GlobalState.JoseFS);
		Tracker.T.setVar("AlejandroFriendship", GlobalState.AlejandroFS);
		Tracker.T.setVar("ParentsFriendship", GlobalState.ParentsFS);
		Tracker.T.setVar("TeacherFriendship", GlobalState.TeacherFS);
		Tracker.T.setVar("RiskFriendship", GlobalState.Risk);
	}

	public override void Tick()
	{

	}

}
