using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateTerrainScript : MonoBehaviour
{
    private ushort[,] frontTilesValue;
    private ushort[,] frontTilesResourceValues;
    private ushort[,] backTilesValue;
    private ushort[,] backTilesResourceValue;
    private ushort[,] vegetationTilesValue;

    //public int backTileLayerID;
    //public int frontTileLayerID;
    //public int grassLayerID;

    private ushort xDimension;
    private float smoothness;
    private int heightMultiple;
    private ushort heightAddition;

    private int caveChanceVal;
    private int caveSimRep;
    private int caveMinNeighReq;
    private int caveMaxNeighReq;
    private float caveChangeInHeight;
    
    private int stoneChanceVal;                      
    private int stoneSimRep;                         
    private int stoneMinNeighReq;                    
    private int stoneMaxNeighReq;                    
    private float stoneChangeInHeight;               
    
    private int coalChance;
    private int coalNeighChance;
    private float coalChangeInHeight;
    
    private int copperChance;
    private int copperNeighChance;
    private float copperChangeInHeight;

    private int ironChance;
    private int ironNeighChance;
    private float ironChangeInHeight;

    private int silverChance;
    private int silverNeighChance;
    private float silverChangeInHeight;
    
    private int goldChance;
    private int goldNeighChance;
    private float goldChangeInHeight;
    
    private int diamondChance;
    private int diamondNeighChance;
    private float diamondChangeInHeight;
    
    private int flowerChance;
    private int treeChance;
    private int minTreeHeight;
    private int maxTreeHeight;

    private ushort DIRT_ID, STONE_ID, SAND_ID, GRASS_ID, FLOWER_ID, TREE_CORE_ID, TREE_TOP_ID;
    private ushort worldXDimension, worldYDimension;

    public void StartTerrainGeneration(TerrainManagerScript terrainManager, ushort xDim, ushort heightA, ushort chunkSize, ushort terrainType)
    {
        //Debug.Log(terrainType);
        this.heightAddition = heightA;
        xDimension = (ushort)(xDim - (xDim % chunkSize));
        worldXDimension = xDimension;
        worldYDimension = (ushort)(heightAddition + 50);

        SetVariables(terrainType);

        frontTilesValue              = new ushort[worldXDimension, worldYDimension];
        frontTilesResourceValues     = new ushort[worldXDimension, worldYDimension];
        backTilesValue               = new ushort[worldXDimension, worldYDimension];
        backTilesResourceValue       = new ushort[worldXDimension, worldYDimension];
        vegetationTilesValue         = new ushort[worldXDimension, worldYDimension];

        GenerateIDs(terrainType);

        GenerateTerrainNoise();
        GenerateStone();

        GenerateResources((ushort)EnumClass.TileEnum.COAL, coalChance, coalNeighChance, coalChangeInHeight);
        GenerateResources((ushort)EnumClass.TileEnum.COPPER, copperChance, copperNeighChance, copperChangeInHeight);
        GenerateResources((ushort)EnumClass.TileEnum.SILVER, silverChance, silverNeighChance, silverChangeInHeight);
        GenerateResources((ushort)EnumClass.TileEnum.GOLD, goldChance, goldNeighChance, goldChangeInHeight);
        GenerateResources((ushort)EnumClass.TileEnum.DIAMOND, diamondChance, diamondNeighChance, diamondChangeInHeight);


        if(terrainType != (ushort)EnumClass.TerrainType.MOON && terrainType != (ushort)EnumClass.TerrainType.ASTEROID)
        {
            GenerateGrass();
        }
        GenerateCaves();

        terrainManager.SetTiles(frontTilesValue, frontTilesResourceValues, backTilesValue, backTilesResourceValue, vegetationTilesValue);
    }

    private void SetVariables(ushort terrainType)
    {
        TerrainInfo terrain = TerrainVariableReader.GetTerrainInfo(terrainType);
        //Debug.Log(terrainType);
        if (terrain != null)
        {
            this.heightMultiple = terrain.heightMultiple;
            this.smoothness = terrain.smoothness;
            
            this.caveChanceVal = terrain.caveChanceVal;
            this.caveSimRep = terrain.caveSimRep;
            this.caveMinNeighReq = terrain.caveMinNeighReq;
            this.caveMaxNeighReq = terrain.caveMaxNeighReq;
            this.caveChangeInHeight = terrain.caveChangeInHeight;
            
            this.stoneChanceVal = terrain.stoneChanceVal;
            this.stoneSimRep = terrain.stoneSimRep;
            this.stoneMinNeighReq = terrain.stoneMinNeighReq;
            this.stoneMaxNeighReq = terrain.stoneMaxNeighReq;
            this.stoneChangeInHeight = terrain.stoneChangeInHeight;
            
            this.coalChance = terrain.coalChance;
            this.coalNeighChance = terrain.coalNeighChance;
            this.coalChangeInHeight = terrain.coalChangeInHeight;
            //Debug.Log("coal neigh " + coalNeighChance);
            
            this.copperChance = terrain.copperChance;
            this.copperNeighChance = terrain.copperNeighChance;
            this.copperChangeInHeight = terrain.copperChangeInHeight;


            this.ironChance = terrain.ironChance;
            this.ironNeighChance = terrain.ironNeighChance;
            this.ironChangeInHeight = terrain.ironChangeInHeight;
            
            this.silverChance = terrain.silverChance;
            this.silverNeighChance = terrain.silverNeighChance;
            this.silverChangeInHeight = terrain.silverChangeInHeight;
            
            this.goldChance = terrain.goldChance;
            this.goldNeighChance = terrain.goldNeighChance;
            this.goldChangeInHeight = terrain.goldChangeInHeight;
            
            this.diamondChance = terrain.diamondChance;
            this.diamondNeighChance = terrain.diamondNeighChance;
            this.diamondChangeInHeight = terrain.diamondChangeInHeight;
        
            this.flowerChance = terrain.flowerChance;
            this.treeChance = terrain.treeChance;
            this.minTreeHeight = terrain.minTreeHeight;
            this.maxTreeHeight = terrain.maxTreeHeight;
}
    }

    private void GenerateIDs(ushort terrainType)
    {
        //Debug.Log("TERRAIN TYPE: " + terrainType);
        switch (terrainType)
        {
            case (ushort)EnumClass.TerrainType.GREEN:
                DIRT_ID = (ushort)EnumClass.TileEnum.REGULAR_DIRT;
                STONE_ID = (ushort)EnumClass.TileEnum.REGULAR_STONE;
                SAND_ID = (ushort)EnumClass.TileEnum.REGULAR_SAND;
                GRASS_ID = (ushort)EnumClass.TileEnum.REGULAR_GRASS;
                FLOWER_ID = (ushort)EnumClass.TileEnum.REGULAR_FLOWER;
                TREE_CORE_ID = (ushort)EnumClass.TileEnum.REGULAR_TREETRUNK;
                TREE_TOP_ID = (ushort)EnumClass.TileEnum.REGULAR_TREELEAF;
                break;
            case (ushort)EnumClass.TerrainType.DESERT:
                DIRT_ID = (ushort)EnumClass.TileEnum.DESERT_DIRT;
                STONE_ID = (ushort)EnumClass.TileEnum.DESERT_STONE;
                SAND_ID = (ushort)EnumClass.TileEnum.REGULAR_SAND;
                GRASS_ID = (ushort)EnumClass.TileEnum.DESERT_GRASS;
                FLOWER_ID = (ushort)EnumClass.TileEnum.DESERT_FLOWER;
                TREE_CORE_ID = (ushort)EnumClass.TileEnum.DESERT_TREE_TRUNK;
                TREE_TOP_ID = (ushort)EnumClass.TileEnum.DESERT_TREE_LEAF;
                break;
            case (ushort)EnumClass.TerrainType.SNOW:
                DIRT_ID = (ushort)EnumClass.TileEnum.SNOW_DIRT;
                STONE_ID = (ushort)EnumClass.TileEnum.SNOW_STONE;
                SAND_ID = (ushort)EnumClass.TileEnum.REGULAR_SAND;
                GRASS_ID = (ushort)EnumClass.TileEnum.SNOW_GRASS;
                FLOWER_ID = (ushort)EnumClass.TileEnum.SNOW_FLOWER;
                TREE_CORE_ID = (ushort)EnumClass.TileEnum.SNOW_TREE_TRUNK;
                TREE_TOP_ID = (ushort)EnumClass.TileEnum.SNOW_TREE_LEAF;
                break;
            case (ushort)EnumClass.TerrainType.MOON:
                DIRT_ID = (ushort)EnumClass.TileEnum.MOON_DIRT;
                STONE_ID = (ushort)EnumClass.TileEnum.MOON_STONE;
                SAND_ID = (ushort)EnumClass.TileEnum.MOON_SAND;
                GRASS_ID = (ushort)EnumClass.TileEnum.REGULAR_GRASS;
                FLOWER_ID = (ushort)EnumClass.TileEnum.REGULAR_FLOWER;
                TREE_CORE_ID = (ushort)EnumClass.TileEnum.REGULAR_TREETRUNK;
                TREE_TOP_ID = (ushort)EnumClass.TileEnum.REGULAR_TREELEAF;
                break;
            case (ushort)EnumClass.TerrainType.ASTEROID:
                Debug.LogError("Asteroid biome generation not set.");
                break;
            default:
                break;
        }
    }

    private void GenerateTerrainNoise()
    {
        for (ushort x = 0; x < worldXDimension; x++)
        {
            float xOff = x * 1.0f;


            float height = Mathf.RoundToInt(Mathf.PerlinNoise(xOff / smoothness, xOff / smoothness) * heightMultiple) + heightAddition;


            for (ushort y = 0; y < height; y++)
            {
                frontTilesValue[x, y] = DIRT_ID;
                backTilesValue[x, y] = DIRT_ID;

            }
        }
    }

    private void GenerateStone()
    {
        byte[,] stoneArray = new byte[worldXDimension, worldYDimension];

        for (int x = 0; x < worldXDimension; x++)
        {
            for (int y = 0; y < worldYDimension; y++)
            {
                if (frontTilesValue[x, y] != 0)
                {
                    if (UnityEngine.Random.Range(1, 100) <= (stoneChanceVal - y * stoneChangeInHeight))
                    {
                        stoneArray[x, y] = 1;
                    }
                }
            }
        }

        for (int repetition = 0; repetition < stoneSimRep; repetition++)
        {
            int neighborCount = 0;

            for (int x = 1; x < worldXDimension - 1; x++)
            {
                for (int y = 1; y < worldYDimension - 1; y++)
                {
                    if (/*StoneChance[x,y] == 0 ||*/ frontTilesValue[x, y] == 0) continue;

                    neighborCount = GetNeighBorhood(stoneArray, x, y, 1);


                    if (neighborCount > stoneMinNeighReq)// && neighborCount <= stoneMaxNeighReq)
                    {
                        stoneArray[x, y] = 1;
                    }
                    else if (neighborCount < stoneMinNeighReq)
                    {
                        stoneArray[x, y] = 0;
                    }

                }
            }
        }

        for (int x = 0; x < worldXDimension; x++)
        {
            for (int y = 0; y < worldYDimension; y++)
            {
                if (stoneArray[x, y] == 1)
                {
                    frontTilesValue [x, y] = STONE_ID;     //2 for stone
                    backTilesValue[x, y] = STONE_ID;       //2 for stone
                }
            }
        }

    }

    private void GenerateGrass()
    {
        for (int x = 0; x < worldXDimension; ++x)
        {
            for (int y = 0; y < worldYDimension; ++y)
            {
                if ((y + 1) < worldYDimension && frontTilesValue[x, y + 1] == 0 && frontTilesValue[x, y] != 0)
                {
                    vegetationTilesValue[x, y] = GRASS_ID;
                    vegetationTilesValue[x, y + 1] = FLOWER_ID;

                    ushort chance = (ushort)UnityEngine.Random.Range(0, 100);
                    if (chance < treeChance)
                    {
                        CreateTree(x, y + 1);
                    }
                    break;
                }
            }
        }
    }

    private void CreateTree(int x, int y)
    {
        int height = UnityEngine.Random.Range(minTreeHeight, maxTreeHeight);
        vegetationTilesValue[x, y] = TREE_CORE_ID;
        for (int i = 0; i < height; i++)
        {
            vegetationTilesValue[x, y + 1 + i] = TREE_CORE_ID;
        }
        vegetationTilesValue[x, y + height] = TREE_TOP_ID;

    }

    private void GenerateCaves()
    {
        byte[,] caveArray = new byte[worldXDimension, worldYDimension];

        for (int x = 0; x < worldXDimension; x++)
        {
            for (int y = 0; y < worldYDimension; y++)
            {
                if (frontTilesValue[x, y] != 0)
                {
                    if (UnityEngine.Random.Range(1, 100) <= (caveChanceVal - y * caveChangeInHeight))
                    {
                        caveArray[x, y] = 1;
                    }
                }
            }
        }

        for (int repetition = 0; repetition < caveSimRep; repetition++)
        {
            int neighborCount = 0;

            for (int x = 1; x < worldXDimension  - 1; x++)
            {
                for (int y = 1; y < worldYDimension - 1; y++)
                {
                    if (frontTilesValue[x, y] == 0) continue;

                    neighborCount = GetNeighBorhood(caveArray, x, y, 1);


                    if (neighborCount > stoneMinNeighReq)
                    {
                        caveArray[x, y] = 1;
                    }
                    else if (neighborCount < stoneMinNeighReq)
                    {
                        caveArray[x, y] = 0;
                    }

                }
            }
        }

        for (int x = 0; x < worldXDimension; x++)
        {
            for (int y = 0; y < worldYDimension; y++)
            {
                if (caveArray[x, y] == 1)
                {
                    frontTilesValue[x, y] = 0;
                    frontTilesResourceValues[x, y] = 0;
                    vegetationTilesValue[x, y] = 0;
                }
            }
        }

    }

    private void GenerateResources(ushort resourceID, int resourceChance, int resourceNeighChance, float resourceChangeInHeight)
    {
        //Debug.Log("resource id " + resourceID);
        //Debug.Log("resource chance " + resourceChance);
        //Debug.Log("resource neigh chance " + resourceNeighChance);
        //Debug.Log("resource change in height " + resourceChangeInHeight);
        byte[,] frontTileResourceArray = new byte[frontTilesValue.GetLength(0), frontTilesValue.GetLength(1)];
        byte[,] backtTileResourceArray = new byte[frontTilesValue.GetLength(0), frontTilesValue.GetLength(1)];
        for (int x = 0; x < worldXDimension; x++)
        {
            for (int y = 0; y < worldYDimension; y++)
            {
                if (frontTilesValue[x,y] !=0 && UnityEngine.Random.Range(1, 100) <= (resourceChance - y * resourceChangeInHeight))
                {
                    frontTileResourceArray[x, y] = 1;
                }
                if (UnityEngine.Random.Range(1, 100) <= (resourceChance - y * resourceChangeInHeight))
                {
                    backtTileResourceArray[x, y] = 1;
                }
            }
        }

        for (int x = 0; x < worldXDimension; x++)
        {
            for (int y = 0; y < worldYDimension; y++)
            {
                if (frontTilesValue[x, y] != 0 && GetNeighBorhood(frontTileResourceArray, x, y, 1) > 1)
                {
                    if (UnityEngine.Random.Range(1, 100) <= resourceNeighChance)
                    {
                        frontTileResourceArray[x, y] = 1;
                    }
                    else
                    {
                        frontTileResourceArray[x, y] = 0;
                    }

                    if (UnityEngine.Random.Range(1, 100) <= resourceNeighChance)
                    {
                        backtTileResourceArray[x, y] = 1;
                    }
                    else
                    {
                        backtTileResourceArray[x, y] = 0;
                    }
                }
                else
                {
                    frontTileResourceArray[x, y] = 0;
                    backtTileResourceArray[x, y] = 0;
                }
            }
        }

        for (int x = 0; x < worldXDimension; x++)
        {
            for (int y = 0; y < worldYDimension; y++)
            {
                if (frontTileResourceArray[x, y] == 1)
                {
                    frontTilesResourceValues[x, y] = resourceID;
                    //Debug.Log("resource id set");
                }
                if (backtTileResourceArray[x, y] == 1)
                {
                    backTilesResourceValue[x, y] = resourceID;
                }

            }
        }
    }

    //HELPER FUNCTIONS

    private int GetNeighBorhood(byte[,] neighArray, int x, int y, int checkID)
    {
        int neighbors = 0;

        if ((x - 1) >= 0 && (y - 1) >= 0 && (x + 2) < neighArray.GetLength(0) && (y + 2) < neighArray.GetLength(1))
        {
            if (neighArray[x - 1, y - 1] == checkID)//left bottom
            {
                neighbors++;
            }
            if (neighArray[x - 1, y] == checkID)//left center
            {
                neighbors++;
            }
            if (neighArray[x - 1, y + 1] == checkID)//left top
            {
                neighbors++;
            }
            if (neighArray[x, y - 1] == checkID)//center bottom
            {
                neighbors++;
            }
            if (neighArray[x, y + 1] == checkID)//center top
            {
                neighbors++;
            }
            if (neighArray[x + 1, y - 1] == checkID)//right bottom
            {
                neighbors++;
            }
            if (neighArray[x + 1, y] == checkID)//right center
            {
                neighbors++;
            }
            if (neighArray[x + 1, y + 1] == checkID)//right top
            {
                neighbors++;
            }
        }
        return neighbors;
    }
}
