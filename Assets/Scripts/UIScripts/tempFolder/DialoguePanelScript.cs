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

    UIScript uiScript;

    public void SetDialoguePanel(UIScript uScript)
    {
        uiScript = uScript;
    }

    private void Start()
    {
        dialoguePanelAnimator = this.GetComponent<Animator>();
    }

    public void ToggleDialoguePanel(bool toggle)
    {
        dialoguePanelAnimator.SetBool("isShowing", toggle);
    }

    private void DialogueNextClicked()
    {
        DialogueController.NextDialogue();
    }

    private void DialogueCloseClicked()
    {
        dialoguePanelAnimator.SetBool("isShowing", false);
    }

    public void SetDialoguePanelText(string title, string dialogue)
    {
        dialogueTitleText.text = title;
        dialogueMainText.text = dialogue;
    }
}
