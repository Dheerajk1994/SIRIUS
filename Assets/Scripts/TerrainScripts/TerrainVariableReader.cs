using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class TerrainVariableReader  {
    private static string path = Application.streamingAssetsPath + "/terrainVariables.json";
    private static TerrainList listOfTerrainVariables;

    public static TerrainInfo GetTerrainInfo(ushort terrainTypeEnum)
    {
        listOfTerrainVariables = new TerrainList();
        try
        {
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                listOfTerrainVariables = JsonUtility.FromJson<TerrainList>(jsonString);

                foreach(TerrainInfo terrainInfo in listOfTerrainVariables.Terrains)
                {
                    if(terrainInfo.terrainType == terrainTypeEnum)
                    {
                        return terrainInfo;
                    }
                }

            }
        }
        catch(Exception e)
        {
            throw e;
        }
        return null;
    }
}

[System.Serializable]
public class TerrainList
{
    public List<TerrainInfo> Terrains;
}

[System.Serializable]
public class TerrainInfo
{
    public ushort terrainType;
    public string terrainTypeName;

    public int heightMultiple;
    public float smoothness;

    public int caveChanceVal;
    public int caveSimRep;
    public int caveMinNeighReq;
    public int caveMaxNeighReq;
    public float caveChangeInHeight;

    public int stoneChanceVal;                     
    public int stoneSimRep;                        
    public int stoneMinNeighReq;                   
    public int stoneMaxNeighReq;                   
    public float stoneChangeInHeight;              

    public int coalChance;
    public int coalNeighChance;
    public float coalChangeInHeight;

    public int copperChance;
    public int copperNeighChance;
    public float copperChangeInHeight;

    public int ironChance;
    public int ironNeighChance;
    public float ironChangeInHeight;

    public int silverChance;
    public int silverNeighChance;
    public float silverChangeInHeight;

    public int goldChance;
    public int goldNeighChance;
    public float goldChangeInHeight;

    public int diamondChance;
    public int diamondNeighChance;
    public float diamondChangeInHeight;

    public int flowerChance;
    public int treeChance;
    public int minTreeHeight;
    public int maxTreeHeight;

   
}
