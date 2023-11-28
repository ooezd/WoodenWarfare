using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid<T>
{
    float cellSize;
    int width;
    int height;

    T[,] gridArray;
    Vector3 startPoint;
    public CustomGrid(float cellSize) 
    {
        this.cellSize = cellSize;
    }
    public void GenerateGridFromEdgeCollider(EdgeCollider2D edge, Func<CustomGrid<T>,int,int,T> createGridObject)
    {
        var max = edge.bounds.max;
        var min = edge.bounds.min;
        startPoint = min;

        var diff = max - min;
        width = Mathf.CeilToInt(diff.x / cellSize);
        height = Mathf.CeilToInt(diff.y / cellSize);

        gridArray = new T[width, height];

        for(int x = 0; x < gridArray.GetLength(0); x++) 
        { 
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 50);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 50);
            }
        }

        //Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 50);
        //Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 50);

    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return startPoint + new Vector3(x, y) * cellSize;
    }
    
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition-startPoint).x / cellSize);
        y = Mathf.FloorToInt((worldPosition-startPoint).y / cellSize);
    }

    public void SetValue(int x, int y, T value)
    {
        if (IsCoordinateValid(x, y))
        {
            gridArray[x, y] = value;
        }
    }

    public void SetValue(Vector3 worldPosition, T value)
    {
        GetXY(worldPosition, out int x, out int y);
        SetValue(x, y, value);
    }

    public T GetValue(int x, int y)
    {
        if (IsCoordinateValid(x, y))
        {
            return gridArray[x, y];
        }
        else
            return default;
    }
    public T GetValue(Vector3 worldPos)
    {
        GetXY(worldPos, out int x, out int y);
        return GetValue(x, y);
    }
    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    bool IsCoordinateValid(int x, int y)
    {
        return !(x < 0 || y < 0 || x > width || y > height);
    }

    private void DrawGrid()
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                var color = Color.green;
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), color, 20);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), color, 20);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 20);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 20);
    }
}
