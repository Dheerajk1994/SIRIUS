using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnerScript : MonoBehaviour {
   public List<Vector2> spawnLocations; //to store spawn locations
   public int sizeOfSpawnArea = 7; //mobs will only spawn in an empty(no tiles) 7x7 area

   public GameObject blobPrefab;
   private GameObject blob;

   public GameObject shroomMenPrefab;
   private GameObject shroomMen;

   private GameManagerScript gameManagerScript; 


    public bool IsASpawnableArea(ushort[,] fValue, int x, int y, int n) { //check if the nxn area is spawnable the by checking each value of front tiles
        //for(int x = i; x < i+n; x++)
        //{
        //    for(int y = j; y < j+n; y++)
        //    {
        //        if(fValue[x,y] != (ushort)EnumClass.TileEnum.EMPTY || fValue[x, y - 1] == (ushort)EnumClass.TileEnum.EMPTY)
        //        {
        //            return false;
        //        }
        //    }
        //}

        //check if middle tiel and top is empty and bottom tile is not empty
        if(fValue[x,y] == (ushort)EnumClass.TileEnum.EMPTY && fValue[x, y - 1] != (ushort)EnumClass.TileEnum.EMPTY && fValue[x, y + 1] == (ushort)EnumClass.TileEnum.EMPTY){
            return true;
        }

        return false;
    }
   public void SetMobSpawner(GameManagerScript gm)
    {
        this.gameManagerScript = gm;
    }
   public void GenerateSpawnLocations(ushort[,] fValue) { //finds spawn locations in a pxp portion of the map and then stores it as a Vector2 object into the list of spawnLocations
        spawnLocations = new List<Vector2>();
        ushort worldXDimension = gameManagerScript.terrainManagerScript.GetXDimension();
        Vector2 playerPos = gameManagerScript.player.transform.position;
        //ushort worldXDimension = fValue.Length(0);

        ushort relativeX = (ushort)Mathf.Floor((playerPos.x - 20 % worldXDimension + worldXDimension) % worldXDimension);
        ushort relativeY = (ushort)(playerPos.y - 10);

        for (int x = relativeX; x < relativeX + 40; x++)
        {
            for (int y = relativeY; y < relativeY + 40; y++)
            {
                ushort xPos = (ushort)Mathf.Floor((x % worldXDimension + worldXDimension) % worldXDimension);
                if (IsASpawnableArea(fValue, xPos, y, sizeOfSpawnArea))
                {
                    //spawnLocations.Add(new Vector2(x + (sizeOfSpawnArea / 2), y + (sizeOfSpawnArea / 2)));                    
                    spawnLocations.Add(new Vector2(xPos, y));
                }
            }
        }
    }

    public IEnumerator SpawnBlobs(ushort[,] fValue, int numberOfBlobs, GameObject playArea)
    {
        GenerateSpawnLocations(fValue);

        //make a temp list of vectors from the possible locations that are close to current position of sam
        List<Vector2> spawnAreasCloseby = new List<Vector2>();
        float distance;
        foreach(Vector2 potentialSpawns in spawnLocations){
            distance = Vector2.Distance(potentialSpawns, gameManagerScript.player.transform.position);
            if (distance > 15f && distance < 30f ){
                spawnAreasCloseby.Add(potentialSpawns);
            }
        }

        //go through these locations that are close to sam and pick random ones from it
        foreach(Vector2 pos in spawnAreasCloseby){
            if(UnityEngine.Random.Range(0, 1000) < 50){
                blob = Instantiate(blobPrefab);
                blob.transform.parent = playArea.transform;
                blob.transform.localPosition = pos;
                //Debug.Log(pos);
            }
        }

        //Debug.Log("position inside spawnlocations " + spawnLocations.Count);
        //Debug.Log("position inside spawnlocationscloseby " + spawnAreasCloseby.Count);


        //while (count <= numberOfBlobs)
        //{
        //    int randomLoc = Random.Range(0, spawnLocations.Count);
        //    blob = Instantiate(blobPrefab);
        //    blob.transform.parent = playArea.transform;
        //    blob.transform.localPosition = spawnLocations[randomLoc];
        //    Debug.Log(spawnLocations[randomLoc].x + ", " + spawnLocations[randomLoc].y);
        //    count++;
        //} 
        yield return null;
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
}
