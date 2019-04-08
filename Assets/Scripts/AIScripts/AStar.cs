using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public static class AStar
{
    public static void FindPath(Vector2 startPos, Vector2 endPos, ushort[,] terrain, ExplorerScript eScript)
    {
        //Stopwatch stopwatch = new Stopwatch();
        //stopwatch.Start();

        Grid grid = new Grid();
        grid.CreateGrid(terrain);

        //List<Node> openSet = new List<Node>();
        Heap<Node> openSet = new Heap<Node>(1000);
        HashSet<Node> closeSet = new HashSet<Node>();

        Node startNode;
        Node endNode;

        try
        {
            startNode = grid.grid[(int)startPos.x, (int)startPos.y];
            endNode = grid.grid[(int)endPos.x, (int)endPos.y];
            openSet.Add(startNode);
        }
        catch (Exception e)
        {
            throw e;
        }

        bool nodeFound = false;

        while (openSet.Count > 0 && !nodeFound)
        {
            //without heap
            //Node currentNode = openSet[0];
            //for (int i = 1; i < openSet.Count; i++)
            //{
            //    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
            //    {
            //        currentNode = openSet[i];
            //    }
            //}
            //openSet.Remove(currentNode);
            //without heap


            //with heap
            Node currentNode = openSet.RemoveFirstItem();
            //with heap

            closeSet.Add(currentNode);

            if (currentNode == endNode)
            {
                nodeFound = true;
            }
            else
            {
                foreach (Node neighborNode in grid.GetNeighbors(currentNode))
                {
                    if (neighborNode.isObstacle || closeSet.Contains(neighborNode))
                        continue;

                    int newDistanceToNeighbor = currentNode.gCost + GetDistance(currentNode, neighborNode);
                    if (newDistanceToNeighbor < neighborNode.gCost || !openSet.Contains(neighborNode))
                    {
                        neighborNode.gCost = newDistanceToNeighbor;
                        neighborNode.hCost = GetDistance(currentNode, endNode);
                        neighborNode.parent = currentNode;
                        if (!openSet.Contains(neighborNode))
                        {
                            openSet.Add(neighborNode);
                        }
                    }
                }
            }
        }

        List<Vector2> path = new List<Vector2>();
        if (!nodeFound) {
            eScript.SetPotentialPath(path);
            //return path;
            return;
        }
        Node currenNode = endNode;

        while (currenNode != startNode)
        {
            Vector2 pos = new Vector2(currenNode.xPos, currenNode.yPos);
            path.Add(pos);
            currenNode = currenNode.parent;
        }
        //stopwatch.Stop();
        //UnityEngine.Debug.Log("path finding took: " + stopwatch.ElapsedMilliseconds + " ms");
        path.Reverse();

        eScript.SetPotentialPath(path);
        //return path;
    }

    static int GetDistance(Node a, Node b)
    {
        int xDif = Mathf.Abs(a.xPos - b.xPos);
        int yDif = Mathf.Abs(a.yPos - b.yPos);

        if (xDif < yDif)
        {
            return ((xDif * 14) + (10 * (yDif - xDif)));
        }
        else
        {
            return ((yDif * 14) + (10 * (xDif - yDif)));
        }
    }
}

class Grid
{
    public Node[,] grid;

    int xDim;
    int yDim;

    public int MaxSize
    {
        get { return xDim * yDim; }
    }

    public void CreateGrid(ushort[,] terrain)
    {
        xDim = terrain.GetLength(0);
        yDim = terrain.GetLength(1);
        grid = new Node[xDim, yDim];

        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                bool obstacle = (terrain[x, y] == 0) ? false : true;
                if (!obstacle)
                {
                    if (y - 1 > 0 && x - 1 > 0 && x + 1 < xDim && y + 1 < yDim)
                    {
                        if (
                            terrain[x, y - 1] == 0 &&
                            terrain[x, y + 1] == 0 &&
                            terrain[x - 1, y] == 0 &&
                            terrain[x + 1, y] == 0
                            )
                        {
                            obstacle = true;    
                        }
                    }
                }
                Node n = new Node(obstacle, x, y);
                grid[x, y] = n;
            }
        }
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();
        //NEED OPTIMIZATION
        int xCheck, yCheck;

        //left tile
        xCheck = node.xPos - 1;
        yCheck = node.yPos + 0;
        if (xCheck >= 0 && xCheck < xDim)
        {
            neighbors.Add(grid[xCheck, yCheck]);
        }

        //right tile
        xCheck = node.xPos + 1;
        yCheck = node.yPos + 0;
        if (xCheck >= 0 && xCheck < xDim)
        {
            neighbors.Add(grid[xCheck, yCheck]);
        }

        //top tile
        xCheck = node.xPos + 0;
        yCheck = node.yPos + 1;
        if (yCheck >= 0 && yCheck < yDim)
        {
            neighbors.Add(grid[xCheck, yCheck]);
        }

        //bottom tile
        xCheck = node.xPos + 0;
        yCheck = node.yPos - 1;
        if (yCheck >= 0 && yCheck < yDim)
        {
            neighbors.Add(grid[xCheck, yCheck]);
        }

        //top left
        xCheck = node.xPos - 1;
        yCheck = node.yPos + 1;
        if (xCheck >= 0 && xCheck < xDim && yCheck >= 0 && yCheck < yDim)
        {
            neighbors.Add(grid[xCheck, yCheck]);
        }

        //top right
        xCheck = node.xPos + 1;
        yCheck = node.yPos + 1;
        if (xCheck >= 0 && xCheck < xDim && yCheck >= 0 && yCheck < yDim)
        {
            neighbors.Add(grid[xCheck, yCheck]);
        }

        //bot left
        xCheck = node.xPos - 1;
        yCheck = node.yPos - 1;
        if (xCheck >= 0 && xCheck < xDim && yCheck >= 0 && yCheck < yDim)
        {
            neighbors.Add(grid[xCheck, yCheck]);
        }

        //bot right
        xCheck = node.xPos + 1;
        yCheck = node.yPos - 1;
        if (xCheck >= 0 && xCheck < xDim && yCheck >= 0 && yCheck < yDim)
        {
            neighbors.Add(grid[xCheck, yCheck]);
        }



        return neighbors;
    }
}


class Node : IHeapItem<Node>
{
    public bool isObstacle;
    public bool isVisited;

    public int gCost;
    public int hCost;

    public int xPos;
    public int yPos;

    public int heapIndex;

    public Node parent;


    public Node(bool _isObstacle, int _xPos, int _yPos)
    {
        isObstacle = _isObstacle;
        isVisited = false;
        xPos = _xPos;
        yPos = _yPos;
        parent = null;
    }

    public int fCost
    {
        get { { return gCost + hCost; } }
    }

    //interface fucntions
    public int HeapIndex
    {
        get{ return heapIndex; }
        set{ heapIndex = value; }
    }

    public int CompareTo(Node otherNode)
    {
        int compare = fCost.CompareTo(otherNode.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(otherNode.hCost);
        }
        return -compare;
    }
}

