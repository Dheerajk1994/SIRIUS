using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    //public static GameManagerScript instance;

    #region WORLD_INFO
    public string greenWorldSavePath = "greenWorld";
    public string moonWorldSavePath = "moonWorld";

    public EnumClass.TerrainType currentWorld;         
    public string currentWorldSavePath = "greenWorld";
    #endregion

    #region PREFABS
    public GameObject TerrainManagerPrefab;
    public GameObject UIPrefab;
    public GameObject PlayerPrefab;
    public GameObject MainCameraPrefab;
    public GameObject InputManagerPrefab;
    public GameObject InventoryControllerPrefab;
    public GameObject AIManagerPrefab;
    public GameObject QuestManagerPrefab;
    public GameObject ShipPrefab;
    public GameObject AudioManagerPrefab;

    #endregion

    #region INSTANTIATED_PREFABS
    public GameObject terrainManager;
    public GameObject ui;
    public GameObject player;
    public GameObject mainCameraObject;
    public GameObject inputManager;
    public GameObject inventoryController;
    public GameObject aiManager;
    public GameObject questManager;
    public GameObject ship;
    public GameObject audioManager;
    #endregion

    #region SCRIPT_REFERENCES
    public TerrainManagerScript terrainManagerScript;
    public UIScript uiScript;
    public Player playerScript;
    public CameraScript cameraScript;
    public InputManagerScript inputManagerScript;
    public InventoryControllerScript inventoryControllerScript;
    public QuestManagerScript questManagerScript;   
    public ShipScript shipScript;
    public AudioManagerScript audioManagerScript;

    #endregion

    #region LOCAL_VARIABLES
    public bool readyToGo = false;
    public bool worldPresent = false;
    public Vector3 playerPos;
    #endregion

    //AWAKE
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else if (this != instance)
    //    {
    //        Destroy(this);
    //    }
    //    //DontDestroyOnLoad(this.gameObject);
    //}

    //START
    private void Start()
    {
        PopulateSceneObjects();
    }

    private void Update()
    {
        if (readyToGo && currentWorld != EnumClass.TerrainType.SHIP)
        {
            if (Vector2.Distance(player.transform.position, playerPos) > 20.0f)
            {
                StartCoroutine(terrainManagerScript.DisplayChunks(player.transform.position, true));
                playerPos = player.transform.position;
            }
        }
    }

    private void ReceiveFromTheImmortal()
    {

    }

    private void GiveToTheImmortal()
    {

    }

    private void PopulateSceneObjects()
    {
        ////
        currentWorld = TheImmortalScript.instance.WorldTypeToGenerate;

        terrainManager = Instantiate(TerrainManagerPrefab);
        terrainManagerScript = terrainManager.GetComponent<TerrainManagerScript>();

        ui = Instantiate(UIPrefab);
        uiScript = ui.GetComponent<UIScript>();
        ui.gameObject.SetActive(false);

        player = Instantiate(PlayerPrefab);
        playerScript = player.GetComponent<Player>();
        player.gameObject.SetActive(false);

        mainCameraObject = Instantiate(MainCameraPrefab);
        cameraScript = mainCameraObject.GetComponent<CameraScript>();
        mainCameraObject.gameObject.SetActive(false);

        inputManager = Instantiate(InputManagerPrefab);
        inputManagerScript = inputManager.GetComponent<InputManagerScript>();

        inventoryController = Instantiate(InventoryControllerPrefab);
        inventoryControllerScript = inventoryController.GetComponent<InventoryControllerScript>();

        aiManager = Instantiate(AIManagerPrefab);

        questManager = Instantiate(QuestManagerPrefab);
        questManagerScript = questManager.GetComponent<QuestManagerScript>();
        
        audioManager = Instantiate(AudioManagerPrefab);
        audioManagerScript = audioManager.GetComponent<AudioManagerScript>();
        

        terrainManagerScript.SetTerrainManager(this, this.GetComponent<TilePoolScript>(), player, inventoryControllerScript);
        playerScript.SetPlayerScript(this, uiScript, inputManagerScript);
        inputManagerScript.SetInputManager(this, uiScript, terrainManagerScript);

        inventoryControllerScript.SetInventoryController(this, uiScript);

        questManagerScript.SetQuestManager(uiScript.QuestPanel.GetComponent<QuestPanelScript>());

        if (currentWorld == EnumClass.TerrainType.SHIP)
        {
            ship = Instantiate(ShipPrefab);
            shipScript = ship.GetComponent<ShipScript>();
            shipScript.SetShip(uiScript);
        }

        readyToGo = false;
        worldPresent = false;

        GameObject PlayArea = new GameObject("PlayArea");
        PlayArea.transform.position = new Vector3(0f, 0f, 0f);

        GenerateScene();

        uiScript.SetUIPanel(this, inputManagerScript, audioManagerScript, player);
        inventoryControllerScript.PopulatePlayerHotbar(TheImmortalScript.instance.PlayerHotbarItems);
        inventoryControllerScript.PopulatePlayerInventory(TheImmortalScript.instance.PlayerInventoryItems);


        player.transform.position = playerPos;
        player.SetActive(false);
        bool stat = true;
        if(currentWorld != EnumClass.TerrainType.SHIP)
        {
            StartCoroutine(terrainManagerScript.DisplayChunks(player.transform.position, stat));
        }
        if (stat)
        {
            player.SetActive(true);
            readyToGo = true;
        }
    }

    private void GenerateScene()
    {
        GameDataHandler.HandleTerrainDataGeneration(this, TheImmortalScript.instance, terrainManagerScript);
        ui.SetActive(true);
        mainCameraObject.SetActive(true);

        player.SetActive(true);
        player.GetComponent<SpriteRenderer>().sortingOrder = (int)EnumClass.LayerIDEnum.FRONTLAYER;
        player.transform.SetParent(GameObject.Find("PlayArea").transform);

        if (currentWorld == EnumClass.TerrainType.SHIP)
        {
            ship.transform.position = new Vector2(0f, 0f);
            playerPos = ship.GetComponent<ShipScript>().spawnPosition.position;
        }

        cameraScript.SetCamera(player.transform, currentWorld);
        worldPresent = true;
    }

    public void TeleportToShip()
    {
        uiScript.loadingScreen.gameObject.SetActive(true);
        TheImmortalScript.instance.WorldTypeToGenerate = EnumClass.TerrainType.SHIP;
        TheImmortalScript.instance.PlayerHotbarItems = inventoryControllerScript.FetchItemsInPlayerHotbar();
        TheImmortalScript.instance.PlayerInventoryItems = inventoryControllerScript.FetchItemsInPlayerInventory();
        GameDataHandler.HandleTerrainSaving(this, terrainManagerScript, TheImmortalScript.instance);
        StartCoroutine(LoadScene("ShipScene"));
        //operation.allowSceneActivation = true;
    }

    public void TeleportToTerrain()
    {
        uiScript.loadingScreen.gameObject.SetActive(true);
        TheImmortalScript.instance.PlayerHotbarItems = inventoryControllerScript.FetchItemsInPlayerHotbar();
        TheImmortalScript.instance.PlayerInventoryItems = inventoryControllerScript.FetchItemsInPlayerInventory();
        StartCoroutine(LoadScene("TerrainScene"));
        //operation.allowSceneActivation = true;
    }

    private IEnumerator LoadScene(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        //operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            yield return null;
        }
    }



}//END GAME MANAGER
