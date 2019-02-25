using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager {

    private static string path;
    

    public static void SaveGame (
        string savePath,
        ushort[,] fTilesValue, 
        ushort[,] fRValue, 
        ushort[,] bTilesValue, 
        ushort[,] bRValue, 
        ushort[,] vTilesValue,
        Vector3 playerPos
        )
    {
        path = Application.persistentDataPath + "/" + savePath;

        SerializedSaveData data = new SerializedSaveData(fTilesValue, fRValue, bTilesValue, bRValue, vTilesValue, playerPos);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;

        stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static SerializedSaveData LoadGame(string loadPath)
    {
        path = Application.persistentDataPath + "/" + loadPath;

        if (File.Exists(path))
        {
            SerializedSaveData data;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream;

            stream = new FileStream(path, FileMode.Open);
            data = (SerializedSaveData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
        
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

