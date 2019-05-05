using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue  {

    public int dialogueID;
    public bool autoDisplayDialogue;
    public string dialogueGiverName;
    public string[] dialogueLines;
    public int dialogueActivated;
    public int[] questsNeededToActivateDialogue;
    public int[] questsActivatedByDialgue;

    public Dialogue(int dialogueID, bool autoDisplayDialogue, string dialogueGiverName, string[] dialogueLines, int dialogueActivated, int[] questsNeededToActivateDialogue, int[] questsActivatedByDialgue)
    {
        this.dialogueID = dialogueID;
        this.autoDisplayDialogue = autoDisplayDialogue;
        this.dialogueGiverName = dialogueGiverName;
        this.dialogueLines = dialogueLines;
        this.dialogueActivated = dialogueActivated;
        this.questsNeededToActivateDialogue = questsNeededToActivateDialogue;
        this.questsActivatedByDialgue = questsActivatedByDialgue;
    }
}
