using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    private Animator chestPanelAnimator;
    public Button exitButn;
    private bool isOpen = false;

    UIScript uiScript;

    public void SetChestPanel(UIScript uScript)
    {
        uiScript = uScript;
    }

    private void Start()
    {
        chestPanelAnimator = this.GetComponent<Animator>();
        //enginePanelToggleButn.onClick.AddListener(ToggleEnginePanel);
    }

    public void ToggleChestPanel(bool toggle)
    {
        isOpen = toggle;
        chestPanelAnimator.SetBool("isOpen", toggle);
    }

    public void ToggleChestPanel()
    {
        isOpen = !isOpen;
        chestPanelAnimator.SetBool("isOpen", isOpen);
    }
}
