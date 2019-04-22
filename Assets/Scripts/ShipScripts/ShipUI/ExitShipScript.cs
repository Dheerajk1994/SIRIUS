using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitShipScript : MonoBehaviour
{

    //private Animator exitShipPanelAnimator;
    public Button exitShipPanelToggleButn;
    public Button yesButn;
    public Button noButn;
    private bool isOpen = false;

    UIScript uiScript;

    public void SetExitShipPanel(UIScript uScript)
    {
        uiScript = uScript;
    }

    private void Start()
    {
        //exitShipPanelAnimator = this.GetComponent<Animator>();
        //exitShipPanelToggleButn.onClick.AddListener(ToggleExitShipPanel);
    }

    public void ToggleExitShipPanel(bool toggle)
    {
        //exitShipPanelAnimator.SetBool("isOpen", toggle);
        isOpen = toggle;
    }

    public void ToggleExitShipPanel()
    {
        isOpen = !isOpen;
        //exitShipPanelAnimator.SetBool("isOpen", isOpen);

    }

}
