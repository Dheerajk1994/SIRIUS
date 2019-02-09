using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TileEnum {
    EMPTY = 0,
    DIRT,
    STONE,
    WOOD,
    TREETRUNK = 17,
    TREELEAF,
    FLOWER,
    GRASS, 
    COAL,
    COPPER, 
    SILVER,
    GOLD, 
    DIAMOND,
    CAMPFIRE = 50
};


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
        //(a % b + b) % b;
        ushort chunkPosY = (ushort)Mathf.Floor(playerPos.y / chunkSize);

        int xRelativel = Mathf.FloorToInt(((playerPos.x - chunkSize )% worldXDimension + worldXDimension) % worldXDimension);
        int xRelativem = Mathf.FloorToInt(( playerPos.x              % worldXDimension + worldXDimension) % worldXDimension);
        int xRelativer = Mathf.FloorToInt(((playerPos.x + chunkSize )% worldXDimension + worldXDimension) % worldXDimension);

        ushort chunkToDisplayl = (ushort)Mathf.FloorToInt(xRelativel / chunkSize);
        ushort chunkToDisplaym = (ushort)Mathf.FloorToInt(xRelativem / chunkSize);
        ushort chunkToDisplayr = (ushort)Mathf.FloorToInt(xRelativer / chunkSize);

        int cppxl = Mathf.FloorToInt((playerPos.x - chunkSize) / chunkSize) * chunkSize;
        int cppxm = Mathf.FloorToInt((playerPos.x            ) / chunkSize) * chunkSize;
        int cppxr = Mathf.FloorToInt((playerPos.x + chunkSize) / chunkSize) * chunkSize;

        chunks[chunkToDisplayl, chunkPosY].transform.localPosition = new Vector2(cppxl, chunkPosY * chunkSize);
        chunks[chunkToDisplaym, chunkPosY].transform.localPosition = new Vector2(cppxm, chunkPosY * chunkSize);
        chunks[chunkToDisplayr, chunkPosY].transform.localPosition = new Vector2(cppxr, chunkPosY * chunkSize);


        //Debug.Log(chunkToDisplayl + " " + chunkToDisplaym + " " + chunkToDisplayr);
        Debug.Log(chunkPosY);
        if(!chunksLoadedIntoMemory[chunkToDisplayl,chunkPosY]) LoadChunk(chunkToDisplayl, chunkPosY);
        if(!chunksLoadedIntoMemory[chunkToDisplaym,chunkPosY]) LoadChunk(chunkToDisplaym, chunkPosY);
        if(!chunksLoadedIntoMemory[chunkToDisplayr,chunkPosY]) LoadChunk(chunkToDisplayr, chunkPosY);
        //if (!chunksLoadedIntoMemory[0, 1]) LoadChunk(0, 1);
        //if (!chunksLoadedIntoMemory[1, 1]) LoadChunk(1, 1);
        //if (!chunksLoadedIntoMemory[2, 1]) LoadChunk(2, 1);

    }

    void LoadChunk(ushort cx, ushort cy)        //LOADS A CHUNK - USE WHEN CHUNK IS NOT POPULATED YET
    {
        //int cppx = Mathf.FloorToInt(ppos.x / chunkSize) * chunkSize;
        //int cppy = Mathf.FloorToInt(ppos.y / chunkSize);
        ushort fetchPosX = 0;
        ushort fetchPosY = 0;
        for (ushort x = 0; x < chunkSize; ++x)
        {
            for (ushort y = 0; y < chunkSize; y++)
            {
                fetchPosX = (ushort)(x + (cx * chunkSize));
                fetchPosY = (ushort)(y + (cy * chunkSize));
                //PLACE TILES
                GameObject tile;

                //FRONT TILE
                tile = GetTileToPlace(fetchPosX, fetchPosY, frontTilesValue);
                if (tile)
                {
                    tile = Instantiate(tile);
                    tile.transform.SetParent(chunks[cx, cy].transform);
                    tile.transform.localPosition = new Vector2(x, y);
                    PlaceTileInFrontLayer(tile);
                }
                //BACK TILES
                tile = GetTileToPlace(fetchPosX, fetchPosY, backTilesValue);
                if (tile)
                {
                    tile = Instantiate(tile);
                    tile.transform.SetParent(chunks[cx, cy].transform);
                    tile.transform.localPosition = new Vector2(x, y);
                    PlaceTileInBackLayer(tile);
                }
                //VEGETATION TILES
                tile = GetTileToPlace(fetchPosX, fetchPosY, vegetationTilesValue);
                if (tile)
                {
                    tile = Instantiate(tile);
                    tile.transform.SetParent(chunks[cx, cy].transform);
                    tile.transform.localPosition = new Vector2(x, y);
                    PlaceTileInVegetationtLayer(tile);
                }
            }
        }
        chunksLoadedIntoMemory[cx, cy] = true;
        chunksCurrentlyDisplaying[cx, cy] = true;

    }

    GameObject GetTileToPlace (ushort x, ushort y, ushort [,] tileLayer)
    {
        GameObject tile = null;

        switch (tileLayer[x,y])
        {
            case (ushort)TileEnum.EMPTY:
                break;
            case (ushort)TileEnum.DIRT:
                tile = dirtObjects[UnityEngine.Random.Range(0, dirtObjects.Length)];
                break;
            case (ushort)TileEnum.STONE:
                tile = stoneObjects[UnityEngine.Random.Range(0, dirtObjects.Length)];
                break;
            case (ushort)TileEnum.WOOD:
                Debug.LogError("WOOD GAMEOBJECT MISSING");
                break;
            case (ushort)TileEnum.TREETRUNK:
                tile = tree1Core[UnityEngine.Random.Range(0, tree1Core.Length)];
                break;
            case (ushort)TileEnum.TREELEAF:
                tile = tree1Top;
                break;
            case (ushort)TileEnum.FLOWER:
                tile = flowers[UnityEngine.Random.Range(0, 4)];
                break;
            case (ushort)TileEnum.GRASS:
                tile = grass[UnityEngine.Random.Range(0, 4)];
                break;
            case (ushort)TileEnum.COAL:
                tile = coal;
                break;
            case (ushort)TileEnum.COPPER:
                tile = coal;
                break;
            case (ushort)TileEnum.SILVER:
                tile = silver;
                break;
            case (ushort)TileEnum.GOLD:
                tile = gold;
                break;
            case (ushort)TileEnum.DIAMOND:
                tile = diamond;
                break;
            case (ushort)TileEnum.CAMPFIRE:
                Debug.LogError("CAMPFIRE GAMEOBJECT MISSING");
                break;
            default:
                Debug.LogError("INVALID VALUE RECEIVED IN FUNCTION TILETOPLACE : VALUE " + tileLayer[x, y]);
                break;

        }

        return tile;
    }

    void PlaceTileInFrontLayer(GameObject tile)
    {
        tile.GetComponent<SpriteRenderer>().sortingOrder = frontTileLayerID;
    }

    void PlaceTileInBackLayer(GameObject tile)
    {
        tile.GetComponent<SpriteRenderer>().sortingOrder = backTileLayerID;
        tile.GetComponent<Collider2D>().enabled = false;
        tile.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    void PlaceTileInVegetationtLayer(GameObject tile)
    {
        tile.GetComponent<SpriteRenderer>().sortingOrder = grassLayerID;
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
