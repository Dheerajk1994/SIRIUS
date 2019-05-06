using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager {

    private static string path;
    private static string saveGamePath = Application.persistentDataPath + "/savedGame";
    

    public static void SaveTerrain (
        string savePath,
        ushort[,] fTilesValue, 
        ushort[,] fRValue, 
        ushort[,] bTilesValue, 
        ushort[,] bRValue, 
        ushort[,] vTilesValue
        )
    {
        path = Application.persistentDataPath + "/" + savePath;

        SerializedTerrainData data = new SerializedTerrainData(fTilesValue, fRValue, bTilesValue, bRValue, vTilesValue);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;

        stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SerializedTerrainData LoadTerrain(string loadPath)
    {
        path = Application.persistentDataPath + "/" + loadPath;

        if (File.Exists(path))
        {
            SerializedTerrainData data;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream;

            stream = new FileStream(path, FileMode.Open);
            data = (SerializedTerrainData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
        
    }

    public static void SaveGame(TheImmortalScript theImmortal)
    {
        SerializedImmortalData immortalData = new SerializedImmortalData(
            (int)theImmortal.WorldTypeToGenerate, 
            (int)theImmortal.TerrainGenerated, 
            theImmortal.IsNewGame,
            theImmortal.PlayerInventoryItems, 
            theImmortal.PlayerHotbarItems, 
            theImmortal.ShipInventoryItems, 
            theImmortal.ShipFuelAmount, 
            theImmortal.QuestsCompleted, 
            theImmortal.ActiveQuests,
            theImmortal.DialoguesCompleted,
            (int)theImmortal.GreenWorldStatus,
            (int)theImmortal.MoonWorldStatus,
            (int)theImmortal.SnowWorldStatus,
            (int)theImmortal.DesertWorldStatus,
            theImmortal.PlayerLandPosInGreen,
            theImmortal.PlayerLandPosInMoon,
            theImmortal.PlayerLandPosInSnow,
            theImmortal.PlayerLandPosInDesert);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;

        stream = new FileStream(saveGamePath, FileMode.Create);
        formatter.Serialize(stream, immortalData);
        stream.Close();
    }

    public static SerializedImmortalData LoadGame()
    {
        if (File.Exists(saveGamePath))
        {
            SerializedImmortalData immortalData;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream;

            stream = new FileStream(saveGamePath, FileMode.Open);
            immortalData = (SerializedImmortalData)formatter.Deserialize(stream);
            stream.Close();

            return immortalData;
        }
        else
        {
            return null;
        }
    }

}


[Serializable]
public class SerializedTerrainData
{
    public ushort[,] ftileData;
    public ushort[,] fResourceData; 

    public ushort[,] btileData;
    public ushort[,] bResourceData;

    public ushort[,] vtileData;

    //CONSTRUCTOR
    public SerializedTerrainData(
        ushort[,] ftileValues, 
        ushort[,] fRValues, 
        ushort[,] btileValues, 
        ushort[,] bRValues, 
        ushort[,] vtileValues
        )
    {
        ftileData = ftileValues;
        fResourceData = fRValues;

        btileData = btileValues;
        bResourceData = bRValues;

        vtileData = vtileValues;
    }
}

[Serializable]
public class SerializedImmortalData
{
    public int worldTypeToGenerate;
    public int terrainGenerated;
    public bool isNewGame;

    public ushort[,] playerInventoryItems;
    public ushort[,] playerHotbarItems;

    public ushort[,] shipInventoryItems;
    public int shipFuelAmount;

    public List<int> questsCompleted;
    public List<int> activeQuests;
    public List<int> dialoguesCompleted;

    public int greenWorldStatus;
    public int moonWorldStatus;
    public int snowWorldStatus;
    public int desertWorldStatus;

    public int playerLandPosInGreenX;
    public int playerLandPosInGreenY;

    public int playerLandPosInMoonX;
    public int playerLandPosInMoonY;

    public int playerLandPosInSnowX;
    public int playerLandPosInSnowY;

    public int playerLandPosInDesertX;
    public int playerLandPosInDesertY;

    public SerializedImmortalData(int worldTypeToGenerate,
        int terrainGenerated,
        bool isNewGame, 
        ushort[,] playerInventoryItems, 
        ushort[,] playerHotbarItems, 
        ushort[,] shipInventoryItems, 
        int shipFuelAmount,
        List<int> questsCompleted,
        List<int> activeQuests,
        List<int> dialoguesCompleted,
        int greenWorldStatus,
        int moonWorldStatus, 
        int snowWorldStatus, 
        int desertWorldStatus,
        Vector2 playerLandPosInGreen,
        Vector2 playerLandPosInMoon,
        Vector2 playerLandPosInSnow, 
        Vector2 playerLandPosInDesert)
    {
        this.worldTypeToGenerate = worldTypeToGenerate;
        this.terrainGenerated = terrainGenerated;
        this.isNewGame = isNewGame;
        this.playerInventoryItems = playerInventoryItems;
        this.playerHotbarItems = playerHotbarItems;
        this.shipInventoryItems = shipInventoryItems;
        this.shipFuelAmount = shipFuelAmount;
        this.questsCompleted = questsCompleted;
        this.activeQuests = activeQuests;
        this.dialoguesCompleted = dialoguesCompleted;
        this.greenWorldStatus = greenWorldStatus;
        this.moonWorldStatus = moonWorldStatus;
        this.snowWorldStatus = snowWorldStatus;
        this.desertWorldStatus = desertWorldStatus;

        this.playerLandPosInGreenX = (int)playerLandPosInGreen.x;
        this.playerLandPosInGreenY = (int)playerLandPosInGreen.y;

        this.playerLandPosInMoonX = (int)playerLandPosInMoon.x;
        this.playerLandPosInMoonY = (int)playerLandPosInMoon.y;

        this.playerLandPosInSnowX = (int)playerLandPosInSnow.x;
        this.playerLandPosInSnowY = (int)playerLandPosInSnow.y;

        this.playerLandPosInDesertX = (int)playerLandPosInDesert.x;
        this.playerLandPosInDesertY = (int)playerLandPosInDesert.y;
    }
}