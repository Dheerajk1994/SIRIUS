using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineScript : MonoBehaviour
{

    //private Animator enginePanelAnimator;
    public Button exitButn;
    public Button refuelButn;
    private bool isOpen = false;

    UIScript uiScript;

    public void SetEnginePanel(UIScript uScript)
    {
        uiScript = uScript;
    }

    private void Start()
    {
        //enginePanelAnimator = this.GetComponent<Animator>();
        //enginePanelToggleButn.onClick.AddListener(ToggleEnginePanel);
    }

    public void ToggleEnginePanel(bool toggle)
    {
        //enginePanelAnimator.SetBool("isOpen", toggle);
        isOpen = toggle;
    }

    public void ToggleEnginePanel()
    {
        isOpen = !isOpen;
        //enginePanelAnimator.SetBool("isOpen", isOpen);
    }
}
