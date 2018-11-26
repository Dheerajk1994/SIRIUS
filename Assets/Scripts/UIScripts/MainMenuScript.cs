using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public GameObject GameManager;


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
}