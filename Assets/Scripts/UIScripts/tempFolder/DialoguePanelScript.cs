using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanelScript : MonoBehaviour {


    private Animator dialoguePanelAnimator;
    public Text dialogueTitleText;
    public Text dialogueMainText;
    public Button dialogueNextButn;
    public Button dialogueCloseButn;

    int dialogueID;
    string[] dialogueStrings;
    int currentDialogueStringIndex;

    UIScript uiScript;

    public void SetDialoguePanel(UIScript uScript)
    {
        uiScript = uScript;
        dialogueNextButn.onClick.AddListener(DialogueNextClicked);
        dialogueCloseButn.onClick.AddListener(DialogueCloseClicked);
        dialoguePanelAnimator = this.GetComponent<Animator>();
    }
    

    public void ToggleDialoguePanel(bool toggle)
    {
        dialoguePanelAnimator.SetBool("isShowing", toggle);
    }

    private void DialogueNextClicked()
    {
        currentDialogueStringIndex++;
        if(currentDialogueStringIndex < dialogueStrings.Length)
        {
            dialogueMainText.text = dialogueStrings[currentDialogueStringIndex];
        }
        else
        {
            ToggleDialoguePanel(false);
            DialogueManagerScript.instance.FinishedReadingDialogue();
        }
    }

    private void DialogueCloseClicked()
    {
        dialoguePanelAnimator.SetBool("isShowing", false);
    }

    public void SetDialoguePanelText(int id, string title, string[] dialogue)
    {
        dialogueID = id;
        dialogueTitleText.text = title;
        dialogueStrings = dialogue;
        currentDialogueStringIndex = 0;

        dialogueMainText.text = dialogue[currentDialogueStringIndex];

    }
}
