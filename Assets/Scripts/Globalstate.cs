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

	// Frienship
	public static int MariaFS { get; set; }
	public static int AlisonFS { get; set; }
	public static int AnaFS { get; set; }
	public static int GuillermoFS { get; set; }
	public static int JoseFS { get; set; }
	public static int AlejandroFS { get; set; }
	public static int ParentsFS { get; set; }
	public static int TeacherFS { get; set; }
	public static int Risk{ get { return 100 - ((MariaFS + AlisonFS + AnaFS + GuillermoFS + JoseFS + AlejandroFS) / 12) - ParentsFS/4 - TeacherFS/4 ; } }

	//QuestsPerDay

	public static int MariaQuest { get; set; }
	//Day1 -> Meet Maria [0 - no spoken] [1 - freak] [2 - friend]

	public static int AlisonQuest { get; set; }
	//Day1 -> Found earing [0 - not found] [1 - found]

	public static int AnaQuest { get; set; }
	//Day1 -> Meet Ana [0 - no spoken] [1 - spoken]

	public static int GuillermoQuest { get; set; }
	//Day1 -> Meet Guille [0 - no spoken] [1 - spoken] [2 - noteboard]

	public static int JoseQuest { get; set; }		
	//Day1 -> Meet Jose [0 - no spoken] [1 - spoken]

	public static int AlejandroQuest { get; set; }  
	//Day1 -> Meet Alex [0 - sorry] [1 - threat] [2 - bad]

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

	public int MariaFSNonStatic
	{
		get { return MariaFS; }
	}
	public int AlisonFSNonStatic
	{
		get { return AlisonFS; }
	}
	public int AnaFSNonStatic
	{
		get { return AnaFS; }
	}
	public int GuillermoFSNonStatic
	{
		get { return GuillermoFS; }
	}
	public int JoseFSNonStatic
	{
		get { return JoseFS; }
	}
	public int AlejandroFSNonStatic
	{
		get { return AlejandroFS; }
	}
	public int ParentsFSNonStatic
	{
		get { return ParentsFS; }
	}
	public int TeacherFSNonStatic
	{
		get { return TeacherFS; }
	}

	public int MariaQuestNonStatic
	{
		get { return MariaQuest; }
	}
	public int AlisonQuestNonStatic
	{
		get { return AlisonQuest; }
	}
	public int AnaQuestNonStatic
	{
		get { return AnaFS; }
	}
	public int GuillermoQuestNonStatic
	{
		get { return GuillermoQuest; }
	}
	public int JoseQuestNonStatic
	{
		get { return JoseFS; }
	}
	public int AlejandroQuestNonStatic
	{
		get { return AlejandroQuest; }
	}

	public static bool NowIsLaterThan(int hour, int min)
	{
		if(Hour > hour)
		{
			return true;
		} else if (hour == Hour && Minute > min)
		{
			return true;
		} else
		{

			return false;
		}
	}
}
