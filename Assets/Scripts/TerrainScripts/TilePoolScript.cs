using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePoolScript : MonoBehaviour {

    private static TilePoolScript instance;
    public GameObject tile;

    public ushort tilePoolSize;

    private Queue<GameObject> tilePoolQueue;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        tilePoolQueue = new Queue<GameObject>();
        GameObject tempTile;
        for (ushort i = 0; i < tilePoolSize; ++i)
        {
            tempTile = Instantiate(tile);
            tempTile.gameObject.SetActive(false);
            tilePoolQueue.Enqueue(tempTile);
        }
    }

    public GameObject FetchTileFromPool()
    {
        if(tilePoolQueue.Peek() != null)
        {
            return tilePoolQueue.Dequeue();
        }
        return null;
    }

    public void AddTileIntoPool(GameObject tile)
    {
        tile.gameObject.SetActive(false);
        tilePoolQueue.Enqueue(tile);
    }


}
