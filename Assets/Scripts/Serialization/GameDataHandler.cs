using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
USE THIS CLASS TO HANDLE SAVING AND LOADING THE GAME
PUBLIC STATIC CLASS SO CAN BE CALLED FROM ANYWHERE
REDUCES CLUTTER IN GAMEMANAGER 
*/
public static class GameDataHandler {

    public static void HandleTerrainDataGeneration(GameManagerScript gameManager, TheImmortalScript immortal, TerrainManagerScript terrainManager)
    {
        switch (gameManager.currentWorld)
        {
            case EnumClass.TerrainType.GREEN:
                if (immortal.GreenWorldStatus == TheImmortalScript.TerrainStatus.GENERATED)
                {
                    LoadTerrain(immortal.GreenWorldSavePath, terrainManager);
                    gameManager.playerPos = immortal.PlayerLandPosInGreen;
                }
                else
                {
                    GenerateNewTerrain(gameManager, EnumClass.TerrainType.GREEN, terrainManager);
                }
                break;
            case EnumClass.TerrainType.MOON:
                if (immortal.MoonWorldStatus == TheImmortalScript.TerrainStatus.GENERATED)
                {
                    LoadTerrain(immortal.MoonWorldSavePath, terrainManager);
                    gameManager.playerPos = immortal.PlayerLandPosInMoon;

                }
                else
                {
                    GenerateNewTerrain(gameManager, EnumClass.TerrainType.MOON, terrainManager);
                }
                break;
            case EnumClass.TerrainType.SNOW:
                if (immortal.SnowWorldStatus == TheImmortalScript.TerrainStatus.GENERATED)
                {
                    LoadTerrain(immortal.SnowWorldSavePath, terrainManager);
                    gameManager.playerPos = immortal.PlayerLandPosInSnow;
                }
                else
                {
                    GenerateNewTerrain(gameManager, EnumClass.TerrainType.SNOW, terrainManager);
                }
                break;
            case EnumClass.TerrainType.DESERT:
                if (immortal.DesertWorldStatus == TheImmortalScript.TerrainStatus.GENERATED)
                {
                    LoadTerrain(immortal.DesertWorldSavePath, terrainManager);
                    gameManager.playerPos = immortal.PlayerLandPosInDesert;
                }
                else
                {
                    GenerateNewTerrain(gameManager, EnumClass.TerrainType.DESERT, terrainManager);
                }
                break;
            default:
                Debug.Log("game data handler handleterraindatagen called with invalid terraintype");
                break;
        }

    }

    private static void GenerateNewTerrain(GameManagerScript gameManager, EnumClass.TerrainType terrainType, TerrainManagerScript terrainManager)
    {
        terrainManager.StartTerrainGen(terrainType);
        gameManager.playerPos = terrainManager.GetSafePlaceToSpawnPlayer();
    }

    private static void LoadTerrain(string path, TerrainManagerScript terrainManager)
    {
        SerializedTerrainData data = SaveManager.LoadTerrain(path);
        terrainManager.SetTiles(data.ftileData, data.fResourceData, data.btileData, data.bResourceData, data.vtileData);
    }

    public static void HandleTerrainSaving(GameManagerScript gameManager, TerrainManagerScript terrainManager, TheImmortalScript immortal)
    {
        switch (gameManager.currentWorld)
        {
            case EnumClass.TerrainType.GREEN:
                SaveTerrain(immortal.GreenWorldSavePath, gameManager, terrainManager, immortal);
                immortal.GreenWorldStatus = TheImmortalScript.TerrainStatus.GENERATED;
                immortal.PlayerLandPosInGreen = gameManager.player.transform.position;
                break;
            case EnumClass.TerrainType.MOON:
                SaveTerrain(immortal.MoonWorldSavePath, gameManager, terrainManager, immortal);
                immortal.MoonWorldStatus = TheImmortalScript.TerrainStatus.GENERATED;
                immortal.PlayerLandPosInMoon = gameManager.player.transform.position;
                break;
            case EnumClass.TerrainType.SNOW:
                SaveTerrain(immortal.SnowWorldSavePath, gameManager, terrainManager, immortal);
                immortal.SnowWorldStatus = TheImmortalScript.TerrainStatus.GENERATED;
                immortal.PlayerLandPosInSnow = gameManager.player.transform.position;
                break;
            case EnumClass.TerrainType.DESERT:
                SaveTerrain(immortal.DesertWorldSavePath, gameManager, terrainManager, immortal);
                immortal.DesertWorldStatus = TheImmortalScript.TerrainStatus.GENERATED;
                immortal.PlayerLandPosInDesert = gameManager.player.transform.position;
                break;
            case EnumClass.TerrainType.ASTEROID:
                break;
            case EnumClass.TerrainType.SHIP:
                break;
            default:
                break;
        }
    }

