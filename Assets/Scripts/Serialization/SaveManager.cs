using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager {

    private static readonly string save1Path = Application.persistentDataPath + "/save1.data";
    

    public static void SaveGame (ushort [,] frontTilesValue, ushort[,] backTilesValue, ushort[,] vegetationTilesValue)
    {
        SerializedSaveData data = new SerializedSaveData(frontTilesValue, backTilesValue, vegetationTilesValue);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;

        stream = new FileStream(save1Path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static void LoadGame(TerrainManagerScript tScript)
    {
        SerializedSaveData data;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;

        stream = new FileStream(save1Path, FileMode.Open);
        data   = (SerializedSaveData)formatter.Deserialize(stream);
        stream.Close();

        tScript.SetTiles(data.ftileData, data.btileData, data.vtileData);

    }
}

//POTENTIAL OPTIMIZATION IDEA : CREATE 3 ARRAYS IN THIS TILEDATA CLASS AND JUST USE THAT
//INSTEAD OF CRATING 3 SEPARATE INSTANCES OF THE CLASS
[Serializable]
public class SerializedSaveData
{
    ushort playerPosX, playerPosY;

    public ushort[,] ftileData;
    public ushort[,] btileData;
    public ushort[,] vtileData;

    public SerializedSaveData(ushort[,] ftileValues, ushort[,] btileValues, ushort[,] vtileValues)
    {
        ftileData = ftileValues;
        btileData = btileValues;
        vtileData = vtileValues;
    }
}

