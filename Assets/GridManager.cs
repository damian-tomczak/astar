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
    public List<Node> path;

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
                bool inp = false;
                var obstalce = Random.Range(0, 69);
                if (obstalce % 11 == 0)
                    inp = true;

                Grid[x, y] = new Node(inp, x, y, new Vector2(x-((int)Camera.main.orthographicSize - 0.5f), y - ((int)Camera.main.orthographicSize - 0.5f)));
                SpawnTile(Grid[x,y],x,y);
            }
        }
    }
    
    public Node GetNodeFromGrid(Vector2 gridPosition)
    {
        return Grid[(int)gridPosition.x, (int)gridPosition.y];
    }

    public List<Node> GetNeigbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

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

                if (checkX >= 0 && checkX < Rows && checkY >0 && checkY < Columns)
                {
                    neighbours.Add(Grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    private void SpawnTile(Node node, int x, int y)
    {
        node.g = new GameObject("X: " + x + "Y: " + y);
        node.g.transform.position = node.worldPosition;
        node.g.AddComponent<SpriteRenderer>();
    }

    public void UpdateTile()
    {
        for (int y = 0; y < Columns; y++)
        {
            for (int x = 0; x < Rows; x++)
            {
                var s = Grid[x,y].g.GetComponent<SpriteRenderer>();
                s.sprite = sprite;

                if (x == Start.x && y == Start.y)
                {
                    s.color = new Color(0, 1, 0);
                }
                else if (x == End.x && y == End.y)
                {
                    s.color = new Color(0, 0, 1);
                }
                else if (x == )
                {

                }
                else
                {
                    s.color = new Color(1, 1, 1);
                }

                if (path !=null)
                {
                    foreach (Node n in path)
                    {
                        Debug.Log(End.x + " " + End.y);
                        if (x == n.x && y == n.y && (x != End.x || y != End.y))
                        {
                            Debug.Log(x + " " + y);
                            s.color = new Color(0, 0, 0);
                        }

                    }
                }
            }
        }

    }
}
