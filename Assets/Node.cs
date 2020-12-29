using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x;
    public int y;

    public int hCost;
    public int gCost;

    public bool bisWall;


    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public Node(bool bisWall, int x, int y)
    {
        this.bisWall = bisWall;
        this.x = x;
        this.y = y;
    }
}
