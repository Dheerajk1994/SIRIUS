using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenu : MonoBehaviour
{

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Game Exits");
    }

    public void showPanel(GameObject gamee){
        gamee.gameObject.SetActive(true);
    }
    public void hidePanel(GameObject gamee){
        gamee.gameObject.SetActive(false);
    }
}