using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar
{
    
}

public class Spot
{
    public int F;
    public int G;
    public int H;
    public Spot()
    {
        F = 0;
        G = 0;
        H = 0;
    }
}