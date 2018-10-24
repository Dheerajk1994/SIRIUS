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
            frontTiles[x, y] = Instantiate(tile, new Vector2(x, y), Quaternion.identity);
            return true;
        }
        return false;
    }



}
