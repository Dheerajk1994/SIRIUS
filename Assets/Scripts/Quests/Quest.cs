using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest {

    private bool isActive;
    public bool isComplete = false;

/****** QUEST ATTRIBUTES******/

    public int questID;
    public int[] prerequisiteID;
    public string questName;
    public string questDescription;
    public List<QuestRewardItems> rewards;

    public GameObject objectToActivate;
    public QuestGoal questGoal;

    public Quest(int id, int qType, int[] pID, string name, string description, int[] reqItems, string[] mobs, int[] killAmnt, int[] questRewards){
        questID = id;
        
        prerequisiteID = pID;
        questName = name;
        questDescription = description;

        questGoal = new QuestGoal(qType, reqItems, mobs, killAmnt);
        if(questRewards != null)
        {
            rewards = new List<QuestRewardItems>();
            for (int i = 0; i < questRewards.Length; ++i)
            {
                ushort rid = (ushort)questRewards[i];
                int amnt = questRewards[i++];
                rewards.Add(new QuestRewardItems(rid, amnt));
            }
        }
    }
}

[System.Serializable]
public class QuestGoal{
    public QuestType questType;
    public List<QuestItemsRequirement> requiredItems;
    public List<QuestMobRequirement> requiredMobs;
    public bool interacted;

    public QuestGoal(int type, int[] reqItems, string[] mobs, int[] killAmnt){
        requiredItems = new List<QuestItemsRequirement>();
        requiredMobs = new List<QuestMobRequirement>();
        questType = (QuestType)type;
        if(reqItems != null)
        {
            for (int i = 0; i < reqItems.Length; ++i)
            {
                int id = reqItems[i];
                i += 1;
                int amnt = reqItems[i];
                requiredItems.Add(new QuestItemsRequirement((ushort)id, 0, amnt));
            }
        }
       
        if(mobs != null)
        {
            for (int i = 0; i < mobs.Length; ++i)
            {
                string name = mobs[i];
                int amnt = killAmnt[i];
                requiredMobs.Add(new QuestMobRequirement(name, amnt));
            }
        }
    }

    public bool IsReached()
    {
        if(questType == QuestType.KILLING){
            foreach(QuestMobRequirement mobs in requiredMobs){
                if(mobs.currentKillCount < mobs.requiredKillCount){
                    return false;
                }
            }
            return true;    
        }

        if(questType == QuestType.GATHERING){
            foreach(QuestItemsRequirement items in requiredItems){
                if(items.currentAmnt < items.requiredAmnt){
                    return false;
                }
            }
            return true;
        }

        return false;
    }

    public void EnemyKilled(string name,int amnt){
        foreach (QuestMobRequirement mobs in requiredMobs)
        {
            if (mobs.name == name)
            {
                mobs.currentKillCount += amnt;
            }
        }
    }

    public void ItemCollected(ushort id, int amnt){
        foreach(QuestItemsRequirement items in requiredItems)
        {
            if(items.itemID == id)
            {
                items.currentAmnt += amnt;
            }
        }
    }
}

public class QuestItemsRequirement
{
        public ushort itemID;
        public int currentAmnt;
        public int requiredAmnt;

        public QuestItemsRequirement(ushort id, int camnt, int ramnt) //what if we already have items in the invo???
        {
            itemID = id;
            currentAmnt = camnt;
            requiredAmnt = ramnt;
        }
 }

public class QuestMobRequirement{
    public string name;
    public int currentKillCount;
    public int requiredKillCount;

    public QuestMobRequirement(string n, int rkc){
        name = n;
        currentKillCount = 0;
        requiredKillCount = rkc;
    }
}

public class QuestRewardItems
{
    public ushort itemId;
    public int rewardAmnt;

    public QuestRewardItems(ushort id, int amnt)
    {
        itemId = id;
        rewardAmnt = amnt;
    }
}

public enum QuestType {
     INTERACTING = 0, 
     GATHERING = 1, 
     KILLING = 2
}