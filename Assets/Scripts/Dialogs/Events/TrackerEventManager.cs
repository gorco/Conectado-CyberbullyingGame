﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Isometra;
using System;
using Isometra.Sequences;
using UnityEngine.SceneManagement;
using Simva;
using Xasu.HighLevel;
using Xasu.Util;

public class TrackerEventManager : EventManager {

    private float lastSceneChangeTime;

	public override void ReceiveEvent(IGameEvent ev)
	{
		if (PlayerPrefs.GetInt("online") == 1 || SimvaManager.Instance.IsActive)
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

                            AlternativeTracker.Instance.Selected(questionID, response.Text, AlternativeTracker.AlternativeType.Dialog);
							break;

						case "show dialog fragment":
							Fragment dfragment = finished.getParameter("fragment") as Fragment;

							string character = dfragment.Character;
							string message = dfragment.Msg;
							string name = dfragment.Name;
                            CompletableTracker.Instance.Completed(character + " " + message, CompletableTracker.CompletableType.StoryNode);
							break;
					}
					break;
				case "show dialog fragment":
					Fragment dfragmentf = ev.getParameter("fragment") as Fragment;

					string characterf = dfragmentf.Character;
					string messagef = dfragmentf.Msg;
					string namef = dfragmentf.Name;
                    CompletableTracker.Instance.Initialized(characterf + " " + messagef, CompletableTracker.CompletableType.StoryNode);
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
                        AlternativeTracker.Instance.Unlocked(friend, value.ToString());
                        CompletableTracker.Instance.Progressed(friend, value);
					}
					break;

                case "change scene":
                    int scene = SceneManager.GetActiveScene().buildIndex;
                    string sceneName = GetSceneName(scene);
                    CompletableTracker.Instance.Completed("scene" + scene, Time.realtimeSinceStartup - lastSceneChangeTime)
                        .WithResultExtensions(GetStateExtensions())
                        .WithResultExtensions(new Dictionary<string, object>
                        {
                            {"https://w3id.org/xapi/seriousgames/extensions/progress", scene / 27f}
                        });
                    CompletableTracker.Instance.Completed(sceneName, CompletableTracker.CompletableType.StoryNode, Time.realtimeSinceStartup - lastSceneChangeTime);
                    lastSceneChangeTime = Time.realtimeSinceStartup;
                    break;

                case "change variable":
					object vVar = ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD);
					string var = null;
					if (vVar != null)
					{
						var = ((String)vVar).Replace("\"", "");
					}

					var valueVar = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);
                    ExtensionsPool.AddResultExtension("conectado://"+var, valueVar.ToString());
                    break;

				case "move camera":
					string key = ev.getParameter(SequenceGenerator.EVENT_KEY_FIELD).ToString().Replace("\"", "");
                    AccessibleTracker.Instance.Accessed(key)
                        .WithResultExtensions(GetStateExtensions());
                    break;

				case "pick":
					string pickVar = ((String)ev.getParameter(SequenceGenerator.EVENT_VARIABLE_FIELD)).Replace("\"", "");
					var pickValue = ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);
                    ExtensionsPool.AddResultExtension("conectado://" + pickVar, pickValue.ToString());
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

	public static Dictionary<string, object> GetStateExtensions()
	{
        return new Dictionary<string, object> {
            { "conectado://Final", GlobalState.Final},
            { "conectado://GameDay", GlobalState.Day},
            { "conectado://GameHour", GlobalState.Hour + ":" + (GlobalState.Minute < 10 ? "0" + GlobalState.Minute.ToString() : GlobalState.Minute.ToString())},

            { "conectado://MariaFriendship", GlobalState.MariaFS},
            { "conectado://AlisonFriendship", GlobalState.AlisonFS},
            { "conectado://AnaFriendship", GlobalState.AnaFS},
            { "conectado://GuillermoFriendship", GlobalState.GuillermoFS},
            { "conectado://JoseFriendship", GlobalState.JoseFS},
            { "conectado://AlejandroFriendship", GlobalState.AlejandroFS},
            { "conectado://ParentsFriendship", GlobalState.ParentsFS},
            { "conectado://TeacherFriendship", GlobalState.TeacherFS},
            { "conectado://RiskFriendship", GlobalState.Risk}
        };
    }

	public override void Tick()
	{

	}

}
