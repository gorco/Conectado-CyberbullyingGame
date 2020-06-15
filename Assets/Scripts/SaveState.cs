using System;
using System.IO;
using UnityEngine;

public class SaveState
{
	readonly String StorageDir = Application.persistentDataPath;
	MobileChat mobileChat;
	ComputerManager computer;

	public SaveState(MobileChat mobileChatScript, ComputerManager computerScript)
	{
		String StorageDir = Application.persistentDataPath;
		mobileChat = mobileChatScript;
		computer = computerScript;
	}

	public void SaveDay(int scene)
	{
		String fileId = GlobalState.Nick;
		if (File.Exists(Path.Combine(StorageDir, fileId)))
		{
			File.Delete(Path.Combine(StorageDir, fileId));
		}
		String fileData = "";
		fileData += "Day:" + GlobalState.Day.ToString() + '\n'
				+ "Hour:" + GlobalState.Hour + '\n'
				+ "Min:" + GlobalState.Minute + '\n'
				+ "Scene:" + scene + '\n'
				+ "Male:" + (GlobalState.MaleSex ? "Yes" : "No") + '\n'
				+ "UserName:" + GlobalState.UserName + '\n'
				+ "UserPass:" + GlobalState.UserPass + '\n'
				+ "MariaFS:" + GlobalState.MariaFS.ToString() + '\n'
				+ "MariaQuest:" + GlobalState.MariaQuest.ToString() + '\n'
				+ "AlisonFS:" + GlobalState.AlisonFS.ToString() + '\n'
				+ "AlisonQuest:" + GlobalState.AlisonQuest.ToString() + '\n'
				+ "AnaFS:" + GlobalState.AnaFS.ToString() + '\n'
				+ "AnaQuest:" + GlobalState.AnaQuest.ToString() + '\n'
				+ "GuillermoFS:" + GlobalState.GuillermoFS.ToString() + '\n'
				+ "GuillermoQuest:" + GlobalState.GuillermoQuest.ToString() + '\n'
				+ "JoseFS:" + GlobalState.JoseFS.ToString() + '\n'
				+ "JoseQuest:" + GlobalState.JoseFS.ToString() + '\n'
				+ "AlejandroFS:" + GlobalState.AlejandroFS.ToString() + '\n'
				+ "AlejandroQuest:" + GlobalState.AlejandroQuest.ToString() + '\n'
				+ "GumQuest:" + GlobalState.GumQuest.ToString() + '\n'
				+ "BoardQuest:" + GlobalState.BoardQuest.ToString() + '\n'
				+ "ParentsMeetingQuest:" + GlobalState.ParentsMeetingQuest.ToString() + '\n'
				+ "SharedPassQuest:" + GlobalState.SharedPassQuest.ToString() + '\n'
				+ mobileChat.GetMobileStringState()
				+ computer.GetComputerStringState();

		File.WriteAllText(Path.Combine(StorageDir, fileId + ".save"), fileData);
	}

	public int CheckFile(string userName)
	{
		String fileId = GlobalState.UserName;
		String path = Path.Combine(StorageDir, fileId);
		int day = -1;
		if (File.Exists(path))
		{
			String[] stateLines = File.ReadAllLines(path);
			foreach (String line in stateLines)
			{
				String[] parts = line.Split(':');
				if (parts[0]== "Day")
				{
					day = int.Parse(parts[1]);
					return day;
				}
			}
		}
		return day;
	}

	public int LoadDayByUsername(string userName)
	{
		String path = Path.Combine(StorageDir + "/", userName + ".save");
		return LoadDay(path, userName);
	}

	public int LoadDay(string path, string nick)
	{
		int scene = -1;
		if (File.Exists(path))
		{
			String[] stateLines = File.ReadAllLines(path);
			GlobalState.Nick = nick;
			foreach (String line in stateLines)
			{
				String[] parts = line.Split(':');
				switch (parts[0])
				{
					case "Day": GlobalState.Day = int.Parse(parts[1]);  break;
					case "Hour": GlobalState.Hour = int.Parse(parts[1]); break;
					case "Min": GlobalState.Minute = int.Parse(parts[1]); break;
					case "Scene": scene = int.Parse(parts[1]); break;
					case "Male":
						{
							if (parts[1] == "Yes")
								GlobalState.MaleSex = true;
							else
								GlobalState.MaleSex = false;
							break;
						}
					case "UserName": GlobalState.UserName = parts[1]; break;
					case "UserPass": GlobalState.UserPass = parts[1]; break;
					case "MariaFS": GlobalState.MariaFS = int.Parse(parts[1]); break;
					case "MariaQuest": GlobalState.MariaQuest = int.Parse(parts[1]); break;
					case "AlisonFS": GlobalState.AlisonFS = int.Parse(parts[1]); break;
					case "AlisonQuest": GlobalState.AlisonQuest = int.Parse(parts[1]); break;
					case "AnaFS": GlobalState.AnaFS = int.Parse(parts[1]); break;
					case "AnaQuest": GlobalState.AnaQuest = int.Parse(parts[1]); break;
					case "GuillermoFS": GlobalState.GuillermoFS = int.Parse(parts[1]); break;
					case "GuillermoQuest": GlobalState.GuillermoQuest = int.Parse(parts[1]); break;
					case "JoseFS": GlobalState.JoseFS = int.Parse(parts[1]); break;
					case "JoseQuest": GlobalState.JoseQuest = int.Parse(parts[1]); break;
					case "AlejandroFS": GlobalState.AlejandroFS = int.Parse(parts[1]); break;
					case "AlejandroQuest": GlobalState.AlejandroQuest = int.Parse(parts[1]); break;
					case "GumQuest": GlobalState.GumQuest = int.Parse(parts[1]); break;
					case "BoardQuest": GlobalState.BoardQuest = int.Parse(parts[1]); break;
					case "ParentsMeetingQuest": GlobalState.ParentsMeetingQuest = int.Parse(parts[1]); break;
					case "SharedPassQuest": GlobalState.SharedPassQuest = int.Parse(parts[1]); break;
					case "Mobile": LoadMobileState(parts[1]); break;
					case "Publication": LoadPublication(parts[1]); break;
					case "Comment": LoadComment(parts[1]); break;
					case "Friend": LoadFriend(parts[1]); break;
				}
			}

		}
		return scene;
	}

	private void LoadFriend(string friendRequest)
	{
		string[] data = friendRequest.Split('|');
		computer.AddFriendRequest(data[0], data[2], data[1]);
		computer.ResolveFriendRequest(true, data[0], null);
	}

	private void LoadComment(string commentInfo)
	{
		string[] data = commentInfo.Split('|');
		computer.AddPublicationComment(data[0], data[1], data[2]);
	}

	private void LoadPublication(string publicationInfo)
	{
		string[] data = publicationInfo.Split('|');
		computer.NewPublication(data[1], data[3], data[0], data[2]);
	}

	public void LoadMobileState(string message)
	{
		string[] messageParts = message.Split('|');
		mobileChat.AddMessage(messageParts[2], messageParts[1], messageParts[0]);
	}

	public void RemoveStates()
	{

	}
}
