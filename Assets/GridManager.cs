using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Sprite sprite;
    public float[,] Grid;
    int Vertical, Horizontal, Columns, Rows;
    Camera camera;

    void Start()
    {
        Columns = (int)Camera.main.orthographicSize * 2;
        Rows = (int)Camera.main.orthographicSize * 2;

        Grid = new float[Columns, Rows];

        for (int i =0;i<Columns;i++)
        {
            for(int j=0;j<Rows;j++)
            {
                Grid[i, j] = Random.Range(0, 10);
                SpawnTile(i, j, Grid[i, j]);
            }
        }
    }
    
    private void SpawnTile(int x, int y, float value)
    {
        GameObject g = new GameObject("X:"+x+"Y: "+y);
        g.transform.position = new Vector3(x-((int)Camera.main.orthographicSize - 0.5f), y - ((int)Camera.main.orthographicSize - 0.5f));
        var s = g.AddComponent<SpriteRenderer>();
        s.sprite = sprite;
        s.color = new Color(1, 1, 1);
    }

}
