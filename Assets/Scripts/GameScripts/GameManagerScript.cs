using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerScript : MonoBehaviour
{
    GameManagerScript instance;

    public GameObject stone;

    //PREFABS
    public GameObject UIPrefab;             
    public GameObject PlayerPrefab;
    public GameObject TerrainManagerPrefab;
    public Camera MainCameraPrefab;

    //INSTANTIATEED PREFABS
    public GameObject player;
    public GameObject TerrainManager;

    public Camera     mainCamera;
    public GameObject UI;

    private TerrainManagerScript terrainManagerScript;
    public UIScript   uiScript;


    public GameObject playerInvoPanel;
    public GameObject craftingPanel;
    //public GameObject attributePanel;

    public Slider healthBar;
    public Slider staminaBar;
    public Slider hungerBar;

    public bool isInDemoMode;
    public bool readyToGo = false;
    public bool worldPresent;

    public Vector3 playerPos;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(this != instance)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }


    private void Start()
    {
        UI               = Instantiate(UIPrefab);

        uiScript        = UI.GetComponent<UIScript>();
        playerInvoPanel = uiScript.playerInvoPanel;
        craftingPanel   = uiScript.craftingPanel;
        healthBar       = uiScript.healthBar;
        staminaBar      = uiScript.staminaBar;
        hungerBar       = uiScript.hungerBar;



        TerrainManager   = Instantiate(TerrainManagerPrefab);
        //mainCamera       = Instantiate(Resources.Load("Main Camera", typeof(Camera))) as Camera;
        mainCamera = Instantiate(MainCameraPrefab);

        player           = Instantiate(PlayerPrefab);
        player.gameObject.SetActive(false);
        

        playerInvoPanel.GetComponent<PlayerInventoryPanelScript>().player = player;

        terrainManagerScript = TerrainManager.GetComponent<TerrainManagerScript>();


        GameObject PlayArea = new GameObject("PlayArea");
        PlayArea.transform.position = new Vector3(0f, 0f, 0f);

        worldPresent = false;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) StartNewGame();
        else if (Input.GetKeyDown(KeyCode.S)) SaveGame();
        else if (Input.GetKeyDown(KeyCode.L)) LoadGame();
        else if (Input.GetKeyDown(KeyCode.O)) uiScript.ToggleBotDialoguePanel(true);
        else if (Input.GetKeyDown(KeyCode.P)) uiScript.ToggleBotDialoguePanel(false);
        else if (Input.GetKeyDown(KeyCode.Q)) uiScript.ToggleQuestPanel(true);
        else if (Input.GetKeyDown(KeyCode.E)) uiScript.ToggleQuestPanel(false);

        if (readyToGo)
        {
            if (Input.GetMouseButton(0)) LeftMouseClicked();
            else if (Input.GetMouseButton(1)) RightMouseClicked();
            else if (Input.GetKeyDown(KeyCode.I)) ToggleInventory();
            else if (Input.GetKeyDown(KeyCode.C)) ToggleCrafting();
            
            if (Vector2.Distance(player.transform.position, playerPos) > 20)
            {
                terrainManagerScript.DisplayChunks(player.transform.position);
                playerPos = player.transform.position;
            }
            terrainManagerScript.DisplayChunks(player.transform.position);
        }
    }

    public void StartNewGame()
    {
        GameDataHandler.NewGame(this, terrainManagerScript);
    }

    public void SaveGame()
    {
        GameDataHandler.SaveGame(this, terrainManagerScript);
    }

    public void LoadGame()
    {
        GameDataHandler.LoadGame(this, terrainManagerScript);
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
