using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuSceneScript : MonoBehaviour {

    public Button newGameButn;
    public Button loadGameButn;
    public Button exitButn;
    private AudioSource menuBGM;

    public GameObject ErrorPanel;
    public Transform loadingScreen;

    public string MainGameString = "ShipScene";

    int waitCounter = 0;

    private void Start()
    {
        loadingScreen.gameObject.SetActive(false);
        ErrorPanel.GetComponent<ErrorPanelScript>().SetErrorPanel();
        newGameButn.onClick.AddListener(NewGame);
        loadGameButn.onClick.AddListener(LoadGame);
        exitButn.onClick.AddListener(ExitGame);
        menuBGM = this.GetComponent<AudioSource>();
        menuBGM.Play();
    }

    private void NewGame()
    {
        loadingScreen.gameObject.SetActive(true);
        SetUpTheImmortal();
        StartCoroutine(LoadAsynchronously("ShipScene"));
    }

    private void LoadGame()
    {
        //ErrorPanel.GetComponent<ErrorPanelScript>().SetError("not implemented yet");
        SerializedImmortalData immortalData = SaveManager.LoadGame();
        if(immortalData == null)
        {
            ErrorPanel.GetComponent<ErrorPanelScript>().SetError("A saved game does not exist.");
            return;
        }
        else
        {
            LoadUpTheImmortal(immortalData);
            if(TheImmortalScript.instance.WorldTypeToGenerate == EnumClass.TerrainType.SHIP)
            {
                StartCoroutine(LoadAsynchronously("ShipScene"));
            }
            else
            {
                StartCoroutine(LoadAsynchronously("TerrainScene"));
            }

        }
    }

    private void ExitGame()
    {

    }

    private void SetUpTheImmortal()
    {
        TheImmortalScript.instance.WorldTypeToGenerate = EnumClass.TerrainType.SHIP;
        TheImmortalScript.instance.TerrainGenerated = EnumClass.TerrainType.GREEN;
        TheImmortalScript.instance.IsNewGame = true;

        TheImmortalScript.instance.PlayerHotbarItems = new ushort[10, 10];
        TheImmortalScript.instance.PlayerInventoryItems = new ushort[40, 40];

        TheImmortalScript.instance.QuestsCompleted = new List<int>();
        TheImmortalScript.instance.ActiveQuests = new List<int>();
        TheImmortalScript.instance.DialoguesCompleted = new List<int>();

        //add some stuff to ship chest
        ushort [,] items = new ushort[40, 40];

        items[0, 0] = (ushort)EnumClass.TileEnum.REGULAR_WOOD;
        items[1, 0] = 60;

        items[0, 1] = (ushort)EnumClass.TileEnum.DIAMOND;
        items[1, 1] = 30;

        items[0, 2] = (ushort)EnumClass.TileEnum.SILVER;
        items[1, 2] = 30;

        items[0, 3] = (ushort)EnumClass.TileEnum.IRON;
        items[1, 3] = 30;

        items[0, 4] = (ushort)EnumClass.TileEnum.COAL;
        items[1, 4] = 30;

        items[0, 5] = (ushort)EnumClass.TileEnum.COPPER;
        items[1, 5] = 30;

        TheImmortalScript.instance.ShipInventoryItems = new ushort[40,40];
        TheImmortalScript.instance.ShipInventoryItems = items;
        TheImmortalScript.instance.ShipFuelAmount = 0;

        TheImmortalScript.instance.GreenWorldStatus = TheImmortalScript.TerrainStatus.NOT_GENERATED;
        TheImmortalScript.instance.MoonWorldStatus = TheImmortalScript.TerrainStatus.NOT_GENERATED;
        TheImmortalScript.instance.SnowWorldStatus = TheImmortalScript.TerrainStatus.NOT_GENERATED;
        TheImmortalScript.instance.DesertWorldStatus = TheImmortalScript.TerrainStatus.NOT_GENERATED;

    }

    private void LoadUpTheImmortal(SerializedImmortalData immortalData)
    {
        TheImmortalScript.instance.WorldTypeToGenerate = (EnumClass.TerrainType)immortalData.worldTypeToGenerate;
        TheImmortalScript.instance.TerrainGenerated = (EnumClass.TerrainType)immortalData.terrainGenerated;
        TheImmortalScript.instance.IsNewGame = immortalData.isNewGame;

        TheImmortalScript.instance.PlayerHotbarItems = immortalData.playerHotbarItems;
        TheImmortalScript.instance.PlayerInventoryItems = immortalData.playerInventoryItems;

        TheImmortalScript.instance.QuestsCompleted = immortalData.questsCompleted;
        TheImmortalScript.instance.ActiveQuests = immortalData.activeQuests;
        TheImmortalScript.instance.DialoguesCompleted = immortalData.dialoguesCompleted;

        TheImmortalScript.instance.ShipInventoryItems = immortalData.shipInventoryItems;
        TheImmortalScript.instance.ShipFuelAmount = immortalData.shipFuelAmount;

        TheImmortalScript.instance.GreenWorldStatus = (TheImmortalScript.TerrainStatus)immortalData.greenWorldStatus;
        TheImmortalScript.instance.MoonWorldStatus = (TheImmortalScript.TerrainStatus)immortalData.moonWorldStatus;
        TheImmortalScript.instance.SnowWorldStatus = (TheImmortalScript.TerrainStatus)immortalData.snowWorldStatus;
        TheImmortalScript.instance.DesertWorldStatus = (TheImmortalScript.TerrainStatus)immortalData.desertWorldStatus;

        TheImmortalScript.instance.PlayerLandPosInGreen = new Vector2(immortalData.playerLandPosInGreenX, immortalData.playerLandPosInGreenY);
        TheImmortalScript.instance.PlayerLandPosInMoon = new Vector2(immortalData.playerLandPosInMoonX, immortalData.playerLandPosInMoonY);
        TheImmortalScript.instance.PlayerLandPosInSnow = new Vector2(immortalData.playerLandPosInSnowX, immortalData.playerLandPosInSnowY);
        TheImmortalScript.instance.PlayerLandPosInDesert = new Vector2(immortalData.playerLandPosInDesertX, immortalData.playerLandPosInDesertY);

    }

    IEnumerator LoadAsynchronously(string loadScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(loadScene);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
