using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateTerrainScript : MonoBehaviour
{
    private ushort[,] frontTilesValue;
    private ushort[,] backTilesValue;
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


    public void StartTerrainGeneration(TerrainManagerScript terrainManager, ushort xDim, ushort heightA, ushort chunkSize)
    {
        this.heightAddition = heightA;
        Debug.Log(xDim + " % " + chunkSize + ": " + xDim % chunkSize);
        //xDimension = (ushort)(xDim - (xDim - (chunkSize * (Mathf.Floor(xDim / chunkSize)))));   //MAKE SURE XDIMENSION IS DIVISIBLE BY CHUNKSIZE
        xDimension = (ushort)(xDim - (xDim % chunkSize));

        frontTilesValue      = new ushort[xDimension, heightAddition + 50];
        backTilesValue       = new ushort[xDimension, heightAddition + 50];
        vegetationTilesValue = new ushort[xDimension, heightAddition + 50];

        GenerateTerrainNoise();
        GenerateStone();
              
        GenerateResources(21, coalChance, coalNeighChance, coalChangeInHeight);
        GenerateResources(22, copperChance, copperNeighChance, copperChangeInHeight);
        GenerateResources(23, silverChance, silverNeighChance, silverChangeInHeight);
        GenerateResources(24, goldChance, goldNeighChance, goldChangeInHeight);
        GenerateResources(25, diamondChance, diamondNeighChance, diamondChangeInHeight);

        GenerateGrass();
        GenerateCaves();

        //GenerateChunk();

        //CreateTileGameobjects();

        terrainManager.SetTiles(frontTilesValue, backTilesValue, vegetationTilesValue);

    }


    private void GenerateTerrainNoise()
    {
        for (int x = 0; x < xDimension; x++)
        {
            float xOff = x * 1.0f;


            float height = Mathf.RoundToInt(Mathf.PerlinNoise(xOff / smoothness, xOff / smoothness) * heightMultiple) + heightAddition;


            for (int y = 0; y < height; y++)
            {
                frontTilesValue[x, y] = 1;
                backTilesValue[x, y] = 1;

            }
        }
    }

    private void GenerateStone()
    {
        byte[,] stoneArray = new byte[frontTilesValue.GetLength(0), frontTilesValue.GetLength(1)];

        for (int x = 0; x < frontTilesValue.GetLength(0); x++)
        {
            for (int y = 0; y < frontTilesValue.GetLength(1); y++)
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

            for (int x = 1; x < frontTilesValue.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < frontTilesValue.GetLength(1) - 1; y++)
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

        for (int x = 0; x < frontTilesValue.GetLength(0); x++)
        {
            for (int y = 0; y < frontTilesValue.GetLength(1); y++)
            {
                if (stoneArray[x, y] == 1)
                {
                    frontTilesValue [x, y] = 2;     //2 for stone
                    backTilesValue[x, y] = 2;       //2 for stone
                }
            }
        }

    }

    private void GenerateGrass()
    {
        for (int x = 0; x < frontTilesValue.GetLength(0); x++)
        {
            for (int y = 0; y < frontTilesValue.GetLength(1); y++)
            {
                if ((y + 1) < frontTilesValue.GetLength(1) && frontTilesValue[x, y + 1] == 0 && frontTilesValue[x, y] != 0)
                {
                    vegetationTilesValue[x, y + 1] = 19;
                    vegetationTilesValue[x, y] = 20;

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
            vegetationTilesValue[x, y + 1 + i] = 17;
        }
        vegetationTilesValue[x, y + height] = 18;

    }

    private void GenerateCaves()
    {
        byte[,] caveArray = new byte[frontTilesValue.GetLength(0), frontTilesValue.GetLength(1)];

        for (int x = 0; x < frontTilesValue.GetLength(0); x++)
        {
            for (int y = 0; y < frontTilesValue.GetLength(1); y++)
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

            for (int x = 1; x < frontTilesValue.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < frontTilesValue.GetLength(1) - 1; y++)
                {
                    if (/*StoneChance[x,y] == 0 ||*/ frontTilesValue[x, y] == 0) continue;

                    neighborCount = GetNeighBorhood(caveArray, x, y, 1);


                    if (neighborCount > stoneMinNeighReq)// && neighborCount <= stoneMaxNeighReq)
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

        for (int x = 0; x < frontTilesValue.GetLength(0); x++)
        {
            for (int y = 0; y < frontTilesValue.GetLength(1); y++)
            {
                if (caveArray[x, y] == 1)
                {
                    frontTilesValue[x, y] = 0;
                    vegetationTilesValue[x, y] = 0;
                }
            }
        }

    }

    private void GenerateResources(ushort resourceID, int resourceChance, int resourceNeighChance, float resourceChangeInHeight)
    {
        byte[,] frontTileResourceArray = new byte[frontTilesValue.GetLength(0), frontTilesValue.GetLength(1)];
        byte[,] backtTileResourceArray = new byte[frontTilesValue.GetLength(0), frontTilesValue.GetLength(1)];
        for (int x = 0; x < frontTilesValue.GetLength(0); x++)
        {
            for (int y = 0; y < frontTilesValue.GetLength(1); y++)
            {
                if (frontTilesValue[x, y]  == 2)
                {
                    if (UnityEngine.Random.Range(1, 100) <= (resourceChance - y * resourceChangeInHeight))
                    {
                        frontTileResourceArray[x, y] = 1;
                    }
                    if (UnityEngine.Random.Range(1, 100) <= (resourceChance - y * resourceChangeInHeight))
                    {
                        backtTileResourceArray[x, y] = 1;
                    }
                }
            }
        }

        for (int x = 0; x < frontTileResourceArray.GetLength(0); x++)
        {
            for (int y = 0; y < frontTileResourceArray.GetLength(1); y++)
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

        for (int x = 0; x < frontTileResourceArray.GetLength(0); x++)
        {
            for (int y = 0; y < frontTileResourceArray.GetLength(1); y++)
            {
                if (frontTileResourceArray[x, y] == 1)
                {
                    frontTilesValue[x, y] = resourceID;
                }
                if (backtTileResourceArray[x, y] == 1)
                {
                    backTilesValue[x, y] = resourceID;
                }

            }
        }
    }

    //private void GenerateChunk()
    //{
    //    int chunkArrayIndex = 0;
    //    Debug.Log(frontTiles.GetLength(0) + " " + frontTiles.GetLength(1));
    //    for (int x = 0; x < frontTiles.GetLength(0); x++)
    //    {
    //        for (int y = 0; y < frontTiles.GetLength(1); y++)
    //        {
    //            if (frontTiles[x, y] != null)
    //            {
    //                frontTiles[x, y].gameObject.transform.parent = chunkArray[chunkArrayIndex].transform;// SetParent(chunkArray[chunkArrayIndex].transform);
    //            }
    //        }
    //        if (x != 0 && x % chunkSize == 0)
    //        {
    //            chunkArrayIndex++;
    //        }
    //    }
    //}

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
