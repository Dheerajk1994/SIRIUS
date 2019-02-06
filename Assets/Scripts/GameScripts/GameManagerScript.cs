using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    public GameObject player;
    public GameObject TerrainManager;
    public GameObject stone;

    public GameObject UI;
    public GameObject playerInvoPanel;
    public GameObject craftingPanel;
    public GameObject attributePanel;

    public Slider healthBar;
    public Slider staminaBar;
    public Slider hungerBar;


    public bool isInDemoMode;

    private void Start()
    {
        player.gameObject.SetActive(false);
        if (isInDemoMode)
        {
            StartNewGame();
        }
    }

    public void StartNewGame()
    {
        TerrainManager.GetComponent<TerrainManagerScript>().StartTerrainGen();
        Camera.main.gameObject.transform.SetParent(player.transform);
        player.gameObject.SetActive(true);
        player.GetComponent<SpriteRenderer>().sortingLayerName = "frontTileLayer";
        player.transform.SetParent(GameObject.Find("PlayArea").transform);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) LeftMouseClicked();
        else if (Input.GetMouseButton(1)) RightMouseClicked();
        else if(Input.GetKeyDown(KeyCode.I)) ToggleInventory();
        else if (Input.GetKeyDown(KeyCode.C)) ToggleCrafting();
        TerrainManager.GetComponent<TerrainManagerScript>().DisplayChunks(player.transform.position);
    }

    private void LeftMouseClicked()
    {
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TerrainManager.GetComponent<TerrainManagerScript>().MineTile(Mathf.RoundToInt(mPos.x), Mathf.RoundToInt(mPos.y));
    }

    private void RightMouseClicked()
    {
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TerrainManager.GetComponent<TerrainManagerScript>().PlaceTile(Mathf.RoundToInt(mPos.x), Mathf.RoundToInt(mPos.y), stone);
    }

    private void ToggleInventory()
    {
        UI.GetComponent<UIScript>().TogglePlayerInventory();
    }

    private void ToggleCrafting()
    {
        UI.GetComponent<UIScript>().ToggleCraftingPanel();
    }


}
