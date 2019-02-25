using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuSceneScript : MonoBehaviour {

    public Button newGameButn;
    public Button loadGameButn;
    public Button exitButn;

    public string MainGameString = "MainGame";

    private void Start()
    {
        newGameButn.onClick.AddListener(NewGame);
        loadGameButn.onClick.AddListener(LoadGame);
        exitButn.onClick.AddListener(ExitGame);
    }

    private void NewGame()
    {
        GameObject.Find("GameManager").GetComponent<GameManagerScript>().StartNewGame();
    }

    private void LoadGame()
    {
        GameObject.Find("GameManager").GetComponent<GameManagerScript>().LoadGame();
    }

    private void ExitGame()
    {

    }


}
