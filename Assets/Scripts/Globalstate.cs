using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState
{

    public static string Language { get; set; }

	public static int Day {	get; set; }

	public static int Hour { get; set; }

	public static int Minute { get; set; }

	public static bool Repeated { get; set; }

	public static bool MaleSex { get; set; }

	public static bool MessagesPending { get; set; }

	public static string UserName { get; set; }

	public static string Nick { get; set; }

	public static string UserPass { get; set; }

	public static bool NotRepeatedDays { get; set; }

	// SocialNet
	public static int MariaNet { get; set; }
	public static int AlisonNet { get; set; }
	public static int AnaNet { get; set; }
	public static int GuillermoNet { get; set; }
	public static int JoseNet { get; set; }
	public static int AlejandroNet { get; set; }
	public static int ParentsNet { get; set; }
	public static int TeacherNet { get; set; }

	// Frienship
	public static int MariaFS { get; set; }
	public static int AlisonFS { get; set; }
	public static int AnaFS { get; set; }
	public static int GuillermoFS { get; set; }
	public static int JoseFS { get; set; }
	public static int AlejandroFS { get; set; }
	public static int ParentsFS { get; set; }
	public static int TeacherFS { get; set; }
	public static int Risk{ get { return Mathf.CeilToInt((MariaFS + AlisonFS + AnaFS + GuillermoFS + JoseFS + AlejandroFS) * 0.11f + ParentsFS * 0.2f + TeacherFS * 0.14f) ; } }

	// QuestsPerDay

	public static int MariaQuest { get; set; }
	// Day1 -> Meet Maria [0 - not spoken] [1 - freak] [2 - friend]
	// Day2 -> Speak Maria [0 - spoken] [1 - joke advise]
	// Day3 -> []
	// Day4 -> Maria Joke [0 - no joke] [1 - jokeIncomplete] [2 - jokeCompleted]

	public static int AlisonQuest { get; set; }
	// Day1 -> Found earing [0 - not found] [1 - found]
	// Day2 -> Speak Alison [0 - not spoken] [1 - spoken] [2 - joke advise]
	// Day3 -> []
	// Day4 -> []

	public static int AnaQuest { get; set; }
	// Day1 -> Meet Ana [0 - not spoken] [1 - spoken]
	// Day2 -> Speak Ana []
	// Day3 -> []
	// Day4 -> []

	public static int GuillermoQuest { get; set; }
	// Day1 -> Meet Guille [0 - not spoken] [1 - spoken] [2 - noteboard]
	// Day2 -> Speak Guille [0 - not spoken] [1 - spoken and go] [2 - spoken and not go] [3 - spoken and quest]
	// Day3 -> Speak Guille [0 - speak] [1 - about board]
	// Day4 -> []

	public static int JoseQuest { get; set; }
	// Day1 -> Meet Jose [0 - not spoken] [1 - spoken]
	// Day2 -> Speak Jose [0 - not spoken] [1 - spoken]
	// Day3 -> []
	// Day4 -> []

	public static int AlejandroQuest { get; set; }
	// Day1 -> Meet Alex [0 - sorry] [1 - threat] [2 - bad]
	// Day2 -> Speak Alex [0 - not spoken] [1 - joke] [2 - zero] [3 - fight]
	// Day3 -> []
	// Day4 -> []

	public static int GumQuest { get; set; }
	// Day2 -> Gum [0 - not washed] [1 - washed] [2 - not gum]

	public static int BoardQuest { get; set; }
	// Day3 -> Board [0 - not seen] [1 - seen]

	public static int ParentsMeetingQuest { get; set; }
	// Day3 -> Parent [0 - No Meeting] [1 - Meeting Cancel] [2 - Meeting Success]

	public static int SharedPassQuest { get; set; }
	// Day4 -> Alison [0 - no shared pass (mobile lost)] [1 - shared pass] [2 - no shared pass (mobile found)]

	public static int Final { get; set; }
	// [1 - bad end, alex win] [2 - normal end, advise] [3 - good end, bye Alex]

	protected static GlobalState instance;
	public static GlobalState Instance {  get { return instance == null ? instance = new GlobalState() : instance; } }

	public bool NotRepeatedDaysNonStatic
	{
		get { return NotRepeatedDays; }
	}

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

	public bool MessagesPendingNonStatic
	{
		get { return MessagesPending; }
	}

	public string UserNameNonStatic
	{
		get { return UserName; }
	}

	// FriendShip
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

	//Quests
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
		get { return AnaQuest; }
	}
	public int GuillermoQuestNonStatic
	{
		get { return GuillermoQuest; }
	}
	public int JoseQuestNonStatic
	{
		get { return JoseQuest; }
	}
	public int AlejandroQuestNonStatic
	{
		get { return AlejandroQuest; }
	}
	public int GumQuestNonStatic
	{
		get { return GumQuest; }
	}
	public int BoardQuestNonStatic
	{
		get { return BoardQuest; }
	}
	public int ParentsMeetingQuestNonStatic
	{
		get { return ParentsMeetingQuest; }
	}
	public int SharedPassQuestNonStatic
	{
		get { return SharedPassQuest; }
	}

	public int FinalNonStatic
	{
		get { return Final; }
	}

	// SocialNet
	public int MariaNetNonStatic
	{
		get { return MariaNet; }
	}
	public int AlisonNetNonStatic
	{
		get { return AlisonNet; }
	}
	public int AnaNetNonStatic
	{
		get { return AnaNet; }
	}
	public int GuillermoNetNonStatic
	{
		get { return GuillermoNet; }
	}
	public int JoseNetNonStatic
	{
		get { return JoseNet; }
	}
	public int AlejandroNetNonStatic
	{
		get { return AlejandroNet; }
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
