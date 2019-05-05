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
    public GameObject DialogueManagerPrefab;
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
    public GameObject dialogueManager;
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
    public DialogueManagerScript dialogueManagerScript;   

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
            if (Vector2.Distance(player.transform.position, playerPos) > 10.0f)
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
        
        audioManager = Instantiate(AudioManagerPrefab);
        audioManagerScript = audioManager.GetComponent<AudioManagerScript>();

        questManager = Instantiate(QuestManagerPrefab);
        questManagerScript = questManager.GetComponent<QuestManagerScript>();

        dialogueManager = Instantiate(DialogueManagerPrefab);
        dialogueManagerScript = dialogueManager.GetComponent<DialogueManagerScript>();

        terrainManagerScript.SetTerrainManager(this, this.GetComponent<TilePoolScript>(), player, inventoryControllerScript);
        playerScript.SetPlayerScript(this, uiScript, inputManagerScript, audioManagerScript);
        inputManagerScript.SetInputManager(this, uiScript, terrainManagerScript);

        inventoryControllerScript.SetInventoryController(this, uiScript, audioManagerScript);

//<<<<<<< ryan
//        terrainManagerScript          .SetTerrainManager(this, this.GetComponent<TilePoolScript>(), player, inventoryControllerScript);
//        playerScript                  .SetPlayerScript(this, uiScript,inputManagerScript, audioManagerScript);
//        inputManagerScript            .SetInputManager(this, uiScript, terrainManagerScript);
//        inventoryControllerScript     .SetInventoryController(this, uiScript, audioManagerScript);
//        questManagerScript            .SetQuestManager(uiScript.QuestPanel.GetComponent<QuestPanelScript>());
        //audioManagerScript
//=======
        
//>>>>>>> dtemp

        if (currentWorld == EnumClass.TerrainType.SHIP)
        {
            ship = Instantiate(ShipPrefab);
            shipScript = ship.GetComponent<ShipScript>();
            shipScript.SetShip(uiScript, audioManagerScript);
            shipScript.GetChestReference().GetComponent<InventoryHandlerScript>().PopulateInventory(TheImmortalScript.instance.ShipInventoryItems);
        }

        readyToGo = false;
        worldPresent = false;

        GameObject PlayArea = new GameObject("PlayArea");
        PlayArea.transform.position = new Vector3(0f, 0f, 0f);

        GenerateScene();

        //UI
        uiScript.SetUIPanel(this, inputManagerScript, audioManagerScript, player);
        inventoryControllerScript.PopulatePlayerHotbar(TheImmortalScript.instance.PlayerHotbarItems);
        inventoryControllerScript.PopulatePlayerInventory(TheImmortalScript.instance.PlayerInventoryItems);

        //QUEST AND DIALOGUE
        questManagerScript.SetQuestManager(uiScript.QuestPanel.GetComponent<QuestPanelScript>(), TheImmortalScript.instance.QuestsCompleted, TheImmortalScript.instance.ActiveQuests);
        dialogueManagerScript.SetDialogueManager(this, uiScript.BottomDialoguePanel.GetComponent<DialoguePanelScript>(), TheImmortalScript.instance.DialoguesCompleted);

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
//<<<<<<< ryan
//            ship = Instantiate(ShipPrefab);
//            shipScript = ship.GetComponent<ShipScript>();
//            shipScript.SetShip(uiScript, audioManagerScript);
//=======
            ship.transform.position = new Vector2(0f, 0f);
            playerPos = ship.GetComponent<ShipScript>().spawnPosition.position;
//>>>>>>> dtemp
        }

        cameraScript.SetCamera(player.transform, currentWorld);
        worldPresent = true;
    }

    public void TeleportToShip()
    {
        uiScript.loadingScreen.gameObject.SetActive(true);
        TheImmortalScript.instance.WorldTypeToGenerate = EnumClass.TerrainType.SHIP;
        SaveInventory();
        SaveQuestsAndDialogues();
        GameDataHandler.HandleTerrainSaving(this, terrainManagerScript, TheImmortalScript.instance);
        StartCoroutine(LoadScene("ShipScene"));
        //operation.allowSceneActivation = true;
    }

    public void TeleportToTerrain()
    {
        uiScript.loadingScreen.gameObject.SetActive(true);
        SaveInventory();
        SaveQuestsAndDialogues();
        StartCoroutine(LoadScene("TerrainScene"));
        //operation.allowSceneActivation = true;
    }

    private void SaveInventory()
    {
        TheImmortalScript.instance.PlayerHotbarItems = inventoryControllerScript.FetchItemsInPlayerHotbar();
        TheImmortalScript.instance.PlayerInventoryItems = inventoryControllerScript.FetchItemsInPlayerInventory();
        if(currentWorld == EnumClass.TerrainType.SHIP)
        {
            TheImmortalScript.instance.ShipInventoryItems = ship.GetComponent<ShipScript>().GetChestReference().GetComponent<InventoryHandlerScript>().FetchAllItemsInInventory();
        }
    }

    private void SaveQuestsAndDialogues()
    {
        TheImmortalScript.instance.QuestsCompleted = questManagerScript.completedQuestsID;
        TheImmortalScript.instance.ActiveQuests = questManagerScript.activeQuestsId;
        TheImmortalScript.instance.DialoguesCompleted = dialogueManagerScript.completedDialogues;
    }

    public void SaveGame()
    {

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

    public AudioManagerScript getAudioManager()
    {
        return audioManagerScript;
    }



}//END GAME MANAGER
