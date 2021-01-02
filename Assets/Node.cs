using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    // Position on the grid
    public int x;
    public int y;
    // Position on the playing surface 
    public Vector2 worldPosition;

    // Distance from end node
    public int hCost;
    // Distance from starting node
    public int gCost;

    // Is Node a obstacle?
    public bool bisWall;

    public Node parent;

    public GameObject g;

    public enum type 
    {
        environment,
        obstalce,
        start,
        end,
        path
    }

    public type Type;
        
    // Sum of gCost and hCost
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public Node(type _type, int _x, int _y, Vector2 _worldPosition)
    {
        bisWall = _bisWall;
        x = _x;
        y = _y;
        worldPosition = _worldPosition;
        Type = _type;
    }
}
