using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class QuestPanelScript : MonoBehaviour {

    [SerializeField] private Button questListButtonPrefab;
    [SerializeField] private GameObject questRewReqPrefab;

    [SerializeField] private Sprite questCompletedIcon;
    [SerializeField] private Sprite questOngoingIcon;

    private GameManagerScript gameManagerScript;
    private AudioManagerScript audiomanager;

    [SerializeField] private Text questTitleTxt;
    [SerializeField] private Text questDescriptionTxt;
    [SerializeField] private Transform questListPanel;
    [SerializeField] private Transform questRequirementsPanel;
    [SerializeField] private Transform questRewardsPanel;
    [SerializeField] private Button abandonQuestButton;
    [SerializeField] private Button turnInQuestButton;

    private int idOfQuestCurrentlyDisplaying = 0;
    
    public void ToggleQuestPanel()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        audiomanager.Play("ui-animation");
        if (this.gameObject.activeSelf)
        {
            List<Quest> quests = QuestManagerScript.instance.GetListOfActiveQuests();
            if(quests.Count > 0)
            {
                SetQuestListPanel(quests);
                SetQuestDescription(quests[0]);
            }
        }
    }

    public void SetQuestPanel(GameManagerScript gms)//called by gamemanager with required references
    {
        gameManagerScript = gms;
        audiomanager = gms.audioManagerScript;
        abandonQuestButton.onClick.AddListener(QuestAbandonButtonClicked);
        turnInQuestButton.onClick.AddListener(QuestTurnInButtonClicked);
    }

    public void SetQuestListPanel(List<Quest> quests)
    {
        ClearPanel(questListPanel);
        foreach(Quest quest in quests)
        {
            Button butn = Instantiate(questListButtonPrefab);
            butn.GetComponentInChildren<Text>().text = quest.questName;
            if (quest.questGoal.IsReached())
            {
                butn.transform.Find("Img_QuestCompletionStatus").GetComponent<Image>().sprite = questCompletedIcon;
            }
            else
            {
                butn.transform.Find("Img_QuestCompletionStatus").GetComponent<Image>().sprite = questOngoingIcon;
            }
            butn.gameObject.AddComponent<QuestListButtonScript>();
            butn.GetComponent<QuestListButtonScript>().mappedQuestID = quest.questID;
            butn.onClick.AddListener(QuestListButtonClicked);
            butn.transform.SetParent(questListPanel.transform, false);
        }
        if(quests.Count > 0)
        {
            SetQuestDescription(quests[0]);
        }
    }

    public void SetQuestDescription(Quest quest)
    {
        idOfQuestCurrentlyDisplaying = quest.questID;

        questTitleTxt.text = quest.questName;
        questDescriptionTxt.text = quest.questDescription;

        PopulateRequirementPanel(quest);
        PopulateRewardsPanel(quest);

        if (!quest.questGoal.IsReached())
        {
            turnInQuestButton.interactable = false;
        }
        else
        {
            turnInQuestButton.interactable = true;
        }
    }

    public void UpdateShowingQuest(Quest quest)
    {
        if(idOfQuestCurrentlyDisplaying == quest.questID)
        {
            PopulateRequirementPanel(quest);
            PopulateRewardsPanel(quest);

            if (!quest.questGoal.IsReached())
            {
                turnInQuestButton.interactable = false;
            }
            else
            {
                turnInQuestButton.interactable = true;
            }
        }
    }

    public void QuestTurnInButtonClicked()
    {
        QuestManagerScript.instance.QuestCompleted(idOfQuestCurrentlyDisplaying);
        audiomanager.Play("btn-refuel");
    }

    public void QuestAbandonButtonClicked()
    {
        audiomanager.Play("btn-confirm");
    }

    private void ClearPanel(Transform panel)
    {
        foreach (Transform child in panel)
        {
            Destroy(child.gameObject);
        }
    }

    private void PopulateRequirementPanel(Quest quest)
    {
        ClearPanel(questRequirementsPanel);
        //fill the required items panel
        if (quest.questGoal.requiredItems != null)
        {
            foreach (QuestItemsRequirement itemsRequirement in quest.questGoal.requiredItems)
            {
                GameObject reqObject = Instantiate(questRewReqPrefab);
                Text uiText = reqObject.GetComponentInChildren<Text>();

                reqObject.GetComponentInChildren<Image>().sprite = InventorySpritesScript.instance.GetSprite(itemsRequirement.itemID);
                uiText.text = itemsRequirement.requiredAmnt.ToString();
                if(itemsRequirement.currentAmnt < itemsRequirement.requiredAmnt)
                {
                    uiText.color = Color.red;
                }
                else
                {
                    uiText.color = Color.green;
                }
                reqObject.transform.SetParent(questRequirementsPanel, false);
            }
        }
    }

    private void PopulateRewardsPanel(Quest quest)
    {
        ClearPanel(questRewardsPanel);
        //fill the rewards items panel
        if (quest.rewards != null)
        {
            foreach (QuestRewardItems rewards in quest.rewards)
            {
                GameObject rewardObject = Instantiate(questRewReqPrefab);
                //Debug.Log("item id: " + itemsRequirement.itemID);
                rewardObject.GetComponentInChildren<Image>().sprite = InventorySpritesScript.instance.GetSprite(rewards.itemId);
                rewardObject.GetComponentInChildren<Text>().text = rewards.rewardAmnt.ToString();
                rewardObject.transform.SetParent(questRewardsPanel, false);
            }
        }
    }

    public void QuestListButtonClicked()
    {
        GameObject clickedButn = EventSystem.current.currentSelectedGameObject.gameObject;
        try
        {
            SetQuestDescription(QuestManagerScript.instance.GetQuestOfID(clickedButn.GetComponent<QuestListButtonScript>().mappedQuestID));
            audiomanager.Play("btn-quick-ui");
        }
        catch(Exception e)
        {
            throw e;
        }
    }
}
class QuestListButtonScript : MonoBehaviour
{
    public int mappedQuestID;
}