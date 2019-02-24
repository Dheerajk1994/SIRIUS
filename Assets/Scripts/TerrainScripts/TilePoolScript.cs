using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePoolScript : MonoBehaviour {

    private static TilePoolScript instance;
    public GameObject tilePrefab;

    public uint tilePoolSize;

    private Queue<GameObject> tilePoolQueue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
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
            tempTile = Instantiate(tilePrefab);
            //tempTile.name = i.ToString();
            tempTile.gameObject.SetActive(false);
            tempTile.transform.parent = this.transform;
            tilePoolQueue.Enqueue(tempTile);
        }
    }

    public GameObject FetchTileFromPool()
    {
        if (tilePoolQueue.Peek() != null)
        {
            return tilePoolQueue.Dequeue();
        }
        return null;
    }

    public void AddTileIntoPool(GameObject tileToAdd)
    {
        tileToAdd.gameObject.SetActive(false);
        //tileToAdd.transform.parent = this.transform;
        tilePoolQueue.Enqueue(tileToAdd);
    }

    public int GetPoolSize()
    {
        return tilePoolQueue.Count;
    }

}
