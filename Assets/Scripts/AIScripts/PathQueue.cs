using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathQueue {

    public static bool pathQueueThreadActive = false;

    private static int maxLowPrioRequests = 20;
    private static int maxHighPrioRequests = 20;

    private static Queue<QueueObject> lowPriorityRequestQueue = new Queue<QueueObject>();
    private static Queue<QueueObject> highPriorityRequestQueue = new Queue<QueueObject>();

    public static void AddToLowPrioQue(Enemy enemy, Vector2 sPos, Vector2 ePos, ushort[,] sTerrain)
    {
        if (lowPriorityRequestQueue.Count >= maxLowPrioRequests) return;
        lowPriorityRequestQueue.Enqueue(new QueueObject(enemy, sPos, ePos, sTerrain));
    }

    public static void AddToHighPrioQue(Enemy enemy, Vector2 sPos, Vector2 ePos, ushort[,] sTerrain)
    {
        if (highPriorityRequestQueue.Count >= maxHighPrioRequests) return;
        highPriorityRequestQueue.Enqueue(new QueueObject(enemy, sPos, ePos, sTerrain));
    }

    private static void StartThread()
    {

    }


}

public class QueueObject
{
    private Enemy enemyObject;
    private Vector2 startPos;
    private Vector2 endPos;
    ushort[,] surroundingTerrain;


    public QueueObject(Enemy enemy, Vector2 sPos, Vector2 ePos, ushort[,] sTerrain)
    {
        enemyObject = enemy;
        startPos = sPos;
        endPos = ePos;
        surroundingTerrain = sTerrain;
    }

}
