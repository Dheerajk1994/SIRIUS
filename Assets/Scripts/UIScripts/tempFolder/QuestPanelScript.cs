using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanelScript : MonoBehaviour {

    private Animator questPanelAnimator;
    public Text questPanelTitleText;
    public Text questPanelMainText;
    public Button questPanelToggleButn;
    private bool questPanelShown = false;

    UIScript uiScript;

    public void SetQuestPanel(UIScript uScript)
    {
        uiScript = uScript;
    }

    private void Start()
    {
        questPanelAnimator = this.GetComponent<Animator>();
    }

    public void SetQuestPanelText(string title, string qSummary)
    {
        questPanelTitleText.text = title;
        questPanelMainText.text = qSummary;
    }

    public void ToggleQuestPanel(bool toggle)
    {
        questPanelAnimator.SetBool("isShowing", toggle);
        questPanelShown = toggle;
    }

    public void ToggleQuestPanel()
    {
        questPanelShown = !questPanelShown;
        questPanelAnimator.SetBool("isShowing", questPanelShown);

    }

}
