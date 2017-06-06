using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Isometra;
using System;
using Isometra.Sequences;
using RAGE.Analytics;
using RAGE.Analytics.Formats;
using UnityEngine.SceneManagement;

public class TrackerEventManager : EventManager {

	public override void ReceiveEvent(IGameEvent ev)
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
				int scene = (int)ev.getParameter(SequenceGenerator.EVENT_VALUE_FIELD);
				AddStateExtensions();
				Tracker.T.setProgress(scene / 27f);
				Tracker.T.completable.Completed("scene"+(scene - 1));
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

	public void AddStateExtensions()
	{
		Tracker.T.setVar("GameDay", GlobalState.Day);
		Tracker.T.setVar("GameHour", GlobalState.Hour + ":" + GlobalState.Minute);

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
