using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState
{

	public static int Day {	get; set; }

	public static int Hour { get; set; }

	public static int Minute { get; set; }

	public static bool Repeated { get; set; }

	public static bool MaleSex { get; set; }

	public static string UserName { get; set; }

	protected static GlobalState instance;
	public static GlobalState Instance {  get { return instance == null ? instance = new GlobalState() : instance; } }

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

	public bool MaleSexNonStatic
	{
		get { return MaleSex; }
	}

	public string UserNameNonStatic
	{
		get { return UserName; }
	}

	public static bool isLate(int hour, int min)
	{
		if(hour > Hour)
		{
			return true;
		} else if (hour == Hour && min > Minute)
		{
			return true;
		} else
		{

			return false;
		}
	}
}
