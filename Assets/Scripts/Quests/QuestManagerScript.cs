using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestManagerScript : MonoBehaviour {

    public static QuestManagerScript instance;
    private QuestPanelScript questPanel;

    public List<Quest> listOfQuests;
    public List<Quest> listOfActiveQuests;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(this != instance)
        {
            Destroy(this);
        }
    }

    public void SetQuestManager(QuestPanelScript qpanel)
    {
        questPanel = qpanel;
        GenerateQuests();
    }

    public void GenerateQuests(){
        listOfQuests = new List<Quest>();
        listOfActiveQuests = new List<Quest>();
        //read in stuff from json
        QuestLists qListFromJson = QuestGenerator.GenerateQuest();
        //make list of quests
        foreach(QuestDescription questDescription in qListFromJson.IntroQuests)
        {
            try
            {
                listOfQuests.Add(new Quest(
                    questDescription.questID,
                    questDescription.type,
                    questDescription.prerequisiteID,
                    questDescription.questName,
                    questDescription.questDescription,
                    questDescription.itemsRequired,
                    questDescription.mobName,
                    questDescription.mobKillAmnt,
                    questDescription.itemRewards));
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        listOfActiveQuests.Add(GetQuestOfID(2));
        if (questPanel.isActiveAndEnabled)
        {
            questPanel.SetQuestListPanel(listOfActiveQuests);
        }
    }

    public List<Quest> GetListOfActiveQuests()
    {
        return listOfActiveQuests;
    }

    public Quest GetQuestOfID(int id)
    {
        foreach(Quest quest in listOfQuests)
        {
            if(quest.questID == id)
            {
                return quest;
            }
        }
        return null;
    }

    public void QuestCompleted(int id)
    {
        foreach (Quest quest in listOfActiveQuests)
        {
            if (quest.questID == id)
            {
                quest.isComplete = true;
                listOfActiveQuests.Remove(quest);
                break;
            }
        }
        
        foreach(Quest quest in listOfQuests)
        {
            if(quest.prerequisiteID.Length > 0)
            {
                bool questPrereqComplete = true;
                foreach(int pID in quest.prerequisiteID)
                {
                    if (!GetQuestOfID(pID).isComplete)
                    {
                        questPrereqComplete = false;
                        break;
                    }
                }
                if (questPrereqComplete && !quest.isComplete)//if all the prereqs are done and the quest is not repeat
                {
                    listOfActiveQuests.Add(quest);
                }
            }
        }
        if (questPanel.isActiveAndEnabled)
        {
            questPanel.SetQuestListPanel(listOfActiveQuests);
        }
    }

    public void RemoveQuestOfID(int id)
    {
        foreach(Quest quest in listOfQuests)
        {
            if (quest.questID == id)
            {
                listOfActiveQuests.Remove(quest);
            }
        }
    }

    //QUEST TRACKING
    public void PickedUpItem(ushort id, int amnt)
    {
        foreach(Quest quest in listOfActiveQuests)
        {
            if(quest.questGoal.questType == QuestType.GATHERING)
            {
                quest.questGoal.ItemCollected(id, amnt);
                if (questPanel.isActiveAndEnabled)
                {
                    questPanel.UpdateShowingQuest(quest);
                }
            }
        }
    }
}
