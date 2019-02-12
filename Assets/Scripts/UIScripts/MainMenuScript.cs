using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    public int resolut;
    public GameObject GameManager;
    public Dropdown myDropdown;
    public void SetDropdownIndex(Dropdown index)
    {
        int res = index.value;
        resolut = res;
    }
public void screenSz()
    {
        switch (resolut)
        {
            case 0:
                Screen.SetResolution(1024, 768, true);
                Debug.Log("Screen size changed0");
                break;

            case 1:
                Screen.SetResolution(1280, 720, true);
                Debug.Log("Screen size changed1");
                break;

            case 2:
                Screen.SetResolution(1600, 900, true);
                Debug.Log("Screen size changed2");
                break;
        }
    }


    public void NewGameButton()
    {
        GameManager.GetComponent<GameManagerScript>().StartNewGame();
        this.gameObject.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Game Exits");
    }

    public void TogglePanel(GameObject panel)
    {
        panel.gameObject.SetActive(!panel.gameObject.activeSelf);
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }


    void OnMouseOver()
    {

    }
}