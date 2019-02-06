using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManagerScript : MonoBehaviour
{
    public ushort xDimension;
    public ushort heightAddition;

    private ushort worldXDimension;
    private ushort worldYDimension;

    public GameObject[] stoneObjects;
    public GameObject[] dirtObjects;
    public GameObject[] grass;
    public GameObject[] flowers;
    public GameObject   tree1Base;
    public GameObject   tree1Top;
    public GameObject[] tree1Core;


    public GameObject coal;
    public GameObject copper;
    public GameObject silver;
    public GameObject gold;
    public GameObject diamond;

    public ushort        chunkSize = 40;
    public GameObject[,] chunks;
    public bool[,]       chunksLoadedIntoMemory;
    public bool[,]       chunksCurrentlyDisplaying;

    public GameObject[,] frontTiles;
    public GameObject[,] backTiles;
    public GameObject[,] vegetationTiles;   //USED TO STORE PLANTS, GRASS, AND OTHER VEGETATION

    ushort[,] frontTilesValue;
    ushort[,] backTilesValue;
    ushort[,] vegetationTilesValue;


    public int backTileLayerID;
    public int frontTileLayerID;
    public int grassLayerID;

    public GameObject player;
    private InventoryScript playerInventoryScript;

    public void StartTerrainGen()
    {
        this.GetComponentInParent<GenerateTerrainScript>().StartTerrainGeneration(this, xDimension, heightAddition, chunkSize);
    }


    public void SetTiles(ushort[,] fTilesV, ushort[,] bTilesV, ushort[,] vTilesV)
    {
        worldXDimension = (ushort)fTilesV.GetLength(0);
        worldYDimension = (ushort)fTilesV.GetLength(1);

        frontTilesValue      = fTilesV;
        backTilesValue       = bTilesV;
        vegetationTilesValue = vTilesV;

        frontTiles      = new GameObject[worldXDimension, worldYDimension];
        backTiles       = new GameObject[worldXDimension, worldYDimension];
        vegetationTiles = new GameObject[worldXDimension, worldYDimension];

        chunks                    = new GameObject[worldXDimension/ chunkSize, worldYDimension / chunkSize];
        chunksLoadedIntoMemory    = new bool      [worldXDimension/ chunkSize, worldYDimension / chunkSize];
        chunksCurrentlyDisplaying = new bool      [worldXDimension/ chunkSize, worldYDimension / chunkSize];
        GameObject ChunkHierarchy = new GameObject("ChunkHierarchy");
        ChunkHierarchy.transform.SetParent(GameObject.Find("PlayArea").transform);
        for (int x = 0; x < chunks.GetLength(0); ++x)
        {for(int y = 0; y < chunks.GetLength(1); ++y)
            {
                chunks[x, y] = new GameObject("chunk [" + x + "," + y + "]");
                chunks[x, y].transform.SetParent(ChunkHierarchy.transform);
                chunks[x, y].transform.position = new Vector2(x * chunkSize, y * chunkSize);
            }
        }
        Debug.Log("values received");
    }


    public void DisplayChunks(Vector2 playerPos)
    {
        for(int x = 0; x < chunks.GetLength(0); ++x)
        {
            for(int y = 0; y < chunks.GetLength(1); ++y)
            {
                if(Vector2.Distance(playerPos, chunks[x,y].transform.position) < 80)
                {
                    if (!chunksLoadedIntoMemory[x, y])
                    {
                        LoadChunk((ushort)x, (ushort)y, playerPos);
                    }
                    else if (!chunksCurrentlyDisplaying[x, y])
                    {
                        chunks[x, y].SetActive(true);
                        chunksCurrentlyDisplaying[x, y] = true;
                    }
                    
                }
                else
                {
                    chunks[x, y].SetActive(false);
                    chunksCurrentlyDisplaying[x, y] = false;
                }
            }
        }
    }

    void LoadChunk(ushort cXpos, ushort cYpos, Vector2 pPos)        //LOADS A CHUNK - USE WHEN CHUNK IS NOT POPULATED YET
    {
        chunks[cXpos, cYpos].SetActive(true);
        Debug.Log(cXpos + " " + cYpos);
        int xPos = cXpos * chunkSize;
        int yPos = cYpos * chunkSize;

        for(int x = xPos; x < xPos + chunkSize; ++x)
        {
            for(int y = yPos; y < yPos + chunkSize; ++y)
            {
                GameObject fTile = null;
                GameObject bTile = null;
                GameObject vTile = null;

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
                    default:
                        Debug.LogError("INVALID ID IN FRONT TILE VALUE ARRAY " + frontTilesValue[x, y]);
                        break;

                }
                //BACK TILES
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
                    default:
                        Debug.LogError("INVALID ID IN BACK TILE VALUE ARRAY " + backTilesValue[x, y]);
                        break;

                }
                //VEGETATION TILES
                switch (vegetationTilesValue[x, y])
                {
                    case 0:
                        break;
                    case 17:
                        vTile = Instantiate(tree1Core[UnityEngine.Random.Range(0, tree1Core.Length)], new Vector2(x, y), Quaternion.identity);
                        vegetationTiles[x, y] = vTile;
                        vTile.GetComponent<SpriteRenderer>().sortingOrder = backTileLayerID;
                        break;
                    case 18:
                        vTile = Instantiate(tree1Top, new Vector2(x, y), Quaternion.identity);
                        vegetationTiles[x, y] = vTile;
                        vTile.GetComponent<SpriteRenderer>().sortingOrder = grassLayerID;
                        vTile.transform.SetParent(vegetationTiles[x, y - 1].transform);
                        break;
                    case 19:
                        vTile = Instantiate(flowers[UnityEngine.Random.Range(0, 4)], new Vector2(x, y), Quaternion.identity);
                        vTile.GetComponent<SpriteRenderer>().sortingOrder = grassLayerID;
                        break;
                    case 20:
                        vTile = Instantiate(grass[UnityEngine.Random.Range(0, 4)], new Vector2(x, y), Quaternion.identity);
                        vTile.GetComponent<SpriteRenderer>().sortingOrder = grassLayerID;
                        break;
                    default:
                        Debug.LogError("INVALID ID IN BACK TILE VALUE ARRAY " + vegetationTilesValue[x, y]);
                        break;
                }

                if (fTile != null)
                {
                    frontTiles[x, y] = fTile;
                    //frontTiles[x, y].transform.position = new Vector2(x, y);
                    fTile.GetComponent<SpriteRenderer>().sortingOrder = frontTileLayerID;
                    fTile.transform.SetParent(chunks[cXpos, cYpos].transform);
                }
                if (bTile != null)
                {
                    backTiles[x, y] = bTile;
                    //backTiles[x, y].transform.position = new Vector2(x, y);
                    bTile.GetComponent<SpriteRenderer>().sortingOrder = backTileLayerID;
                    bTile.GetComponent<Collider2D>().enabled = false;
                    bTile.GetComponent<SpriteRenderer>().color = Color.gray;
                    bTile.transform.SetParent(chunks[cXpos, cYpos].transform);
                }
                if(vTile != null)
                {
                    vTile.transform.SetParent(chunks[cXpos, cYpos].transform);
                }
            }
        }
        
        chunksLoadedIntoMemory[cXpos, cYpos] = true;
        chunksCurrentlyDisplaying[cXpos, cYpos] = true;
    }

    public GameObject MineTile(int x, int y)
    {
        if (x >= 0 && x < worldXDimension&& y >= 0 && y < worldYDimension && frontTiles[x,y] != null)
        {
            if(frontTiles[x,y].GetComponent<TileScript>().tileId == 2)
            {
                CutTree(x, y);
            }
            else
            {
                player.GetComponent<InventoryScript>().AddItemToInventory(frontTiles[x, y], 1);
                Destroy(frontTiles[x, y]);
            }

            
        }

        return null;
    }

    public bool PlaceTile(int x, int y, GameObject tile)
    {
        if (x >= 0 && x < worldXDimension&& y >= 0 && y < worldYDimension && frontTiles[x, y] == null)
        {
            playerInventoryScript = player.GetComponent<InventoryScript>();

            if(playerInventoryScript.CheckItemInInventory(tile, 1))
            {
                playerInventoryScript.RemoveItemFromInventory(tile, 1);
                GameObject t = Instantiate(tile, new Vector2(x, y), Quaternion.identity);
                t.GetComponent<SpriteRenderer>().sortingOrder = frontTileLayerID;
                frontTiles[x, y] = t;
                return true;
            }

            
        }
        return false;
    }

    private void CutTree(int x, int y)
    {
        while(frontTiles[x,y] != null && frontTiles[x,y].GetComponent<TileScript>().tileId == 2 && y < frontTiles.GetLength(1))
        {
            player.GetComponent<InventoryScript>().AddItemToInventory(frontTiles[x, y], 1);
            Destroy(frontTiles[x, y]);
            y++;
        }
    }

    //BEGIN CREATETILE GAME OBJECTS
    private void CreateTileGameobjects(ushort [,] frontTilesValue, ushort[,] backTilesValue, ushort[,] vegetationTilesValue)
    {
        ushort worldXDimension= (ushort)frontTilesValue.GetLength(0);
        ushort yDim = (ushort)frontTilesValue.GetLength(1);

        for (int x = 0; x < worldXDimension; x++)
        {
            for (int y = 0; y < worldYDimension; y++)
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
                    default:
                        Debug.LogError("INVALID ID IN FRONT TILE VALUE ARRAY " + frontTilesValue[x,y]);
                        break;

                }
                //BACK TILES
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
                    default:
                        Debug.LogError("INVALID ID IN BACK TILE VALUE ARRAY " + backTilesValue[x,y]);
                        break;

                }
                //VEGETATION TILES
                switch (vegetationTilesValue[x, y])
                {
                    case 0:
                        break;
                    case 17:
                        GameObject trunk = Instantiate(tree1Core[UnityEngine.Random.Range(0, tree1Core.Length)], new Vector2(x, y), Quaternion.identity);
                        vegetationTiles[x, y] = trunk;
                        trunk.GetComponent<SpriteRenderer>().sortingOrder = backTileLayerID;
                        break;
                    case 18:
                        GameObject treeLeaf = Instantiate(tree1Top, new Vector2(x, y), Quaternion.identity);
                        vegetationTiles[x, y] = treeLeaf;
                        treeLeaf.GetComponent<SpriteRenderer>().sortingOrder = grassLayerID;
                        treeLeaf.transform.SetParent(vegetationTiles[x, y - 1].transform);
                        break;
                    case 19:
                        GameObject flowerObj = Instantiate(flowers[UnityEngine.Random.Range(0, 4)], new Vector2(x, y), Quaternion.identity);
                        flowerObj.GetComponent<SpriteRenderer>().sortingOrder = grassLayerID;
                        break;
                    case 20:
                        GameObject grassObj = Instantiate(grass[UnityEngine.Random.Range(0, 4)], new Vector2(x, y), Quaternion.identity);
                        grassObj.GetComponent<SpriteRenderer>().sortingOrder = grassLayerID;
                        break;
                    default:
                        Debug.LogError("INVALID ID IN BACK TILE VALUE ARRAY " + vegetationTilesValue[x,y]);
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
    //END CREATETILE GAME OBJECTS

}