    private static void SaveTerrain(string path, GameManagerScript gameManager, TerrainManagerScript terrainManager, TheImmortalScript immortal)
    {
        SaveManager.SaveTerrain(
            path,
            terrainManager.frontTilesValue,
            terrainManager.frontTilesResourceValue,
            terrainManager.backTilesValue,
            terrainManager.backTilesResourceValue,
            terrainManager.vegetationTilesValue);
    }


    //public static void NewGame(GameManagerScript gameManager, TerrainManagerScript terrainManager, ushort terrainType) 
    //{
    //    //Debug.Log(terrainType);
    //    if (!gameManager.worldPresent)
    //    {
    //        gameManager.readyToGo = false;

    //        gameManager.player.gameObject.SetActive(true);
    //        gameManager.player.GetComponent<SpriteRenderer>().sortingOrder = (int)EnumClass.LayerIDEnum.FRONTLAYER;
    //        gameManager.player.transform.SetParent(GameObject.Find("PlayArea").transform);

    //        if(terrainType != (ushort)EnumClass.TerrainType.SHIP)
    //        {
    //            terrainManager.StartTerrainGen(terrainType);
    //            gameManager.playerPos = terrainManager.GetSafePlaceToSpawnPlayer();
    //        }
    //        else
    //        {
    //            gameManager.ship.transform.position = new Vector2(0f, 0f);
    //            gameManager.playerPos = gameManager.ship.GetComponent<ShipScript>().spawnPosition.position;
    //        }

    //        gameManager.player.transform.position = gameManager.playerPos;

    //        gameManager.mainCameraObject.gameObject.SetActive(true);
    //        gameManager.mainCameraObject.gameObject.GetComponent<CameraScript>().SetCamera(gameManager.player.transform, terrainType);


    //        gameManager.worldPresent = true;
    //        gameManager.readyToGo = true;
    //    }
    //}

    //public static void SaveGame(string path,GameManagerScript gameManager, TerrainManagerScript terrainManager)
    //{
    //    //CHECK IF THERE IS A WORLD RUNNING & CALL SAVAMANAGER
    //    if (gameManager.worldPresent)
    //    {
    //        gameManager.readyToGo = false;
    //        Debug.Log("Saving game...");
    //        SaveManager.SaveGame(
    //            path,
    //            terrainManager.frontTilesValue,
    //            terrainManager.frontTilesResourceValue,
    //            terrainManager.backTilesValue,
    //            terrainManager.backTilesResourceValue,
    //            terrainManager.vegetationTilesValue,
    //            gameManager.player.transform.position);
    //        Debug.Log("Game saved!");
    //        gameManager.readyToGo = true;
    //    }
    //}

    /*public static bool LoadGame(string path, GameManagerScript gameManager, TerrainManagerScript terrainManager)
    {
        
        //LOAD ONLY IF THERE IS NO WORLD PRESENT
        if (!gameManager.worldPresent)
        {
            gameManager.readyToGo = false;

            gameManager.mainCameraObject.gameObject.SetActive(false);
            gameManager.player.SetActive(false);


            SerializedTerrainData loadedData = SaveManager.LoadGame(path);
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
*/

public static void ClearWorld(GameManagerScript gameManager, TerrainManagerScript terrainManager)
    {
        terrainManager.ClearTerrain();
        gameManager.worldPresent = false;
    }

}
