using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager {

    private static readonly string save1Path = Application.persistentDataPath + "/save1.data";
    

    public static void SaveGame (
        ushort[,] fTilesValue, 
        ushort[,] fRValue, 
        ushort[,] bTilesValue, 
        ushort[,] bRValue, 
        ushort[,] vTilesValue,
        Vector3 playerPos
        )
    {
        SerializedSaveData data = new SerializedSaveData(fTilesValue, fRValue, bTilesValue, bRValue, vTilesValue, playerPos);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;

        stream = new FileStream(save1Path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static SerializedSaveData LoadGame()
    {
        SerializedSaveData data;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;

        stream = new FileStream(save1Path, FileMode.Open);
        data   = (SerializedSaveData)formatter.Deserialize(stream);
        stream.Close();

        return data;
    }
}


[Serializable]
public class SerializedSaveData
{
    public float playerPosX, playerPosY, playerPosZ;

    public ushort[,] ftileData;
    public ushort[,] fResourceData; 

    public ushort[,] btileData;
    public ushort[,] bResourceData;

    public ushort[,] vtileData;

    //CONSTRUCTOR
    public SerializedSaveData(//-  
        ushort[,] ftileValues, 
        ushort[,] fRValues, 
        ushort[,] btileValues, 
        ushort[,] bRValues, 
        ushort[,] vtileValues,
        Vector3 playerPos
        )//-
    {
        ftileData = ftileValues;
        fResourceData = fRValues;

        btileData = btileValues;
        bResourceData = bRValues;

        vtileData = vtileValues;

        playerPosX = playerPos.x;
        playerPosY = playerPos.y;
        playerPosZ = playerPos.z;
    }
}

