using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePoolScript : MonoBehaviour {

    private static TilePoolScript instance;
    public GameObject tilePrefab;

    public uint tilePoolSize;

    private static Queue<GameObject> tilePoolQueue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        
    }

    public GameObject FetchTileFromPool()
    {
        if (tilePoolQueue.Peek() != null)
        {
            return tilePoolQueue.Dequeue();
        }
        return Instantiate(tilePrefab);
    }

    public void AddTileIntoPool(GameObject tileToAdd)
    {
        if(tilePoolQueue.Count < tilePoolSize)
        {
            tileToAdd.gameObject.SetActive(false);
            tilePoolQueue.Enqueue(tileToAdd);
        }
        else
        {
            Destroy(tileToAdd.gameObject);
        }
        
    }

    public int GetPoolSize()
    {
        return tilePoolQueue.Count;
    }

}
