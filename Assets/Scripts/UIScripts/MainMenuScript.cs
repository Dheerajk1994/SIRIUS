using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    public int resolut;
    public GameObject GameManager;
    public Dropdown resDropdown;
    public GameObject pauseMenu;
    public Toggle fullScreenToggle;
    public GameSettings gameSettings;
    public Resolution[] resolution;

    private void OnEnable()
    {
        gameSettings = new GameSettings();
        fullScreenToggle.onValueChanged.AddListener(delegate { ONfullScreenToggle();  });
        resDropdown.onValueChanged.AddListener(delegate { OnResChange(); });
        resolution = Screen.resolutions;

        foreach (Resolution res in resolution) 
        {
            resDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
          }
         
    } 

    public void ONfullScreenToggle() {
       gameSettings.fullscreen =   Screen.fullScreen = fullScreenToggle.isOn; 
    }

    public void OnResChange()
    {
        Screen.SetResolution(resolution[resDropdown.value].width, resolution[resDropdown.value].height, Screen.fullScreen );    
    }

    public void saveSettings() {

    }
     
    public void loadSettings() { 

    }


    public void NewGameButton() // when the new button has been clicked, the game will begin
    {
        GameManager.GetComponent<GameManagerScript>().StartNewGame();
        this.gameObject.SetActive(false);
    }

    public void quitGame() // Ends the game
    {
        Application.Quit();
        Debug.Log("Game Exits");
    }

    public void TogglePanel(GameObject panel) //Toggles the panels hide or show
    {
        panel.gameObject.SetActive(!panel.gameObject.activeSelf);
    }

    public void LoadScene(int scene) // Loads the different scenes
    {
        SceneManager.LoadScene(scene);
    }



    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        // if the esc key is pressed, it toggles the pause menu
        //during the game
        {

            bool isActive = pauseMenu.activeSelf;

            pauseMenu.SetActive(!isActive);
        }
    }

}