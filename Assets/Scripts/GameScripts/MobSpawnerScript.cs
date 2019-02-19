using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnerScript : MonoBehaviour {
   public List<Vector2> spawnLocations; //to store spawn locations
   public int sizeOfSpawnArea = 7; //mobs will only spawn in an empty(no tiles) 7x7 area

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsASpawnableArea(ushort[,] fValue, int i, int j, int n) { //check if the nxn area is spawnable the by checking each value of front tiles
        for(int x = i; x < i+n; x++)
        {
            for(int y = j; y < j+n; y++)
            {
                if(fValue[x,y] != (ushort)TileEnum.EMPTY)
                {
                    return false;
                }
            }
        }

        return true;
    }
   public void generateSpawnLocations(ushort[,] fValue, int p) { //finds spawn locations in a pxp portion of the map and then stores it as a Vector2 object into the list of spawnLocations

        for (int x = 0; x < p; x++)
        {
            for (int y = 0; y < p; y++)
            {
                if (IsASpawnableArea(fValue, x, y, sizeOfSpawnArea))
                {
                    Debug.Log("original : (" + x + ", " + y + ")" + " middle: (" + (x + (sizeOfSpawnArea/2)) + ", " + (y + (sizeOfSpawnArea / 2)) + ")" );
                    spawnLocations.Add(new Vector2(x + (sizeOfSpawnArea/2), y + (sizeOfSpawnArea / 2)));                    
                }
            }
        }
        Debug.Log("Hi");
    }
    public void printSpawnLocations() {
        foreach (Vector2 loc in spawnLocations)
        {
            Debug.Log("(" + loc.x + ", " + loc.y + ")");
        }
    }
    public void printNumberOfSpawnLocations() {
        Debug.Log(spawnLocations.Count);
    }
    /*
    public int NumberOfSpawnableAreas(ushort[,] fValue, int p) { // return the number of spawnable areas in a pxp portion of a map 
        int spawnableAreas = 0;
        for (int x = 0; x < p; x++)
        {
            for(int y = 0; y < p; y++)
            {
                if (IsASpawnableArea(fValue, x, y, sizeOfArea))
                {
                    spawnableAreas++;
                    Debug.Log("fValues: [" + x + "," + y + "] to [" + (x+sizeOfArea) + "," + (y+sizeOfArea) + "] " +  " = " + fValue[x, y] + ", " + fValue[x+1, y+1] + ", " + fValue[x+2, y+2] + ", " + fValue[x+3, y+3] + ", " + fValue[x+4, y+4] + ", " + fValue[x+5, y+5] + ", " + fValue[x+6, y+6]);
                }
            }
        }
        return spawnableAreas;
    }
    */

}
