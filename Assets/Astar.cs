using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    public Vector2 Start;
    public Vector2 End;
    GridManager grid;

    List<Node> path = new List<Node>();

    void Awake()
    {
        grid = GetComponent<GridManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.GenerateObstacle(Input.mousePosition);
            grid.UpdateTile();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            FindPath(Start, End);
            grid.UpdateTile();

            if (path.Count > 0)
            {
                foreach (Node n in path)
                {
                    n.Type = Node.type.environment;
                }
                path.Clear();
            }
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
                Debug.Log("Found path!");
                FollowPath(startNode, endNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeigbours(node))
            {
                if(neighbour.Type == Node.type.obstalce || closedSet.Contains(neighbour))
                {
                    continue;
                }

                if(grid.TopRightCorner(neighbour))
                {
                    continue;
                }

                if (grid.TopLeftCorner(neighbour))
                {
                    continue;
                }

                if (grid.DownRightCorner(neighbour))
                {
                    continue;
                }

                if (grid.DownLeftCorner(neighbour))
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
 
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            currentNode.Type = Node.type.path;
            path.Add(currentNode);
            currentNode = currentNode.parent;

        }
        path.RemoveAt(0);
        endNode.Type = Node.type.end;
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