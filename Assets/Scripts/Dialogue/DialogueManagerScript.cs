using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManagerScript : MonoBehaviour {

    public static DialogueManagerScript instance;

    private GameManagerScript gameManagerScript;
    private DialoguePanelScript dialoguePanel;

    private List<Dialogue> listOfAllWTAFDialogues;
    private Dialogue currentActiveDialogue;
    public List<int> completedDialogues;

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

    public void SetDialogueManager(GameManagerScript gameManager, DialoguePanelScript dialoguePanel, List<int> completedDlg)
    {
        this.gameManagerScript = gameManager;
        this.dialoguePanel = dialoguePanel;
        this.completedDialogues = completedDlg;
        PopulateDialogueList(DialogueGenerator.GenerateDialogues());
        Debug.Log("complete dlg count " + completedDlg.Count);
        if(completedDialogues.Count == 0) SetActiveDialogue(1);
        
    }

    private void PopulateDialogueList(DialogueList listOfDialogueDescription)
    {
        listOfAllWTAFDialogues = new List<Dialogue>();
        foreach(DialogueDescription dDescription in listOfDialogueDescription.WTAFDialogues)
        {
            listOfAllWTAFDialogues.Add(new Dialogue(dDescription.dialogueID, dDescription.autoDisplayDialogue, dDescription.dialogueGiverName, dDescription.dialogueLines, dDescription.dialogueActivated, dDescription.questsNeededToActivateDialogue, dDescription.questsActivatedByDialgue));
        }      
    }

    public void SetActiveDialogue(int id)
    {
        Debug.Log("set active dialogue called with id " + id);
        foreach(Dialogue dialogue in listOfAllWTAFDialogues)
        {
            if(dialogue.dialogueID == id)
            {
                currentActiveDialogue = dialogue;
                if(dialogue.autoDisplayDialogue == true)
                {
                    //call dialogue panel immediately with dialogue
                    dialoguePanel.SetDialoguePanelText(currentActiveDialogue.dialogueID, currentActiveDialogue.dialogueGiverName, currentActiveDialogue.dialogueLines);
                    dialoguePanel.ToggleDialoguePanel(true);
                }
            }
        }
    }

    public void QuestCompletedUpdateDialogues(List<int> completedQuests)
    {
        Dialogue dialogueToDisplay = null;
        foreach(Dialogue dialogue in listOfAllWTAFDialogues)
        {
            for(int i = 0; i < dialogue.questsNeededToActivateDialogue.Length; ++i)
            { 
                if (!completedQuests.Contains(dialogue.questsNeededToActivateDialogue[i]) || completedDialogues.Contains(dialogue.dialogueID)){
                    dialogueToDisplay = null;
                    break;
                }
                dialogueToDisplay = dialogue;
            }
            if (dialogueToDisplay != null) break;
        }
        if(dialogueToDisplay != null)
        {
            currentActiveDialogue = dialogueToDisplay;
            SetActiveDialogue(currentActiveDialogue.dialogueID);
        }
    }


    public void FinishedReadingDialogue()
    {
        QuestManagerScript.instance.AddQuestsOfID(new List<int>(currentActiveDialogue.questsActivatedByDialgue));
        completedDialogues.Add(currentActiveDialogue.dialogueID);
        currentActiveDialogue = null;
    }


}
