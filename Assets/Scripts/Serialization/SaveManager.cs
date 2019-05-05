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

