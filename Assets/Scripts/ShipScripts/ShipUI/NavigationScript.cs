using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationScript : MonoBehaviour {

    private Animator navPanelAnimator;
    public Button exitButn;
    public Button selectButn;
    private bool isOpen = false;

    UIScript uiScript;

    public void SetNavPanel(UIScript uScript)
    {
        uiScript = uScript;
    }

    private void Start()
    {
        navPanelAnimator = this.GetComponent<Animator>();
        //enginePanelToggleButn.onClick.AddListener(ToggleEnginePanel);
    }

    public void ToggleEnginePanel(bool toggle)
    {
        isOpen = toggle;
        navPanelAnimator.SetBool("isOpen", toggle);
    }

    public void ToggleEnginePanel()
    {
        isOpen = !isOpen;
        navPanelAnimator.SetBool("isOpen", isOpen);
    }
}
