using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitShipScript : MonoBehaviour
{
    private Animator exitShipPanelAnimator;
    public Button exitButn;
    public Button yesButn;
    public Button noButn;
    private bool isOpen = false;

    UIScript uiScript;

    public void SetExitShipPanel(UIScript uScript)
    {
        uiScript = uScript;
        yesButn.onClick.AddListener(ExitShipPressed);
    }

    private void Start()
    {
        exitShipPanelAnimator = this.GetComponent<Animator>();
        //exitButn.onClick.AddListener(ToggleExitShipPanel);
    }

    public void ToggleExitShipPanel(bool toggle)
    {
        isOpen = toggle;
        exitShipPanelAnimator.SetBool("isOpen", toggle);
    }

    public void ToggleExitShipPanel()
    {
        isOpen = !isOpen;
        exitShipPanelAnimator.SetBool("isOpen", isOpen);

    }

    public void ExitShipPressed()
    {
        TheImmortalScript.instance.WorldTypeToGenerate = uiScript.NavPanel.GetComponent<NavigationScript>().currentPlanet;
        uiScript.gameManagerScript.TeleportToTerrain();
    }

}
