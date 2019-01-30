using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateTerrainScript : MonoBehaviour
{

    public GameObject[] stoneObjects;
    public GameObject[] dirtObjects;
    public GameObject[] grass;
    public GameObject[] flowers;
    public GameObject tree1Base;
    public GameObject tree1Top;
    public GameObject[] tree1Core;


    public GameObject coal;
    public GameObject copper;
    public GameObject silver;
    public GameObject gold;
    public GameObject diamond;


    private GameObject[,] frontTiles;
    private GameObject[,] backTiles;

    private ushort[,] frontTilesValue;
    private ushort[,] backTilesValue;

    //private GameObject[,] chunkTiles;
    private GameObject[] chunkArray;

    public int xDimension;
    public float smoothness;
    public int heightMultiple;
    public int heightAddition;

    public int chunkSize;

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


    public void StartTerrainGeneration()
    {
        frontTiles = new GameObject[xDimension, heightAddition + 50];
        backTiles = new GameObject[xDimension, heightAddition + 50];

        frontTilesValue = new ushort[xDimension, heightAddition + 50];
        backTilesValue = new ushort[xDimension, heightAddition + 50];

        chunkArray = new GameObject[50];
        for (int i = 0; i < chunkArray.GetLength(0); i++)
        {
            chunkArray[i] = new GameObject("chunk" + i);
        }

        GenerateTerrainNoise();
        GenerateStone();
              
        GenerateResources(21, coalChance, coalNeighChance, coalChangeInHeight);
        GenerateResources(22, copperChance, copperNeighChance, copperChangeInHeight);
        GenerateResources(23, silverChance, silverNeighChance, silverChangeInHeight);
        GenerateResources(24, goldChance, goldNeighChance, goldChangeInHeight);
        GenerateResources(25, diamondChance, diamondNeighChance, diamondChangeInHeight);

        GenerateCaves();

        //GenerateGrass();

        GenerateChunk();

        CreateTileGameobjects();

        this.GetComponent<TerrainManagerScript>().SetFrontTiles(frontTiles);

    }


    private void GenerateTerrainNoise()
    {
        for (int x = 0; x < xDimension; x++)
        {
            float xOff = x * 1.0f;


            float height = Mathf.RoundToInt(Mathf.PerlinNoise(xOff / smoothness, xOff / smoothness) * heightMultiple) + heightAddition;


            for (int y = 0; y < height; y++)
            {
                //GameObject tile1 = Instantiate(dirtObjects[UnityEngine.Random.Range(0, dirtObjects.Length)], new Vector2(x, y), Quaternion.identity);
                //frontTiles[x, y] = tile1;

                //GameObject tile2 = Instantiate(dirtObjects[UnityEngine.Random.Range(0, dirtObjects.Length)], new Vector2(x, y), Quaternion.identity);
                //backTiles[x, y] = tile2;
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
                    //frontTiles[x, y] = Instantiate(stoneObjects[UnityEngine.Random.Range(0, dirtObjects.Length)], new Vector2(x, y), Quaternion.identity);
                    //backTiles[x, y] = Instantiate(stoneObjects[UnityEngine.Random.Range(0, dirtObjects.Length)], new Vector2(x, y), Quaternion.identity);
                    frontTilesValue [x, y] = 2;     //2 for stone
                    backTilesValue[x, y] = 2;       //2 for stone
                }
            }
        }

    }

    //private void GenerateGrass()
    //{
    //    for(int x = 0; x < frontTiles.GetLength(0); x++)
    //    {
    //        for(int y = 0; y < frontTiles.GetLength(1); y++)
    //        {
    //            if(frontTilesValue[x,y + 1] == 0 && frontTilesValue[x,y] != 0)
    //            {
    //                GameObject grassObj = Instantiate(grass[UnityEngine.Random.Range(0, 4)], new Vector2(x, y), Quaternion.identity);
    //                grassObj.transform.parent = frontTiles[x, y].transform;
    //                grassObj.GetComponent<SpriteRenderer>().sortingOrder = grassLayerID;

    //                GameObject flowerObj = Instantiate(flowers[UnityEngine.Random.Range(0, 4)], new Vector2(x, y + 1), Quaternion.identity);
    //                flowerObj.transform.parent = frontTiles[x, y].transform;
    //                flowerObj.GetComponent<SpriteRenderer>().sortingOrder = grassLayerID;

    //                int chance = UnityEngine.Random.Range(0, 100);
    //                if (chance < treeChance)
    //                {
    //                    CreateTree(x, y + 1);
    //                }
    //                break;
    //            }
    //        }
    //    }
    //}

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

    private void GenerateChunk()
    {
        int chunkArrayIndex = 0;
        Debug.Log(frontTiles.GetLength(0) + " " + frontTiles.GetLength(1));
        for (int x = 0; x < frontTiles.GetLength(0); x++)
        {
            for (int y = 0; y < frontTiles.GetLength(1); y++)
            {
                if (frontTiles[x, y] != null)
                {
                    frontTiles[x, y].gameObject.transform.parent = chunkArray[chunkArrayIndex].transform;// SetParent(chunkArray[chunkArrayIndex].transform);
                }
            }
            if (x != 0 && x % chunkSize == 0)
            {
                chunkArrayIndex++;
            }
        }
    }

    private void CreateTileGameobjects()
    {
        for (int x = 0; x < frontTilesValue.GetLength(0); x++)
        {
            for (int y = 0; y < frontTilesValue.GetLength(1); y++)
            {
                GameObject fTile = null;
                GameObject bTile = null;

                //FRONT TILES
                switch (frontTilesValue[x, y])
                {
                    case 0:
                        break;
                    case 1:
                        fTile = Instantiate(dirtObjects[UnityEngine.Random.Range(0, dirtObjects.Length)], new Vector2(x, y), Quaternion.identity);
                        break;
                    case 2:
                        fTile = Instantiate(stoneObjects[UnityEngine.Random.Range(0, stoneObjects.Length)], new Vector2(x, y), Quaternion.identity);
                        break;
                    case 3:
                        break;
                    case 21:
                        fTile = Instantiate(coal, new Vector2(x, y), Quaternion.identity);
                        break;
                    case 22:
                        fTile = Instantiate(copper, new Vector2(x, y), Quaternion.identity);
                        break;
                    case 23:
                        fTile = Instantiate(silver, new Vector2(x, y), Quaternion.identity);
                        break;
                    case 24:
                        fTile = Instantiate(gold, new Vector2(x, y), Quaternion.identity);
                        break;
                    case 25:
                        fTile = Instantiate(diamond, new Vector2(x, y), Quaternion.identity);
                        break;

                }
                //BACKTILES
                switch (backTilesValue[x, y])
                {
                    case 0:
                        break;
                    case 1:
                        bTile = Instantiate(dirtObjects[UnityEngine.Random.Range(0, dirtObjects.Length)], new Vector2(x, y), Quaternion.identity);
                        break;
                    case 2:
                        bTile = Instantiate(stoneObjects[UnityEngine.Random.Range(0, stoneObjects.Length)], new Vector2(x, y), Quaternion.identity);
                        break;
                    case 3:
                        break;
                    case 21:
                        bTile = Instantiate(coal, new Vector2(x, y), Quaternion.identity);
                        break;
                    case 22:
                        bTile = Instantiate(copper, new Vector2(x, y), Quaternion.identity);
                        break;
                    case 23:
                        bTile = Instantiate(silver, new Vector2(x, y), Quaternion.identity);
                        break;
                    case 24:
                        bTile = Instantiate(gold, new Vector2(x, y), Quaternion.identity);
                        break;
                    case 25:
                        bTile = Instantiate(diamond, new Vector2(x, y), Quaternion.identity);
                        break;

                }

 
                if (fTile != null)
                {
                    frontTiles[x, y] = fTile;
                    //frontTiles[x, y].transform.position = new Vector2(x, y);
                    fTile.GetComponent<SpriteRenderer>().sortingOrder = frontTileLayerID;
                }
                if (bTile != null)
                {
                    backTiles[x, y] = bTile;
                    //backTiles[x, y].transform.position = new Vector2(x, y);
                    bTile.GetComponent<SpriteRenderer>().sortingOrder = backTileLayerID;
                    bTile.GetComponent<Collider2D>().enabled = false;
                    bTile.GetComponent<SpriteRenderer>().color = Color.gray;
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

    //private void CreateTree(int x, int y)
    //{
    //    int height = UnityEngine.Random.Range(minTreeHeight, maxTreeHeight);
    //    GameObject tree = Instantiate(tree1Base, new Vector2(x, y), Quaternion.identity);
    //    tree.GetComponent<SpriteRenderer>().sortingOrder = backTileLayerID;
    //    frontTiles[x, y] = tree;
    //    for(int i = 0; i < height; i++)
    //    {
    //        tree = Instantiate(tree1Core[UnityEngine.Random.Range(0, tree1Core.Length)], new Vector2(x, y + 1 + i), Quaternion.identity);
    //        tree.GetComponent<SpriteRenderer>().sortingOrder = backTileLayerID;
    //        frontTiles[x, y + 1 + i] = tree;
    //    }
    //    tree = Instantiate(tree1Top, new Vector2(x, y + 1 + height), Quaternion.identity);
    //    tree.GetComponent<SpriteRenderer>().sortingOrder = grassLayerID;
    //    tree.transform.SetParent(frontTiles[x, y + height].transform);

    //}

}
