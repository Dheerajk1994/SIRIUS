using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public GameObject player;
    public GameObject TerrainManager;
    public GameObject stone;

    public GameObject UI;
    public GameObject playerInvoPanel;
    public GameObject craftingPanel;


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
        TerrainManager.GetComponent<GenerateTerrainScript>().StartTerrainGeneration();
        Camera.main.gameObject.transform.SetParent(player.transform);
        player.gameObject.SetActive(true);
        player.GetComponent<SpriteRenderer>().sortingLayerName = "frontTileLayer";
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) LeftMouseClicked();
        else if (Input.GetMouseButton(1)) RightMouseClicked();
        else if(Input.GetKeyDown(KeyCode.I)) ToggleInventory();
        else if (Input.GetKeyDown(KeyCode.C)) ToggleCrafting();
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
