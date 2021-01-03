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

    // From which neighbor it evolved
    public Node parent;

    // Object visualization 
    public GameObject g;

    // Type of Node
    public enum type 
    {
        environment,
        obstalce,
        start,
        end,
        path,
    }

    public type Type { get; set; }
        
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
        Type = _type;
        x = _x;
        y = _y;
        worldPosition = _worldPosition;
    }
}
