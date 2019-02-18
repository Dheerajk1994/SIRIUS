using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum EnumClass.TileEnum {
//    EMPTY = 0,
//    REGULAR_DIRT             = 1,
//    REGULAR_STONE            = 2,
//    REGULAR_WOOD             = 3,
//    REGULAR_TREETRUNK        = 17,
//    REGULAR_TREELEAF         ,
//    REGULAR_FLOWER           ,
//    REGULAR_GRASS            , 
//    //RESOURCES
//    COAL                     = 21,
//    COPPER                   , 
//    SILVER                   ,
//    GOLD                     , 
//    DIAMOND                  ,
//    //MOON
//    MOON_DIRT                = 40, 
//    MOON_STONE, 
//    MOON_SAND,

//    CAMPFIRE = 100
//};


public class TerrainManagerScript : MonoBehaviour
{
    #region VARIABLES
    public ushort xDimension;
    public ushort heightAddition;

    private ushort worldXDimension;
    private ushort worldYDimension;

    public GameObject TileObjectPrefab;

    //STONES
    public Sprite[] regularStoneSprites;
    public Sprite[] moonStoneSprites;

    //DIRT
    public Sprite[] regularDirtSprites;
    public Sprite[] moonDirtSprites;

    //SAND
    public Sprite[] regularSandSprites;
    public Sprite[] moonSandSprite;

    //GRASS
    public Sprite[] regularGrassSprites;

    //FLOWERS
    public Sprite[] regularFlowerSprites;

    //TREE
    public Sprite[] regularTreeBaseSprite;
    public Sprite[] regularTreeTopSprite;
    public Sprite[] regularTreeCoreSprite;

    //MINEABLE RESOURCES
    public Sprite[] coalSprites;
    public Sprite[] copperSprites;
    public Sprite[] ironSprites;
    public Sprite[] silverSprites;
    public Sprite[] goldSprites;
    public Sprite[] diamondSprites;

    public ushort        chunkSize = 40;
    public GameObject[,] chunks;
    public bool[,]       chunksLoadedIntoMemory;
    public bool[,]       chunksCurrentlyDisplaying;

    public GameObject[,] frontTiles;
    public GameObject[,] backTiles;
    public GameObject[,] vegetationTiles;   //USED TO STORE PLANTS, GRASS, AND OTHER VEGETATION

    public GameObject[,] frontTilesResources;
    public GameObject[,] backTilesResources;

    public ushort[,] frontTilesValue;
    public ushort[,] frontTilesResourceValue;
    public ushort[,] backTilesValue;
    public ushort[,] backTilesResourceValue;
    public ushort[,] vegetationTilesValue;


    public GameObject player;
    private InventoryScript playerInventoryScript;

    #endregion 

    public void StartTerrainGen()
    {
        this.GetComponentInParent<GenerateTerrainScript>().StartTerrainGeneration(this, xDimension, heightAddition, chunkSize, (ushort)EnumClass.TerrainType.MOON);
    }

    public void SetTiles(ushort[,] fTilesV,ushort[,] fTilesRV, ushort[,] bTilesV, ushort[,] bTilesRV, ushort[,] vTilesV)
    {
        player = GameObject.Find("GameManager").GetComponent<GameManagerScript>().player;

        worldXDimension = (ushort)fTilesV.GetLength(0);
        worldYDimension = (ushort)fTilesV.GetLength(1);

        frontTilesValue         = fTilesV;
        frontTilesResourceValue = fTilesRV;
        backTilesValue          = bTilesV;
        backTilesResourceValue  = bTilesRV;
        vegetationTilesValue    = vTilesV;

        frontTiles      = new GameObject[worldXDimension, worldYDimension];
        backTiles       = new GameObject[worldXDimension, worldYDimension];
        vegetationTiles = new GameObject[worldXDimension, worldYDimension];

        frontTilesResources = new GameObject[worldXDimension, worldYDimension];
        backTilesResources  = new GameObject[worldXDimension, worldYDimension];

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
        ushort chunkPosY = (ushort)Mathf.Clamp((ushort)Mathf.Floor(playerPos.y / chunkSize), 0, chunks.GetLength(1) - 1);

        int xRelativell = Mathf.FloorToInt(((playerPos.x - chunkSize * 2 )% worldXDimension + worldXDimension) % worldXDimension);
        int xRelativel  = Mathf.FloorToInt(((playerPos.x - chunkSize)     % worldXDimension + worldXDimension) % worldXDimension);
        int xRelativem  = Mathf.FloorToInt(( playerPos.x                  % worldXDimension + worldXDimension) % worldXDimension);
        int xRelativer  = Mathf.FloorToInt(((playerPos.x + chunkSize)     % worldXDimension + worldXDimension) % worldXDimension);
        int xRelativerr = Mathf.FloorToInt(((playerPos.x + chunkSize * 2 )% worldXDimension + worldXDimension) % worldXDimension);

        ushort chunkToDisplayll = (ushort)Mathf.Floor(xRelativell / chunkSize);
        ushort chunkToDisplayl  = (ushort)Mathf.Floor(xRelativel  / chunkSize);
        ushort chunkToDisplaym  = (ushort)Mathf.Floor(xRelativem  / chunkSize);
        ushort chunkToDisplayr  = (ushort)Mathf.Floor(xRelativer  / chunkSize);
        ushort chunkToDisplayrr = (ushort)Mathf.Floor(xRelativerr / chunkSize);


        int cppxll = Mathf.FloorToInt((playerPos.x - chunkSize * 2) / chunkSize) * chunkSize;
        int cppxl  = Mathf.FloorToInt((playerPos.x - chunkSize)     / chunkSize) * chunkSize;
        int cppxm  = Mathf.FloorToInt((playerPos.x            )     / chunkSize) * chunkSize;
        int cppxr  = Mathf.FloorToInt((playerPos.x + chunkSize)     / chunkSize) * chunkSize;
        int cppxrr = Mathf.FloorToInt((playerPos.x + chunkSize * 2) / chunkSize) * chunkSize;

        //TOP TOP Y - TO TURN OFF
        if (chunkPosY + 2 < chunks.GetLength(1))
        {
            chunks[chunkToDisplayl, chunkPosY + 2].transform.localPosition = new Vector2(cppxl, (chunkPosY + 2) * chunkSize);
            chunks[chunkToDisplaym, chunkPosY + 2].transform.localPosition = new Vector2(cppxm, (chunkPosY + 2) * chunkSize);
            chunks[chunkToDisplayr, chunkPosY + 2].transform.localPosition = new Vector2(cppxr, (chunkPosY + 2) * chunkSize);

            chunks[chunkToDisplayl, chunkPosY + 2].SetActive(false); chunksCurrentlyDisplaying[chunkToDisplayl, chunkPosY + 2] = false;
            chunks[chunkToDisplaym, chunkPosY + 2].SetActive(false); chunksCurrentlyDisplaying[chunkToDisplaym, chunkPosY + 2] = false;
            chunks[chunkToDisplayr, chunkPosY + 2].SetActive(false); chunksCurrentlyDisplaying[chunkToDisplayr, chunkPosY + 2] = false;


        }
        //TOP Y
        if (chunkPosY + 1 < chunks.GetLength(1))
        {
            chunks[chunkToDisplayll, chunkPosY + 1].transform.localPosition = new Vector2(cppxll, (chunkPosY + 1) * chunkSize);//R
            chunks[chunkToDisplayl,  chunkPosY + 1].transform.localPosition = new Vector2(cppxl,  (chunkPosY + 1) * chunkSize);//S
            chunks[chunkToDisplaym,  chunkPosY + 1].transform.localPosition = new Vector2(cppxm,  (chunkPosY + 1) * chunkSize);//S
            chunks[chunkToDisplayr,  chunkPosY + 1].transform.localPosition = new Vector2(cppxr,  (chunkPosY + 1) * chunkSize);//S
            chunks[chunkToDisplayrr, chunkPosY + 1].transform.localPosition = new Vector2(cppxrr, (chunkPosY + 1) * chunkSize);//R

            DisplayChunk(chunkToDisplayl,  (ushort)(chunkPosY + 1));
            DisplayChunk(chunkToDisplaym,  (ushort)(chunkPosY + 1));
            DisplayChunk(chunkToDisplayr,  (ushort)(chunkPosY + 1));


            chunks[chunkToDisplayll, chunkPosY + 1].SetActive(false);chunksCurrentlyDisplaying[chunkToDisplayll, chunkPosY + 1] = false;
            chunks[chunkToDisplayrr, chunkPosY + 1].SetActive(false);chunksCurrentlyDisplaying[chunkToDisplayrr, chunkPosY + 1] = false;
        }                                            


        //MIDDLE Y
        //Debug.LogError(chunks.GetLength(0) + " " + chunks.GetLength(1) + " - " + chunkToDisplayll + " " + chunkPosY);
            chunks[chunkToDisplayll, chunkPosY].transform.localPosition = new Vector2(cppxll, chunkPosY * chunkSize);//R
            chunks[chunkToDisplayl, chunkPosY].transform.localPosition = new Vector2(cppxl, chunkPosY * chunkSize);//S
            chunks[chunkToDisplaym, chunkPosY].transform.localPosition = new Vector2(cppxm, chunkPosY * chunkSize);//S
            chunks[chunkToDisplayr, chunkPosY].transform.localPosition = new Vector2(cppxr, chunkPosY * chunkSize);//S
            chunks[chunkToDisplayrr, chunkPosY].transform.localPosition = new Vector2(cppxrr, chunkPosY * chunkSize);//R


            DisplayChunk(chunkToDisplayl, chunkPosY);
            DisplayChunk(chunkToDisplaym, chunkPosY);
            DisplayChunk(chunkToDisplayr, chunkPosY);

            chunks[chunkToDisplayll, chunkPosY].SetActive(false);chunksCurrentlyDisplaying[chunkToDisplayll, chunkPosY] = false;
            chunks[chunkToDisplayrr, chunkPosY].SetActive(false);chunksCurrentlyDisplaying[chunkToDisplayrr, chunkPosY] = false;



        //BOT Y
        if (chunkPosY - 1 >= 0)
        {
            chunks[chunkToDisplayll, chunkPosY - 1].transform.localPosition = new Vector2(cppxll, (chunkPosY - 1) * chunkSize);//R
            chunks[chunkToDisplayl,  chunkPosY - 1].transform.localPosition = new Vector2(cppxl,  (chunkPosY - 1) * chunkSize);//S
            chunks[chunkToDisplaym,  chunkPosY - 1].transform.localPosition = new Vector2(cppxm,  (chunkPosY - 1) * chunkSize);//S
            chunks[chunkToDisplayr,  chunkPosY - 1].transform.localPosition = new Vector2(cppxr,  (chunkPosY - 1) * chunkSize);//S
            chunks[chunkToDisplayrr, chunkPosY - 1].transform.localPosition = new Vector2(cppxrr, (chunkPosY - 1) * chunkSize);//R

            DisplayChunk(chunkToDisplayl, (ushort)(chunkPosY - 1));
            DisplayChunk(chunkToDisplaym, (ushort)(chunkPosY - 1));
            DisplayChunk(chunkToDisplayr, (ushort)(chunkPosY - 1));

            //Debug.Log(chunkToDisplayl + " " + (ushort)(chunkPosY - 1));

            chunks[chunkToDisplayll, chunkPosY - 1].SetActive(false);chunksCurrentlyDisplaying[chunkToDisplayll, chunkPosY - 1] = false;
            chunks[chunkToDisplayrr, chunkPosY - 1].SetActive(false);chunksCurrentlyDisplaying[chunkToDisplayrr, chunkPosY - 1] = false;
        }                                                      

        //BOT BOT Y - TO REMOVE
        if (chunkPosY - 2 >= 0)
        {
            chunks[chunkToDisplayl, chunkPosY - 2].transform.localPosition = new Vector2(cppxl, (chunkPosY - 2) * chunkSize);
            chunks[chunkToDisplaym, chunkPosY - 2].transform.localPosition = new Vector2(cppxm, (chunkPosY - 2) * chunkSize);
            chunks[chunkToDisplayr, chunkPosY - 2].transform.localPosition = new Vector2(cppxr, (chunkPosY - 2) * chunkSize);

            chunks[chunkToDisplayl, chunkPosY - 2].SetActive(false);chunksCurrentlyDisplaying[chunkToDisplayl, chunkPosY - 2] = false;
            chunks[chunkToDisplaym, chunkPosY - 2].SetActive(false);chunksCurrentlyDisplaying[chunkToDisplaym, chunkPosY - 2] = false;
            chunks[chunkToDisplayr, chunkPosY - 2].SetActive(false);chunksCurrentlyDisplaying[chunkToDisplayr, chunkPosY - 2] = false;
        }

    }

    void DisplayChunk(ushort cx, ushort cy)        //LOADS A CHUNK - USE WHEN CHUNK IS NOT POPULATED YET
    {
        
        if(chunksLoadedIntoMemory[cx, cy])
        {
            if(!chunksCurrentlyDisplaying[cx, cy])
            {
                chunks[cx, cy].SetActive(true);
                chunksCurrentlyDisplaying[cx, cy] = true;
            }
            return;
        }

        ushort fetchPosX = 0;
        ushort fetchPosY = 0;
        GameObject tile;
        for (ushort x = 0; x < chunkSize; ++x)
        {
            for (ushort y = 0; y < chunkSize; y++)
            {
                fetchPosX = (ushort)(x + (cx * chunkSize));
                fetchPosY = (ushort)(y + (cy * chunkSize));
                //PLACE TILES
                

                //FRONT TILE
                tile = GetTileToPlace(fetchPosX, fetchPosY, frontTilesValue);
                if (tile)
                {
                    //tile = Instantiate(tile);
                    tile.transform.SetParent(chunks[cx, cy].transform);
                    tile.transform.localPosition = new Vector2(x, y);
                    frontTiles[fetchPosX, fetchPosY] = tile;
                    PlaceTileInFrontLayer(tile);
                }
                //BACK TILES
                tile = GetTileToPlace(fetchPosX, fetchPosY, backTilesValue);
                if (tile)
                {
                    //tile = Instantiate(tile);
                    tile.transform.SetParent(chunks[cx, cy].transform);
                    tile.transform.localPosition = new Vector2(x, y);
                    backTiles[fetchPosX, fetchPosY] = tile;
                    PlaceTileInBackLayer(tile);
                }
                //VEGETATION TILES
                tile = GetTileToPlace(fetchPosX, fetchPosY, vegetationTilesValue);
                if (tile)
                {
                    //tile = Instantiate(tile);
                    tile.transform.SetParent(chunks[cx, cy].transform);
                    tile.transform.localPosition = new Vector2(x, y);
                    vegetationTiles[fetchPosX, fetchPosY] = tile;
                    PlaceTileInVegetationtLayer(tile);
                }
                //FRONT RESOURCES
                tile = GetTileToPlace(fetchPosX, fetchPosY, frontTilesResourceValue);
                if (tile)
                {
                    tile.transform.SetParent(chunks[cx, cy].transform);
                    tile.transform.localPosition = new Vector2(x, y);
                    frontTilesResources[fetchPosX, fetchPosY] = tile;
                    PlaceTileInFResourceLayer(tile);
                }
                //BACK RESOURCES
                tile = GetTileToPlace(fetchPosX, fetchPosY, backTilesResourceValue);
                if (tile)
                {
                    tile.transform.SetParent(chunks[cx, cy].transform);
                    tile.transform.localPosition = new Vector2(x, y);
                    backTilesResources[fetchPosX, fetchPosY] = tile;
                    PlaceTileInBResourceLayer(tile);
                }


            }
        }
        chunks[cx, cy].SetActive(true);
        chunksLoadedIntoMemory[cx, cy] = true;
        chunksCurrentlyDisplaying[cx, cy] = true;

    }

    GameObject GetTileToPlace (ushort x, ushort y, ushort [,] tileLayer)
    {
        GameObject tile = null;

        switch (tileLayer[x,y])
        {
            case (ushort)EnumClass.TileEnum.EMPTY:
                break;
            case (ushort)EnumClass.TileEnum.REGULAR_DIRT:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = regularDirtSprites[UnityEngine.Random.Range(0, regularDirtSprites.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.REGULAR_DIRT;
                break;
            case (ushort)EnumClass.TileEnum.REGULAR_STONE:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = regularStoneSprites[UnityEngine.Random.Range(0, regularStoneSprites.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.REGULAR_STONE;
                break;
            case (ushort)EnumClass.TileEnum.REGULAR_WOOD:
                Debug.LogError("WOOD GAMEOBJECT MISSING");
                break;
            case (ushort)EnumClass.TileEnum.REGULAR_TREETRUNK:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = regularTreeCoreSprite[UnityEngine.Random.Range(0, regularTreeCoreSprite.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.REGULAR_TREETRUNK;
                break;
            case (ushort)EnumClass.TileEnum.REGULAR_TREELEAF:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = regularTreeTopSprite[UnityEngine.Random.Range(0, regularTreeTopSprite.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.REGULAR_TREELEAF;
                break;
            case (ushort)EnumClass.TileEnum.REGULAR_FLOWER:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = regularFlowerSprites[UnityEngine.Random.Range(0, regularFlowerSprites.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.REGULAR_FLOWER;
                break;
            case (ushort)EnumClass.TileEnum.REGULAR_GRASS:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = regularGrassSprites[UnityEngine.Random.Range(0, regularGrassSprites.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.REGULAR_GRASS;
                break;
            case (ushort)EnumClass.TileEnum.COAL:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = coalSprites[UnityEngine.Random.Range(0, coalSprites.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.COAL;
                break;
            case (ushort)EnumClass.TileEnum.COPPER:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = copperSprites[UnityEngine.Random.Range(0, copperSprites.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.COPPER;
                break;
            case (ushort)EnumClass.TileEnum.SILVER:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = silverSprites[UnityEngine.Random.Range(0, silverSprites.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.SILVER;
                break;
            case (ushort)EnumClass.TileEnum.GOLD:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = goldSprites[UnityEngine.Random.Range(0, goldSprites.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.GOLD;
                break;
            case (ushort)EnumClass.TileEnum.DIAMOND:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = diamondSprites[UnityEngine.Random.Range(0, diamondSprites.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.DIAMOND;
                break;
            case (ushort)EnumClass.TileEnum.MOON_DIRT:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = moonDirtSprites[UnityEngine.Random.Range(0, moonDirtSprites.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.MOON_DIRT;
                break;
            case (ushort)EnumClass.TileEnum.MOON_STONE:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = moonStoneSprites[UnityEngine.Random.Range(0, moonStoneSprites.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.MOON_STONE;
                break;
            case (ushort)EnumClass.TileEnum.MOON_SAND:
                tile = Instantiate(TileObjectPrefab);
                tile.GetComponent<SpriteRenderer>().sprite = moonSandSprite[UnityEngine.Random.Range(0, moonSandSprite.Length)];
                tile.GetComponent<TileScript>().tileId = (ushort)EnumClass.TileEnum.MOON_SAND;
                break;
            case (ushort)EnumClass.TileEnum.CAMPFIRE:
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
        tile.GetComponent<SpriteRenderer>().sortingOrder = (int)EnumClass.LayerIDEnum.FRONTLAYER;
    }

    void PlaceTileInBackLayer(GameObject tile)
    {
        tile.GetComponent<SpriteRenderer>().sortingOrder = (int)EnumClass.LayerIDEnum.BACKLAYER;
        tile.GetComponent<Collider2D>().enabled = false;
        tile.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    void PlaceTileInVegetationtLayer(GameObject tile)
    {
        tile.GetComponent<Collider2D>().enabled = false;
        tile.GetComponent<SpriteRenderer>().sortingOrder = (int)EnumClass.LayerIDEnum.GRASS;
    }

    void PlaceTileInFResourceLayer(GameObject tile)
    {
        tile.GetComponent<Collider2D>().enabled = false;
        tile.GetComponent<SpriteRenderer>().sortingOrder = (int)EnumClass.LayerIDEnum.FRONTLAYER_RESOURCES;
    }

    void PlaceTileInBResourceLayer(GameObject tile)
    {
        tile.GetComponent<SpriteRenderer>().sortingOrder = (int)EnumClass.LayerIDEnum.BACKLAYER_RESOURCES;
        tile.GetComponent<Collider2D>().enabled = false;
        tile.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public GameObject MineTile(int x, int y)
    {
        Debug.Log("mine tile called");
        ushort relativeX = (ushort)Mathf.Floor((x % worldXDimension + worldXDimension) % worldXDimension);
        ushort relativeY = (ushort)Mathf.Floor(y / chunkSize);

        Debug.Log(relativeX+ " " + relativeY);

        if (frontTilesValue[relativeX, relativeY] == (ushort)EnumClass.TileEnum.EMPTY) { Debug.Log(frontTilesValue[relativeX, relativeY]); return null; }

        player.GetComponent<InventoryScript>().AddItemToInventory(GetTileToPlace((ushort)relativeX, (ushort)y, frontTilesValue), 1);

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
        if(frontTilesValue[relativeX, y] == (ushort)EnumClass.TileEnum.EMPTY)
        {
            ushort posXinChunk = (ushort)(Mathf.Floor((x % chunkSize + chunkSize)) % chunkSize);
            ushort posYinChunk = (ushort)(y % chunkSize);

            ushort chunkX = (ushort)Mathf.Floor(relativeX / chunkSize);
            ushort chunkY = (ushort)Mathf.Floor(y / chunkSize);

            //playerInventoryScript.RemoveItemFromInventory(t, 1);
            GameObject tile = Instantiate(t);

            frontTilesValue[relativeX, y] = (ushort)EnumClass.TileEnum.REGULAR_STONE;
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

    public Vector2 GetSafePlaceToSpawnPlayer()
    {
        Vector2 pos = new Vector2(0f,0f);
        ushort x = (ushort)UnityEngine.Random.Range(0, worldXDimension - 1);
        for(ushort y = (ushort)(worldYDimension - 1); y > 0; --y)
        {
            if(frontTilesValue[x, y] != (ushort)EnumClass.TileEnum.EMPTY)
            {
                pos = new Vector2(x, y + 3);
                break;
            }
        }
        return pos;
    }

}
