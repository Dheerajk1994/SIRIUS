using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public GameObject player;
    public GameObject TerrainManager;
    public GameObject stone;
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





}
