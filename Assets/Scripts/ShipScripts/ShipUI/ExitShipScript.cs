using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitShipScript : MonoBehaviour
{
    public AudioManagerScript audiomanager;
    private Animator exitShipPanelAnimator;
    public Button exitButn;
    public Button yesButn;
    public Button noButn;
    private bool isOpen = false;

    UIScript uiScript;

    public void SetExitShipPanel(UIScript uScript)
    {
        exitButn.onClick.AddListener(ClosePanelClicked);
        noButn.onClick.AddListener(ClosePanelClicked);
        uiScript = uScript;
        
        audiomanager = uScript.audioManager.GetComponent<AudioManagerScript>();
        yesButn.onClick.AddListener(YesBtnClicked);
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
        audiomanager.Play("ui-animation");
    }

    public void ToggleExitShipPanel()
    {
        isOpen = !isOpen;
        exitShipPanelAnimator.SetBool("isOpen", isOpen);
        audiomanager.Play("ui-animation");

    }

    public void ClosePanelClicked()
    {
        ToggleExitShipPanel(false);
        audiomanager.Play("btn-confirm");
    }

    public void YesBtnClicked()
    {
        // handle scene change
        audiomanager.Play("btn-confirm");
        TheImmortalScript.instance.WorldTypeToGenerate = uiScript.NavPanel.GetComponent<NavigationScript>().currentPlanet;
        uiScript.gameManagerScript.TeleportToTerrain();
    }

}
