using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
USE THIS CLASS TO HANDLE SAVING AND LOADING THE GAME
PUBLIC STATIC CLASS SO CAN BE CALLED FROM ANYWHERE
REDUCES CLUTTER IN GAMEMANAGER 
*/
public static class GameDataHandler {

    public static void NewGame(GameManagerScript gameManager, TerrainManagerScript terrainManager)
    {
        if (!gameManager.worldPresent)
        {
            gameManager.readyToGo = false;

            gameManager.mainCamera.gameObject.SetActive(false);

            terrainManager.StartTerrainGen();

            gameManager.player.gameObject.SetActive(true);
            gameManager.player.GetComponent<SpriteRenderer>().sortingOrder = (int)EnumClass.LayerIDEnum.FRONTLAYER;
            gameManager.player.transform.SetParent(GameObject.Find("PlayArea").transform);
            gameManager.playerPos = terrainManager.GetSafePlaceToSpawnPlayer();
            gameManager.player.transform.position = gameManager.playerPos;

            gameManager.mainCamera.gameObject.SetActive(true);
            gameManager.mainCamera.gameObject.GetComponent<CameraScript>().playerToFollow = gameManager.player.transform;

            gameManager.worldPresent = true;
            gameManager.readyToGo = true;


        }
    }

    public static void SaveGame(GameManagerScript gameManager, TerrainManagerScript terrainManager)
    {
        //CHECK IF THERE IS A WORLD RUNNING & CALL SAVAMANAGER
        if (gameManager.worldPresent)
        {
            gameManager.readyToGo = false;
            Debug.Log("Saving game...");
            SaveManager.SaveGame(
                terrainManager.frontTilesValue,
                terrainManager.frontTilesResourceValue,
                terrainManager.backTilesValue,
                terrainManager.backTilesResourceValue,
                terrainManager.vegetationTilesValue,
                gameManager.player.transform.position);
            Debug.Log("Game saved!");
            gameManager.readyToGo = true;
        }
    }

    public static void LoadGame(GameManagerScript gameManager, TerrainManagerScript terrainManager)
    {
        //LOAD ONLY IF THERE IS NO WORLD PRESENT
        if (!gameManager.worldPresent)
        {
            gameManager.readyToGo = false;

            gameManager.mainCamera.gameObject.SetActive(false);
            gameManager.player.SetActive(false);


            SerializedSaveData loadedData = SaveManager.LoadGame();
            terrainManager.SetTiles(
                loadedData.ftileData,
                loadedData.fResourceData,
                loadedData.btileData,
                loadedData.bResourceData,
                loadedData.vtileData
                );

            gameManager.player.SetActive(true);
            gameManager.player.transform.localPosition = new Vector3(loadedData.playerPosX, loadedData.playerPosY, loadedData.playerPosZ);
            gameManager.player.GetComponent<SpriteRenderer>().sortingOrder = (int)EnumClass.LayerIDEnum.FRONTLAYER;

            gameManager.mainCamera.gameObject.SetActive(true);
            gameManager.mainCamera.GetComponent<CameraScript>().playerToFollow = gameManager.player.transform;

            gameManager.worldPresent = true;
            gameManager.readyToGo = true;
        }
    }
}
