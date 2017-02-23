using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarTime
{

	public static int Day {	get; set; }

	public static int Hour { get; set; }

	public static int Minute { get; set; }

	public static bool Repeated { get; set; }

	protected static CalendarTime instance;
	public static CalendarTime Instance {  get { return instance == null ? instance = new CalendarTime() : instance; } }

	public int DayNonStatic
	{
		get { return Day; }
	}

	public int HourNonStatic
	{
		get { return Hour; }
	}

	public int MinuteNonStatic
	{
		get { return Minute; }
	}

	public bool RepeatedNonStatic
	{
		get { return Repeated; }
	}
}
