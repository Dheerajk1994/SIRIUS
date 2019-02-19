using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerScript : MonoBehaviour
{

    public GameObject player;
    public GameObject TerrainManager;
    public GameObject stone;
    public GameObject MobSpawner;

    private TerrainManagerScript terrainManagerScript;
    private MobSpawnerScript mobSpawnerScript;

    public GameObject UI;
    public GameObject playerInvoPanel;
    public GameObject craftingPanel;
    public GameObject attributePanel;

    public Slider healthBar;
    public Slider staminaBar;
    public Slider hungerBar;

    public Camera mainCamera;

    public bool isInDemoMode;
    private bool readyToGo = false;

    Vector2 playerPos;

    private void Start()
    {
        player.gameObject.SetActive(false);
        playerPos = player.transform.position;
        terrainManagerScript = TerrainManager.GetComponent<TerrainManagerScript>();
        mobSpawnerScript = MobSpawner.GetComponent<MobSpawnerScript>();
        if (isInDemoMode)
        {
            //StartNewGame();
        }
    }

    private void Update()
    {
        if (readyToGo)
        {
            if (Input.GetMouseButton(0)) LeftMouseClicked();
            else if (Input.GetMouseButton(1)) RightMouseClicked();
            else if (Input.GetKeyDown(KeyCode.I)) ToggleInventory();
            else if (Input.GetKeyDown(KeyCode.C)) ToggleCrafting();
            //if(Vector2.Distance(player.transform.position, playerPos) > 20)
            //{
            //    terrainManagerScript.DisplayChunks(player.transform.position);
            //    playerPos = player.transform.position;
            //}
                terrainManagerScript.DisplayChunks(player.transform.position);
        }
    }

    public void StartNewGame()
    {
        mainCamera.gameObject.SetActive(false);

        terrainManagerScript.StartTerrainGen();
        mobSpawnerScript.generateSpawnLocations(terrainManagerScript.frontTilesValue,50);
        mobSpawnerScript.printSpawnLocations();
        mobSpawnerScript.printNumberOfSpawnLocations();

        player.gameObject.SetActive(true);
        player.GetComponent<SpriteRenderer>().sortingLayerName = "frontTileLayer";
        player.transform.SetParent(GameObject.Find("PlayArea").transform);

        mainCamera.gameObject.SetActive(true);
        readyToGo = true;
    }


    public void SaveGame()
    {
        readyToGo = false;
        Debug.Log("Saving game...");
        SaveManager.SaveGame(terrainManagerScript.frontTilesValue, terrainManagerScript.backTilesValue, terrainManagerScript.vegetationTilesValue);
        Debug.Log("Game saved!");
        readyToGo = true;
    }

    public void LoadGame()
    {
        mainCamera.gameObject.SetActive(false);

        SaveManager.LoadGame(terrainManagerScript);
        player.gameObject.SetActive(true);
        player.GetComponent<SpriteRenderer>().sortingLayerName = "frontTileLayer";
        player.transform.SetParent(GameObject.Find("PlayArea").transform);

        mainCamera.gameObject.SetActive(true);
        readyToGo = true;
    }

    private void LeftMouseClicked()
    {
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        terrainManagerScript.MineTile(Mathf.RoundToInt(mPos.x), Mathf.RoundToInt(mPos.y));
    }

    private void RightMouseClicked()
    {
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        terrainManagerScript.PlaceTile(Mathf.RoundToInt(mPos.x), Mathf.RoundToInt(mPos.y), stone, 2);
    }

    private void ToggleInventory()
    {
        UI.GetComponent<UIScript>().TogglePlayerInventory();
    }

    private void ToggleCrafting()
    {
        UI.GetComponent<UIScript>().ToggleCraftingPanel();
    }


}//END GAME MANAGER
