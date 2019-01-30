using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManagerScript : MonoBehaviour
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


    public GameObject[,] frontTiles;
    private uint frontTilesXdim;
    private uint frontTilesYdim;
    public GameObject[,] backTiles;
    private uint backTilesXdim;
    private uint backTilesYdim;
    public GameObject[,] vegetationTiles;   //USED TO STORE PLANTS, GRASS, AND OTHER VEGETATION
    private uint vegetationTilesXDim;
    private uint vegetationTilesYDim;

    public int backTileLayerID;
    public int frontTileLayerID;
    public int grassLayerID;

    public GameObject player;
    private InventoryScript playerInventoryScript;


    public void SetTiles(ushort[,] fTilesValue, ushort[,] bTilesValue, ushort[,] vTilesValue)
    {
        frontTilesXdim = (uint)fTilesValue.GetLength(0);
        frontTilesYdim = (uint)fTilesValue.GetLength(1);

        backTilesXdim = (uint)bTilesValue.GetLength(0);
        backTilesYdim = (uint)bTilesValue.GetLength(1);

        vegetationTilesXDim = (uint)vTilesValue.GetLength(0);
        vegetationTilesYDim = (uint)vTilesValue.GetLength(0);

        frontTiles = new GameObject[frontTilesXdim, frontTilesYdim];
        backTiles = new GameObject[backTilesXdim, backTilesYdim];
        vegetationTiles = new GameObject[vegetationTilesXDim, vegetationTilesYDim];

        CreateTileGameobjects(fTilesValue, bTilesValue, vTilesValue);
    }


    public GameObject MineTile(int x, int y)
    {
        if (x >= 0 && x < frontTilesXdim && y >= 0 && y < frontTilesYdim && frontTiles[x,y] != null)
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
        if (x >= 0 && x < frontTilesXdim && y >= 0 && y < frontTilesYdim && frontTiles[x, y] == null)
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





    private void CreateTileGameobjects(ushort [,] frontTilesValue, ushort[,] backTilesValue, ushort[,] vegetationTilesValue)
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

}
