using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private CustomGrid<PathNode> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;
    public bool isWalkable;

    public PathNode cameFromNode;

    public PathNode(CustomGrid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.isWalkable = true;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
        return x + "," + y;
    }
}
