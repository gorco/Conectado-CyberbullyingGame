using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Isometra;
using System;
using Isometra.Sequences;
using RAGE.Analytics;
using RAGE.Analytics.Formats;

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
						var optionsName = finished.getParameter("name") as string;
						var optionsQuestion = finished.getParameter("message") as string;
						var optionList = finished.getParameter("options") as List<Option>;
						var optionchosen = (int)ev.getParameter("option");
						var response = optionList[optionchosen];

						Tracker.T.alternative.Selected(optionsName, response.Text, AlternativeTracker.Alternative.Dialog);

						break;
				}

				break;
		}
	}

	public override void Tick()
	{
		throw new NotImplementedException();
	}

}
