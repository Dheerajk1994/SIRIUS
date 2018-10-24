using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public GameObject player;
    public GameObject TerrainManager;
    public GameObject stone;

    private void Start()
    {
        TerrainManager.GetComponent<GenerateTerrainScript>().StartTerrainGeneration();
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
