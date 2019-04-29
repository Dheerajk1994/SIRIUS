using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    #region WORLD_INFO
    public string greenWorldSavePath = "greenWorld";
    public string moonWorldSavePath = "moonWorld";

    public ushort currentWorld;         
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
    #endregion

    #region LOCAL_VARIABLES
    public bool readyToGo = false;
    public bool worldPresent = false;
    public Vector3 playerPos;
    #endregion

    //AWAKE
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (this != instance)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    //START
    private void Start()
    {
        //INITIALIZE ALL THE PREFABS AND SCRIPT REFERENCES
        terrainManager            = Instantiate(TerrainManagerPrefab);
        terrainManagerScript      = terrainManager.GetComponent<TerrainManagerScript>();
                                  
        ui                        = Instantiate(UIPrefab);
        uiScript                  = ui.GetComponent<UIScript>();
                                  
        player                    = Instantiate(PlayerPrefab);
        playerScript              = player.GetComponent<Player>();
                                  
        mainCameraObject          = Instantiate(MainCameraPrefab);
        cameraScript              = mainCameraObject.GetComponent<CameraScript>();
                                  
        inputManager              = Instantiate(InputManagerPrefab);
        inputManagerScript        = inputManager.GetComponent<InputManagerScript>();

        inventoryController       = Instantiate(InventoryControllerPrefab);
        inventoryControllerScript = inventoryController.GetComponent<InventoryControllerScript>();

        aiManager                 = Instantiate(AIManagerPrefab);

        questManager              = Instantiate(QuestManagerPrefab);
        questManagerScript        = questManager.GetComponent<QuestManagerScript>();
        //ship                      = Instantiate(ShipPrefab);
        //shipScript                = ship.GetComponent<ShipScript>();

        ui.      gameObject.SetActive(false);
        player.  gameObject.SetActive(false);

        terrainManagerScript          .SetTerrainManager(this, this.GetComponent<TilePoolScript>(), player, inventoryControllerScript);
        playerScript                  .SetPlayerScript(this, uiScript,inputManagerScript);
        inputManagerScript            .SetInputManager(this, uiScript, terrainManagerScript);
        inventoryControllerScript     .SetInventoryController(this, uiScript);
        questManagerScript            .SetQuestManager(uiScript.QuestPanel.GetComponent<QuestPanelScript>());

        //SET LOCAL VARIABLES
        readyToGo    = false;
        worldPresent = false;

        GameObject PlayArea = new GameObject("PlayArea");
        PlayArea.transform.position = new Vector3(0f, 0f, 0f);


        StartNewGame();
        uiScript                      .SetUIPanel(this, inputManagerScript, player);
    }

    private void Update()
    {
        if (readyToGo && currentWorld != (ushort)EnumClass.TerrainType.SHIP)
        {
            if (Vector2.Distance(player.transform.position, playerPos) > 20.0f)
            {
                //Debug.Log("player transform pos: " + player.transform.position);
                //Debug.Log("playerpos: " + playerPos);
                terrainManagerScript.DisplayChunks(player.transform.position);
                playerPos = player.transform.position;
                terrainManagerScript.DisplayChunks(player.transform.position);
            }
        }
    }

    public void StartNewGame()
    {
        currentWorld =  (ushort)EnumClass.TerrainType.SHIP;//TEST

        if(currentWorld == (ushort)EnumClass.TerrainType.SHIP)
        {
            ship = Instantiate(ShipPrefab);
            shipScript = ship.GetComponent<ShipScript>();
            shipScript.SetShip(uiScript);
        }

        GameDataHandler.NewGame(this, terrainManagerScript, currentWorld);
    }

    public void SaveGame()
    {
        GameDataHandler.SaveGame(currentWorldSavePath, this, terrainManagerScript);
    }

    public void LoadGame()
    {
        GameDataHandler.LoadGame(currentWorldSavePath, this, terrainManagerScript);
    }



}//END GAME MANAGER
