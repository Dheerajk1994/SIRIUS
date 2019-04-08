using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ExplorerScript : MonoBehaviour
{
    //public delegate void PathDelegate(List<Vector2> potentialPath);

    Vector2 currentPos;
    Vector2 targetPos;

    public float moveSpeed = 5f;
    public float jumpForce = 50f;

    List<Vector2> path;
    TerrainManagerScript terrainManagerScript;
    GameObject player;
    Vector2 playerPos;
    Vector2 currentPlayerPos;

    Thread pathThread;
    private bool threadActive = false;

    private void Awake()
    {
        terrainManagerScript = GameObject.Find("TerrainManager(Clone)").GetComponent<TerrainManagerScript>();
        player = GameObject.Find("Sam(Clone)");
        playerPos = player.transform.position;
        currentPlayerPos = playerPos;
        path = new List<Vector2>();
    }

    private void Update()
    {
        currentPlayerPos = player.transform.position;
        currentPos = this.transform.position;
        if(currentPlayerPos != playerPos && !threadActive)
        {
            threadActive = true;
            playerPos = currentPlayerPos;
            //pathThread = new Thread(AStar.FindPath(currentPos, playerPos, terrainManagerScript.frontTilesValue, this));
            pathThread = new Thread(() => AStar.FindPath(currentPos, playerPos, terrainManagerScript.frontTilesValue, this));
            pathThread.Start();
           // Debug.Log("thread called");
        }
        Move();
    }

    public void SetPotentialPath(List<Vector2> pPath)
    {
        //Debug.Log("thread returned");
        if (pPath != null && pPath.Count > 0)
        {
            path = pPath;
            targetPos = path[0];
        }
        threadActive = false;
    }

    private void Move()
    {
        if (path == null && !(path.Count > 0)) return;
        if ( Vector2.Distance(currentPos, targetPos) < 0.05f) path.Remove(targetPos);
        //if (currentPos == targetPos) path.Remove(targetPos);
        if (path.Count == 0) return;
        targetPos = path[0];
        if (targetPos.y > currentPos.y) this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
        transform.position = Vector2.MoveTowards(currentPos, targetPos, Time.deltaTime * moveSpeed);

    }

    private void OnDrawGizmos()
    {
        if (path.Count == 0) return;
        Gizmos.color = Color.red;
        foreach (Vector2 pos in path)
        {
            Gizmos.DrawCube(pos, Vector3.one * 0.3f);
        }
    }


}

