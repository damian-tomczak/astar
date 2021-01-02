using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    public Vector2 Start;
    public Vector2 End;
    GridManager grid;

    float waitTime = 1.0f;
    float timer = 0.0f;
    void Awake()
    {
        grid = GetComponent<GridManager>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            FindPath(Start, End);
            grid.UpdateTile();  
            timer -= waitTime;
        }
        
    }

    void FindPath(Vector2 startPos, Vector2 endPos)
    {
        Node startNode = grid.GetNodeFromGrid(Start);
        Node endNode = grid.GetNodeFromGrid(End);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while(openSet.Count > 0)
        {

            Node node = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                    {
                        node = openSet[i];
                    }
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if(node == endNode)
            {
                FollowPath(startNode, endNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeigbours(node))
            {
                if(neighbour.bisWall || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeigbour = node.gCost + GetDistance(node, neighbour);
                if(newCostToNeigbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeigbour;
                    neighbour.hCost = GetDistance(neighbour, endNode);
                    neighbour.parent = node;

                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

    }

    void FollowPath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;

        }
        path.Reverse();

        grid.path = path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.x - nodeB.x);
        int dstY = Mathf.Abs(nodeA.y - nodeB.y);

        if(dstX>dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        else
        {
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }


}