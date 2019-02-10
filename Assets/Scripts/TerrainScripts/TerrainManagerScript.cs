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
    #region VARIABLES
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

    public ushort[,] frontTilesValue;
    public ushort[,] backTilesValue;
    public ushort[,] vegetationTilesValue;


    public int backTileLayerID;
    public int frontTileLayerID;
    public int grassLayerID;

    public GameObject player;
    private InventoryScript playerInventoryScript;

    #endregion 

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
        ushort chunkPosY = (ushort)Mathf.Floor(playerPos.y / chunkSize);

        int xRelativel = Mathf.FloorToInt(((playerPos.x - chunkSize )% worldXDimension + worldXDimension) % worldXDimension);
        int xRelativem = Mathf.FloorToInt(( playerPos.x              % worldXDimension + worldXDimension) % worldXDimension);
        int xRelativer = Mathf.FloorToInt(((playerPos.x + chunkSize )% worldXDimension + worldXDimension) % worldXDimension);

        ushort chunkToDisplayl = (ushort)Mathf.Floor(xRelativel / chunkSize);
        ushort chunkToDisplaym = (ushort)Mathf.Floor(xRelativem / chunkSize);
        ushort chunkToDisplayr = (ushort)Mathf.Floor(xRelativer / chunkSize);

        int cppxl = Mathf.FloorToInt((playerPos.x - chunkSize) / chunkSize) * chunkSize;
        int cppxm = Mathf.FloorToInt((playerPos.x            ) / chunkSize) * chunkSize;
        int cppxr = Mathf.FloorToInt((playerPos.x + chunkSize) / chunkSize) * chunkSize;

        chunks[chunkToDisplayl, chunkPosY].transform.localPosition = new Vector2(cppxl, chunkPosY * chunkSize);
        chunks[chunkToDisplaym, chunkPosY].transform.localPosition = new Vector2(cppxm, chunkPosY * chunkSize);
        chunks[chunkToDisplayr, chunkPosY].transform.localPosition = new Vector2(cppxr, chunkPosY * chunkSize);

        if(!chunksLoadedIntoMemory[chunkToDisplayl,chunkPosY]) LoadChunk(chunkToDisplayl, chunkPosY);
        if(!chunksLoadedIntoMemory[chunkToDisplaym,chunkPosY]) LoadChunk(chunkToDisplaym, chunkPosY);
        if(!chunksLoadedIntoMemory[chunkToDisplayr,chunkPosY]) LoadChunk(chunkToDisplayr, chunkPosY);

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
                    frontTiles[fetchPosX, fetchPosY] = tile;
                    PlaceTileInFrontLayer(tile);
                }
                //BACK TILES
                tile = GetTileToPlace(fetchPosX, fetchPosY, backTilesValue);
                if (tile)
                {
                    tile = Instantiate(tile);
                    tile.transform.SetParent(chunks[cx, cy].transform);
                    tile.transform.localPosition = new Vector2(x, y);
                    backTiles[fetchPosX, fetchPosY] = tile;
                    PlaceTileInBackLayer(tile);
                }
                //VEGETATION TILES
                tile = GetTileToPlace(fetchPosX, fetchPosY, vegetationTilesValue);
                if (tile)
                {
                    tile = Instantiate(tile);
                    tile.transform.SetParent(chunks[cx, cy].transform);
                    tile.transform.localPosition = new Vector2(x, y);
                    vegetationTiles[fetchPosX, fetchPosY] = tile;
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
        ushort relativeX = (ushort)Mathf.Floor((x % worldXDimension + worldXDimension) % worldXDimension);
        ushort relativeY = (ushort)Mathf.Floor(y / chunkSize);

        player.GetComponent<InventoryScript>().AddItemToInventory(GetTileToPlace((ushort)relativeX, (ushort)y, frontTilesValue), 1);

        //ushort posXinChunk = (ushort)(Mathf.Floor((x % chunkSize + chunkSize)) % chunkSize);
        //ushort posYinChunk = (ushort)(y % chunkSize);

        //chunks[relativeX, relativeY]
        frontTilesValue             [relativeX, y] = 0;
        vegetationTilesValue        [relativeX, y] = 0;

        Destroy(frontTiles          [relativeX, y]);
        Destroy(vegetationTiles     [relativeX, y]);

        return null;
    }

    public bool PlaceTile(int x, int y, GameObject t, ushort id)
    {
        ushort relativeX = (ushort)Mathf.Floor((x % worldXDimension + worldXDimension) % worldXDimension);
        //ushort relativeY = (ushort)Mathf.Floor(y / chunkSize);
        if(frontTilesValue[relativeX, y] == (ushort)TileEnum.EMPTY)
        {
            ushort posXinChunk = (ushort)(Mathf.Floor((x % chunkSize + chunkSize)) % chunkSize);
            ushort posYinChunk = (ushort)(y % chunkSize);

            ushort chunkX = (ushort)Mathf.Floor(relativeX / chunkSize);
            ushort chunkY = (ushort)Mathf.Floor(y / chunkSize);

            //playerInventoryScript.RemoveItemFromInventory(t, 1);
            GameObject tile = Instantiate(t);

            frontTilesValue[relativeX, y] = (ushort)TileEnum.STONE;
            frontTiles[relativeX, y] = tile;
            tile.transform.SetParent(chunks[chunkX, chunkY].transform);
            tile.transform.localPosition = new Vector2(posXinChunk, posYinChunk);
            PlaceTileInFrontLayer(tile);
        }
       
        return true;
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

}
