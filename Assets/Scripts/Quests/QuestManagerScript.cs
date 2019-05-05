using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestManagerScript : MonoBehaviour {

    public static QuestManagerScript instance;
    private QuestPanelScript questPanel;

    public List<Quest> listOfQuests;
    public List<Quest> listOfActiveQuests;
    public List<int> completedQuestsID;
    public List<int> activeQuestsId;


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

    public void SetQuestManager(QuestPanelScript qpanel, List<int> completedQsts, List<int> activeQuests)
    {
        questPanel = qpanel;
        this.completedQuestsID = completedQsts;
        this.activeQuestsId = activeQuests;
        GenerateQuests();
        GenerateActiveQuests(activeQuests);
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
                    questDescription.itemsToInteractWith,
                    questDescription.itemRewards));
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        //UpdateActiveQuests();
        //CallQuestPanelToUpdateListedQuests();
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
                completedQuestsID.Add(quest.questID);
                listOfActiveQuests.Remove(quest);
                activeQuestsId.Remove(quest.questID);
                break;
            }
        }
        UpdateActiveQuests();
    }

    private void UpdateActiveQuests()
    {
        foreach (Quest quest in listOfQuests)
        {
            if (quest.prerequisiteID.Length > 0)
            {
                bool questPrereqComplete = true;
                foreach (int pID in quest.prerequisiteID)
                {
                    if (pID == 0 || !GetQuestOfID(pID).isComplete)
                    {
                        questPrereqComplete = false;
                        break;
                    }
                }
                if (questPrereqComplete && !quest.isComplete)//if all the prereqs are done and the quest is not repeat
                {
                    listOfActiveQuests.Add(quest);
                    activeQuestsId.Add(quest.questID);
                }
            }
        }
        CallQuestPanelToUpdateListedQuests();
    }

    private void GenerateActiveQuests(List<int> id)//called at the start to show saved quests between scene switches
    {
        foreach (int qID in id)
        {
            foreach (Quest quest in listOfQuests)
            {
                if (quest.questID == qID)
                {
                    listOfActiveQuests.Add(quest);
                }
            }
        }
        CallQuestPanelToUpdateListedQuests();
    }

    public void RemoveQuestsOfID(List<int> id)
    {
        foreach (int qID in id)
        {
            foreach (Quest quest in listOfQuests)
            {
                if (quest.questID == qID)
                {
                    listOfActiveQuests.Remove(quest);
                    activeQuestsId.Remove(quest.questID);
                }
            }
        }
        CallQuestPanelToUpdateListedQuests();
    }

    public void AddQuestsOfID(List<int> id)
    {
        foreach(int qID in id)
        {
            foreach (Quest quest in listOfQuests)
            {   
                if (quest.questID == qID)
                {
                    listOfActiveQuests.Add(quest);
                    activeQuestsId.Add(quest.questID);
                }
            }
        }
        CallQuestPanelToUpdateListedQuests();
    }

    private void CallQuestPanelToUpdateListedQuests()
    {
        if (questPanel.isActiveAndEnabled)
        {
            questPanel.SetQuestListPanel(listOfActiveQuests);
        }
    }

    //QUEST TRACKING
    public void InteractedWithItem(string name)
    {
        Debug.Log("interacted with object called");
        foreach (Quest quest in listOfActiveQuests)
        {
            if (quest.questGoal.questType == QuestType.INTERACTING)
            {
                quest.questGoal.ItemInteractedWith(name);
                
                if (quest.questGoal.IsReached())
                {
                    Debug.Log("interact quest completed");
                    completedQuestsID.Add(quest.questID);
                    QuestCompleted(quest.questID);
                    DialogueManagerScript.instance.QuestCompletedUpdateDialogues(completedQuestsID);
                }
                else if (questPanel.isActiveAndEnabled)
                {
                    questPanel.UpdateShowingQuest(quest);
                }
            }
        }
    }

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

    public void KilledMob(string name, int amnt)
    {
        foreach (Quest quest in listOfActiveQuests)
        {
            if (quest.questGoal.questType == QuestType.KILLING)
            {
                quest.questGoal.EnemyKilled(name, amnt);
                if (questPanel.isActiveAndEnabled)
                {
                    questPanel.UpdateShowingQuest(quest);
                }
            }
        }
    }

}
