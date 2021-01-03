using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Sprite sprite;
    public Node[,] Grid;
    int Columns, Rows;

    Astar astar;
    protected Vector2 Start;
    protected Vector2 End;

    void Awake()
    {
        astar = GetComponent<Astar>();
        Start = astar.Start;
        End = astar.End;

        Columns = (int)Camera.main.orthographicSize * 2;
        Rows = Columns;
        CreateGrid();
    }

    void CreateGrid()
    {
        Grid = new Node[Columns, Rows];

        for (int y = 0; y < Columns; y++)
        {
            for (int x = 0; x < Rows; x++)
            {
                var type = Node.type.environment;
                if (x == Start.x && y == Start.y)
                {
                    type = Node.type.start;
                }
                else if (x == End.x && y == End.y)
                {
                    type = Node.type.end;
                }

                Grid[x, y] = new Node(type, x, y, new Vector2(x-((int)Camera.main.orthographicSize - 0.5f), y - ((int)Camera.main.orthographicSize - 0.5f)));
                SpawnTile(Grid[x,y],x,y);
            }
        }

        UpdateTile();
    }
    
    public Node GetNodeFromGrid(Vector2 gridPosition)
    {
        return Grid[(int)gridPosition.x, (int)gridPosition.y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        // Checking neighbors
        for(int x = -1; x<=1;x++)
        {
            for(int y = -1; y<= 1;y++)
            {
                if(x==0 && y==0)
                {
                    continue;
                }

                int checkX = node.x + x;
                int checkY = node.y + y;

                // Checking that it doesn't go beyond the Grid
                if (checkX >= 0 && checkX < Rows && checkY >0 && checkY < Columns)
                {
                    neighbours.Add(Grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    /*
    Checking if the corner has the form:
    []
      []
    instead of:
    [][]
      []
    FOR FOUR CASES:
    */
    public bool TopRightCorner(Node node)
    {
        // Checking that it doesn't go beyond the Grid
        if(node.y + 1 < Rows && node.x + 1 < Columns)
        {
            if (Grid[node.x, node.y + 1].Type == Node.type.obstalce && Grid[node.x + 1, node.y].Type == Node.type.obstalce)
            {
                return true;
            }
        }

        return false;
    }

    public bool TopLeftCorner(Node node)
    {
        if (node.y + 1 < Rows && node.x - 1 >= 0)
        {
            if (Grid[node.x, node.y + 1].Type == Node.type.obstalce && Grid[node.x - 1, node.y].Type == Node.type.obstalce)
            {
                return true;
            }
        }

        return false;
    }

    public bool DownRightCorner(Node node)
    {
        if (node.y - 1 >= 0 && node.x + 1 < Columns)
        {
            if (Grid[node.x, node.y - 1].Type == Node.type.obstalce && Grid[node.x + 1, node.y].Type == Node.type.obstalce)
            {
                return true;
            }
        }

        return false;
    }

    public bool DownLeftCorner(Node node)
    {
        if (node.y - 1 >= 0 && node.x - 1 >= 0)
        {
            if (Grid[node.x, node.y - 1].Type == Node.type.obstalce && Grid[node.x - 1, node.y].Type == Node.type.obstalce)
            {
                return true;
            }
        }
        return false;
    }

    private void SpawnTile(Node node, int x, int y)
    {
        node.g = new GameObject("X: " + x + "Y: " + y);
        node.g.transform.position = node.worldPosition;
        node.g.AddComponent<SpriteRenderer>();
    }

    public void UpdateTile()
    {
        foreach(Node n in Grid)
        {
            var s = Grid[n.x, n.y].g.GetComponent<SpriteRenderer>();
            s.sprite = sprite;

            if (n.Type == Node.type.start)
            {
                s.color = new Color(0, 1, 0);
            }
            else if (n.Type == Node.type.end)
            {
                s.color = new Color(0, 0, 1);
            }
            else if (n.Type == Node.type.path)
            {
                s.color = new Color(0, 0, 0);
            }
            else if (n.Type == Node.type.obstalce)
            {
                s.color = new Color(1, 0, 0);
            }
            else
            {
                s.color = new Color(1, 1, 1);
            }
        }

    }

    public void GenerateObstacle(Vector2 mousePosition)
    {  
        foreach (Node n in Grid)
        {
            float checkX = n.worldPosition.x - Camera.main.ScreenToWorldPoint(mousePosition).x;
            float checkY = n.worldPosition.y - Camera.main.ScreenToWorldPoint(mousePosition).y;
            if (Mathf.Abs(checkX) <= 0.5f && Mathf.Abs(checkY) <= 0.5f)
            {
                if(n.Type == Node.type.obstalce)
                {
                    n.Type = Node.type.environment;
                }
                else
                {
                    n.Type = Node.type.obstalce;
                }
            }

        }
    }
}
