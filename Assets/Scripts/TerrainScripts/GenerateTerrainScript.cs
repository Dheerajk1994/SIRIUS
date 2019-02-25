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

    //private GameObject[,] chunkTiles;
    //private GameObject[] chunkArray;

    public ushort xDimension;
    public float smoothness;
    public int heightMultiple;
    public ushort heightAddition;

    //public int chunkSize;

    public int backTileLayerID;
    public int frontTileLayerID;
    public int grassLayerID;

    public int caveChanceVal;
    public int caveSimRep;
    public int caveMinNeighReq;
    public int caveMaxNeighReq;
    public float caveChangeInHeight;

    public int stoneChanceVal;                      //CHANCE FOR A STONE TO GENERATE 
    public int stoneSimRep;                         //HOW MANY TIMES IT RUNS
    public int stoneMinNeighReq;                       //NEIGHBORS REQUIRED TO REMAIN 1
    public int stoneMaxNeighReq;                       // max NEIGHBORS REQUIRED TO REMAIN 1
    public float stoneChangeInHeight;               //SHOULD THERE BE MORE STONE AS WE GO DEEPER

    public int coalChance;
    public int coalNeighChance;
    public float coalChangeInHeight;

    public int copperChance;
    public int copperNeighChance;
    public float copperChangeInHeight;

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

    private ushort DIRT_ID, STONE_ID, SAND_ID, GRASS_ID, FLOWER_ID, TREE_CORE_ID, TREE_TOP_ID;
    private ushort worldXDimension, worldYDimension;

    public void StartTerrainGeneration(TerrainManagerScript terrainManager, ushort xDim, ushort heightA, ushort chunkSize, ushort terrainType)
    {
        this.heightAddition = heightA;
        Debug.Log(xDim + " % " + chunkSize + ": " + xDim % chunkSize);
        //xDimension = (ushort)(xDim - (xDim - (chunkSize * (Mathf.Floor(xDim / chunkSize)))));   //MAKE SURE XDIMENSION IS DIVISIBLE BY CHUNKSIZE
        xDimension = (ushort)(xDim - (xDim % chunkSize));
        worldXDimension = xDimension;
        worldYDimension = (ushort)(heightAddition + 50);

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

    private void GenerateIDs(ushort terrainType)
    {
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
                Debug.LogError("Desert biome generation not set.");
                break;
            case (ushort)EnumClass.TerrainType.SNOW:
                Debug.LogError("Snow biome generation not set.");
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
        vegetationTilesValue[x, y] = 17;
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
