using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar 
{

    public Node[,] nodes;
    public AStar(Vector3Int[,] tiles, int columns, int rows)
    {
        nodes = new Node[columns, rows];
    }

    //Function to find the path between two points
    public List<Node> FindPath(Vector3Int[,] grid, Vector3Int start, Vector3Int end)
    {
       
        Node Start = null;
        Node End = null;
        var columns = nodes.GetUpperBound(0) + 1;
        var rows = nodes.GetUpperBound(1) + 1;
        nodes = new Node[columns, rows];

        //Create nodes from the grid
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                nodes[i, j] = new Node(grid[i, j].x, grid[i, j].y, grid[i, j].z);
            }
        }

        //Find the neighbors of each node as well as our start and end nodes
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                nodes[i, j].FindNeighbors(nodes, i, j);
                if (nodes[i, j].x == start.x && nodes[i, j].y == start.y)
                    Start = nodes[i, j];
                else if (nodes[i, j].x == end.x && nodes[i, j].y == end.y)
                    End = nodes[i, j];
            }
        }


        //A* algorithm open and closed list
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        //Add the starting node to the list
        openList.Add(Start);

        //While the open list is not empty
        while (openList.Count > 0)
        {

            int bestNode = 0;

            //Find the node with the lowest fCost
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].fCost < openList[bestNode].fCost || openList[i].fCost == openList[bestNode].fCost && openList[i].hCost < openList[bestNode].hCost)
                {
                    bestNode = i;
                }
            }

            //Set the current node to the node with the lowest fCost
            var current = openList[bestNode];

            //If the current node is the end node, we have found the path
            if (End != null && current == End)
            {
                List<Node> path = new List<Node>();
                var temp = current;
                path.Add(temp);
                while (temp.parent != null)
                {
                    path.Add(temp.parent);
                    temp = temp.parent;
                }
                
                return path;
            }
            
            //Remove the current node from the open list and add it to the closed list
            openList.Remove(current);
            closedList.Add(current);

            //Find the neighbors of the current node
            var neighbors = current.Neighbors;

            //For each neighbor
            for (int i = 0; i < neighbors.Count; i++)
            {
                //If the neighbor is not in the closed list and is not a wall
                if (!closedList.Contains(neighbors[i]) && neighbors[i].z < 1)
                {

                    //Calculate the new gCost
                    int newMovementCostToNeighbor = current.gCost + 1;
                    
                    bool newPath = false;

                    //If the neighbor is in the open list
                    if (openList.Contains(neighbors[i]))
                    {
                        //If the new gCost is less than the old gCost
                        if (newMovementCostToNeighbor < neighbors[i].gCost)
                        {
                            //Set the new gCost
                            neighbors[i].gCost = newMovementCostToNeighbor;
                            newPath = true;
                        }
                    }
                    else
                    {
                        //Set the new gCost
                        neighbors[i].gCost = newMovementCostToNeighbor;
                        newPath = true;
                        openList.Add(neighbors[i]);
                    }

                    //If the neighbor is a new path
                    if (newPath)
                    {
                        //Set the parent of the neighbor to the current node
                        neighbors[i].parent = current;
                        neighbors[i].CalculateHCost(End);
                        neighbors[i].CalculateFCost();
                    }
                }
            }
        }
        return null;

    }
    
}

//Class for the nodes inside our grid
public class Node
{

    //Variables for the node
    public int x;
    public int y;  
    public int z;
    public int gCost;
    public int hCost;
    public int fCost;
    public Node parent;
    public List<Node> Neighbors;

    //Constructor for the node
    public Node(int x, int y, int z)
    {
        fCost = 0;
        gCost = 0;
        hCost = 0;
        this.x = x;
        this.y = y;
        this.z = z;
        Neighbors = new List<Node>();

    }

    //Calculate the F cost of the node by adding the G and H cost
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    //Calculate the H cost of the node by using the Manhattan distance
    public void CalculateHCost(Node endNode)
    {
        hCost = Mathf.Abs(endNode.x - x) + Mathf.Abs(endNode.y - y);
    }

    //Find the neighbors of the node
    public void FindNeighbors(Node[,] grid, int x, int y)
    {
        if (x < grid.GetUpperBound(0))
            Neighbors.Add(grid[x + 1, y]);
        if (x > 0)
            Neighbors.Add(grid[x - 1, y]);
        if (y < grid.GetUpperBound(1))
            Neighbors.Add(grid[x, y + 1]);
        if (y > 0)
            Neighbors.Add(grid[x, y - 1]);
    }
}
