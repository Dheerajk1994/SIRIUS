using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
USE THIS CLASS TO HANDLE SAVING AND LOADING THE GAME
PUBLIC STATIC CLASS SO CAN BE CALLED FROM ANYWHERE
REDUCES CLUTTER IN GAMEMANAGER 
*/
public static class GameDataHandler {

    public static void NewGame(GameManagerScript gameManager, TerrainManagerScript terrainManager, ushort terrainType) 
    {
        //Debug.Log(terrainType);
        if (!gameManager.worldPresent)
        {
            gameManager.readyToGo = false;

            gameManager.ui.SetActive(true);
            //gameManager.MainMenu.SetActive(false);

            gameManager.mainCameraObject.gameObject.SetActive(false);

            //terrainManager.StartTerrainGen(terrainType);

            gameManager.player.gameObject.SetActive(true);
            gameManager.player.GetComponent<SpriteRenderer>().sortingOrder = (int)EnumClass.LayerIDEnum.FRONTLAYER;
            gameManager.player.transform.SetParent(GameObject.Find("PlayArea").transform);
            //gameManager.playerPos = terrainManager.GetSafePlaceToSpawnPlayer();
            gameManager.playerPos = new Vector2(-10f, 0f);
            gameManager.player.transform.position = gameManager.playerPos;

            gameManager.mainCameraObject.gameObject.SetActive(true);
            gameManager.mainCameraObject.gameObject.GetComponent<CameraScript>().SetCamera(gameManager.player.transform, terrainType);

            //gameManager.uiScript.FadeInScene();

            gameManager.worldPresent = true;
            gameManager.readyToGo = true;
        }
    }

    public static void SaveGame(string path,GameManagerScript gameManager, TerrainManagerScript terrainManager)
    {
        //CHECK IF THERE IS A WORLD RUNNING & CALL SAVAMANAGER
        if (gameManager.worldPresent)
        {
            gameManager.readyToGo = false;
            Debug.Log("Saving game...");
            SaveManager.SaveGame(
                path,
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

    public static bool LoadGame(string path, GameManagerScript gameManager, TerrainManagerScript terrainManager)
    {
        //LOAD ONLY IF THERE IS NO WORLD PRESENT
        if (!gameManager.worldPresent)
        {
            gameManager.readyToGo = false;

            gameManager.mainCameraObject.gameObject.SetActive(false);
            gameManager.player.SetActive(false);


            SerializedSaveData loadedData = SaveManager.LoadGame(path);
            if (loadedData != null){
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

                gameManager.mainCameraObject.gameObject.SetActive(true);
                gameManager.mainCameraObject.gameObject.GetComponent<CameraScript>().SetCamera(gameManager.player.transform, gameManager.currentWorld);

                //gameManager.uIScript.FadeInScene();

                gameManager.worldPresent = true;
                gameManager.readyToGo = true;

                return true;
            }
            else
            {
                Debug.LogError("LOAD PATH CANNOT BE FOUND");
                return false;
            }
        }
        return false;
    }

    public static void ClearWorld(GameManagerScript gameManager, TerrainManagerScript terrainManager)
    {
        terrainManager.ClearTerrain();
        gameManager.worldPresent = false;
    }

}
