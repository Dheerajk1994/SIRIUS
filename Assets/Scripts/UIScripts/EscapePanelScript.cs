using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapePanelScript : MonoBehaviour {

    private GameManagerScript gameManager;

    [SerializeField] private Button escapeButton;
    [SerializeField] private Toggle toggleSoundButton;

    public void SetEscapePanel(GameManagerScript gm)
    {
        gameManager = gm;
        escapeButton.onClick.AddListener(EscapeButtonClicked);
    }

    public void EscapeButtonClicked()
    {
        gameManager.ExitSceneIntoMainMenu();
    }

    public void ToggleSoundButtonClicked(bool value)
    {
        Debug.Log("toggle button clicked");
    }


}
