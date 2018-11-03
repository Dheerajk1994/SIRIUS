using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManagerScript : MonoBehaviour
{

    public GameObject[,] frontTiles;
    private int frontTilesXdim;
    private int frontTilesYdim;
    public GameObject[,] backTiles;
    private int backTilesXdim;
    private int backTilesYdim;

    public int backTileLayerID;
    public int frontTileLayerID;
    public int grassLayerID;


    public void SetFrontTiles(GameObject[,] fTiles)
    {
        frontTiles = fTiles;
        frontTilesXdim = fTiles.GetLength(0);
        frontTilesYdim = fTiles.GetLength(1);
    }
    public void SetBackTiles(GameObject[,] bTiles)
    {
        backTiles = bTiles;
        backTilesXdim = bTiles.GetLength(0);
        backTilesYdim = bTiles.GetLength(1);
    }

    public GameObject MineTile(int x, int y)
    {
        if (x >= 0 && x < frontTilesXdim && y >= 0 && y < frontTilesYdim)
        {
            Destroy(frontTiles[x, y]);
        }

        return null;
    }

    public bool PlaceTile(int x, int y, GameObject tile)
    {
        if (x >= 0 && x < frontTilesXdim && y >= 0 && y < frontTilesYdim && frontTiles[x, y] == null)
        {
            GameObject t  = Instantiate(tile, new Vector2(x, y), Quaternion.identity);
            t.GetComponent<SpriteRenderer>().sortingOrder = frontTileLayerID;
            frontTiles[x, y] = t;
            return true;
        }
        return false;
    }



}
