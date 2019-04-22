using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class QuestGenerator {
	private static QuestLists listOfQuests;

	private static string path;

	public static QuestLists GenerateQuest()
	{
		Debug.Log("Generate QuestGenerator called");
		path = Application.streamingAssetsPath + "/quests.json";
		if(File.Exists(path))
		{
			string jsonString = File.ReadAllText(path);
			listOfQuests = JsonUtility.FromJson<QuestLists>(jsonString);
			return listOfQuests;
		}
		else
		{
			Debug.LogError("path couldn't be opened");
			return null;
		}
	}
}
[System.Serializable]
public class QuestLists
{
    public List<QuestDescription> IntroQuests;
}

[System.Serializable]
public class QuestDescription
{
    public int questID;
    public int type;
	public int[] prerequisiteID;
    public string questName;
    public string questDescription;
    public int[] itemsRequired;
	public string[] mobName;
	public int[] mobKillAmnt;
    public int[] itemRewards;
}

