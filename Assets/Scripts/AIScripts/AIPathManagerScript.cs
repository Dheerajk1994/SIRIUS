using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class AIPathManagerScript : MonoBehaviour {

    private bool pathThreadActive = false;
    public static AIPathManagerScript instance;

    [Range(1, 100)]
    [SerializeField] private readonly int maxLowPrioPathRequest = 20;

    [Range(1, 100)]
    [SerializeField] private readonly int maxHighPrioPathRequest = 20;

    private Queue<RequestToken> lowPrioPathQue = new Queue<RequestToken>();
    private Queue<RequestToken> highPrioPathQue = new Queue<RequestToken>();

    private RequestToken currentToken;
    private bool currentTokenWaitingForCallBack = false;

    private Thread aStarThread;
    List<Vector2> path;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(this != instance)
        {
            Destroy(this);
        }
    }
    
    public void SubmitLowPrioPathRequest(Vector2 sPos, Vector2 ePos, ushort[,] t, Action<List<Vector2>> cBack)
    {
        //Debug.Log("low prio path requested");
        if (lowPrioPathQue.Count >= maxLowPrioPathRequest) return;
        lowPrioPathQue.Enqueue(new RequestToken(sPos, ePos, t, cBack));
        //Debug.Log("low prio path request added " + lowPrioPathQue.Count + "/" + maxLowPrioPathRequest);

    }

    public void SubmitHighPrioPathRequest(Vector2 sPos, Vector2 ePos, ushort[,] t, Action<List<Vector2>> cBack)
    {
        //Debug.Log("high prio path requested");
        if (highPrioPathQue.Count >= maxHighPrioPathRequest) return;
        highPrioPathQue.Enqueue(new RequestToken(sPos, ePos, t, cBack));
        //Debug.Log("high prio path request added " + highPrioPathQue.Count + "/" + maxHighPrioPathRequest);

    }

    private void Update()
    {
        if (currentTokenWaitingForCallBack)
        {
            CallBackWithRequest();
        }
        if(highPrioPathQue.Count > 0 && !pathThreadActive)
        {
            currentToken = highPrioPathQue.Dequeue();
            pathThreadActive = true;
            aStarThread = new Thread(() => AStar.FindPath(currentToken.startPos, currentToken.endPos, currentToken.terrain));
            aStarThread.Start();
        }
        else if(lowPrioPathQue.Count > 0 && !pathThreadActive)
        {
            currentToken = lowPrioPathQue.Dequeue();
            pathThreadActive = true;
            aStarThread = new Thread(() => AStar.FindPath(currentToken.startPos, currentToken.endPos, currentToken.terrain));
            aStarThread.Start();
        }
    }

    public void ReturnPathFromThread(List<Vector2> p)
    {
        //Debug.Log("ai path manager return path from thread called");
        path = p;
        currentTokenWaitingForCallBack = true;
    }

    public void CallBackWithRequest()
    {
        currentToken.callback(path);
        pathThreadActive = false;
        currentTokenWaitingForCallBack = false;
    }



    private struct RequestToken
    {
        public Vector2 startPos;
        public Vector2 endPos;
        public ushort[,] terrain;
        public Action<List<Vector2>> callback;

        public RequestToken(Vector2 sPos, Vector2 ePos, ushort[,] t, Action<List<Vector2>> cBack)
        {
            startPos = sPos;
            endPos = ePos;
            terrain = t;
            callback = cBack;
        }
    }
}

