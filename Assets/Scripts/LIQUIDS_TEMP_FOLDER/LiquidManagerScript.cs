using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidManagerScript : MonoBehaviour {

    public GameObject WaterPrefab;
    public GameObject MagmaPrefab;
    public GameObject SandPrefab;

    public static LiquidManagerScript instance;

    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            Vector2 placePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Debug.Log(placePos);
            if (!CheckIfTile((ushort)placePos.x, (ushort)placePos.y))
            {
                Debug.Log("water instantiated");
                GameObject water = Instantiate(WaterPrefab);
                water.transform.position = placePos;
            }
        }
        else if (Input.GetKey(KeyCode.K))
        {
            Vector2 placePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            if (!CheckIfTile((ushort)placePos.x, (ushort)placePos.y))
            {
                GameObject sand = Instantiate(SandPrefab);
                sand.transform.position = placePos;
            }
        }
    }

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

    public ushort[,] frontTileValues;

    public bool CheckIfTile(ushort x, ushort y)
    {
        return (frontTileValues[x, y] != 0);
    }

    public bool PlaceTile(ushort x, ushort y, ushort id)
    {
        frontTileValues[x, y] = id;
        return true;
    }
}
