using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuSceneScript : MonoBehaviour {

    public Button newGameButn;
    public Button loadGameButn;
    public Button exitButn;

    public Transform loadingScreen;

    public string MainGameString = "ShipScene";

    int waitCounter = 0;

    private void Start()
    {
        loadingScreen.gameObject.SetActive(false);
        newGameButn.onClick.AddListener(NewGame);
        loadGameButn.onClick.AddListener(LoadGame);
        exitButn.onClick.AddListener(ExitGame);
    }

    private void NewGame()
    {
        loadingScreen.gameObject.SetActive(true);
        SetUpTheImmortal();
        StartCoroutine(LoadAsynchronously("ShipScene"));
    }

    private void LoadGame()
    {
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
        TheImmortalScript.instance.ShipInventoryItems = new ushort[40,40];
        TheImmortalScript.instance.ShipInventoryItems = items;

        TheImmortalScript.instance.GreenWorldStatus = TheImmortalScript.TerrainStatus.NOT_GENERATED;
        TheImmortalScript.instance.MoonWorldStatus = TheImmortalScript.TerrainStatus.NOT_GENERATED;
        TheImmortalScript.instance.SnowWorldStatus = TheImmortalScript.TerrainStatus.NOT_GENERATED;
        TheImmortalScript.instance.DesertWorldStatus = TheImmortalScript.TerrainStatus.NOT_GENERATED;

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
