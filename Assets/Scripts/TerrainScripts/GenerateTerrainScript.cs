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

    //private GameObject[,] chunkTiles;
    private GameObject[] chunkArray;

    public int xDimension;
    public float smoothness;
    public int heightMultiple;
    public int heightAddition;

    public int chunkSize;

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
        chunkArray = new GameObject[50];    ///WORK ON THIS LATER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        for (int i = 0; i < chunkArray.GetLength(0); i++)
        {
            chunkArray[i] = new GameObject("chunk" + i);
        }

        GenerateTerrainNoise();
        GenerateStone();
        GenerateGrass();
        GenerateCaves();
        

        GenerateResources(coal, coalChance, coalNeighChance, coalChangeInHeight);
        GenerateResources(copper, copperChance, copperNeighChance, copperChangeInHeight);
        GenerateResources(silver, silverChance, silverNeighChance, silverChangeInHeight);
        GenerateResources(gold, goldChance, goldNeighChance, goldChangeInHeight);
        GenerateResources(diamond, diamondChance, diamondNeighChance, diamondChangeInHeight);

        GenerateChunk();

        DisplayTerrain();

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
                frontTiles[x, y] = Instantiate(dirtObjects[UnityEngine.Random.Range(0, dirtObjects.Length)], new Vector2(x, y), Quaternion.identity);
            }
        }
    }

    private void GenerateStone()
    {
        byte[,] stoneArray = new byte[frontTiles.GetLength(0), frontTiles.GetLength(1)];

        for (int x = 0; x < frontTiles.GetLength(0); x++)
        {
            for (int y = 0; y < frontTiles.GetLength(1); y++)
            {
                if (frontTiles[x, y] != null)
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

            for (int x = 1; x < frontTiles.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < frontTiles.GetLength(1) - 1; y++)
                {
                    if (/*StoneChance[x,y] == 0 ||*/ frontTiles[x, y] == null) continue;

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

        for (int x = 0; x < frontTiles.GetLength(0); x++)
        {
            for (int y = 0; y < frontTiles.GetLength(1); y++)
            {
                if (stoneArray[x, y] == 1)
                {
                    Destroy(frontTiles[x, y]);
                    frontTiles[x, y] = Instantiate(stoneObjects[UnityEngine.Random.Range(0, dirtObjects.Length)], new Vector2(x, y), Quaternion.identity);
                }
            }
        }

    }

    private void GenerateGrass()
    {
        for(int x = 0; x < frontTiles.GetLength(0); x++)
        {
            for(int y = 0; y < frontTiles.GetLength(1); y++)
            {
                if(frontTiles[x,y + 1] == null)
                {
                    GameObject grassObj = Instantiate(grass[UnityEngine.Random.Range(0, 4)], new Vector2(x, y), Quaternion.identity);
                    grassObj.GetComponent<SpriteRenderer>().sortingOrder = 3;

                    int chance = UnityEngine.Random.Range(0, 100);
                    if(chance <= flowerChance)
                    {
                        GameObject flowerObj = Instantiate(flowers[UnityEngine.Random.Range(0, 4)], new Vector2(x, y+1), Quaternion.identity);
                        flowerObj.GetComponent<SpriteRenderer>().sortingOrder = 3;
                    }

                    chance = UnityEngine.Random.Range(0, 100);
                    if(chance < treeChance)
                    {
                        CreateTree(x, y + 1);
                    }



                    break;
                }
            }
        }
    }

    private void GenerateCaves()
    {

    }

    private void GenerateResources(GameObject resource, int resourceChance, int resourceNeighChance, float resourceChangeInHeight)
    {
        byte[,] resourceArray = new byte[frontTiles.GetLength(0), frontTiles.GetLength(1)];
        for (int x = 0; x < frontTiles.GetLength(0); x++)
        {
            for (int y = 0; y < frontTiles.GetLength(1); y++)
            {
                if (frontTiles[x, y] != null && frontTiles[x, y].GetComponent<TileScript>().tileId == 1)
                {
                    if (UnityEngine.Random.Range(1, 100) <= (resourceChance - y * resourceChangeInHeight))
                    {
                        resourceArray[x, y] = 1;
                    }
                }
            }
        }

        for (int x = 0; x < resourceArray.GetLength(0); x++)
        {
            for (int y = 0; y < resourceArray.GetLength(1); y++)
            {
                if (frontTiles[x, y] != null && GetNeighBorhood(resourceArray, x, y, 1) > 1)
                {
                    if (UnityEngine.Random.Range(1, 100) <= coalNeighChance)
                    {
                        resourceArray[x, y] = 1;
                    }
                    else
                    {
                        resourceArray[x, y] = 0;
                    }
                }
                else
                {
                    resourceArray[x, y] = 0;
                }
            }
        }

        for (int x = 0; x < resourceArray.GetLength(0); x++)
        {
            for (int y = 0; y < resourceArray.GetLength(1); y++)
            {
                if (resourceArray[x, y] == 1)
                {
                    Destroy(frontTiles[x, y]);
                    frontTiles[x, y] = Instantiate(resource, new Vector2(x, y), Quaternion.identity);
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

    private void DisplayTerrain()
    {
        for (int x = 0; x < frontTiles.GetLength(0); x++)
        {
            for (int y = 0; y < frontTiles.GetLength(1); y++)
            {
                if (frontTiles[x, y] != null)
                {
                    frontTiles[x, y].transform.position = new Vector2(x, y);
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

    private void CreateTree(int x, int y)
    {
        int height = UnityEngine.Random.Range(minTreeHeight, maxTreeHeight);
        GameObject tree = Instantiate(tree1Base, new Vector2(x, y), Quaternion.identity);
        tree.GetComponent<SpriteRenderer>().sortingLayerName = "backTileLayer";
        for(int i = 0; i < height; i++)
        {
            tree = Instantiate(tree1Core[UnityEngine.Random.Range(0, tree1Core.Length)], new Vector2(x, y + 1 + i), Quaternion.identity);
            tree.GetComponent<SpriteRenderer>().sortingLayerName = "backTileLayer";
        }
        tree = Instantiate(tree1Top, new Vector2(x, y + 1 + height), Quaternion.identity);
        tree.GetComponent<SpriteRenderer>().sortingLayerName = "grassLayer";

    }

}
