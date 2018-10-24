using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public GameObject GameManager;






    public void NewGameButton()
    {
        GameManager.GetComponent<GameManagerScript>().StartNewGame();
        this.gameObject.SetActive(false);
    }
}
