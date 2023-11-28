using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    public float cellSize = .2f;
    EdgeCollider2D borderCollider;
    BoxCollider2D[] obstacleColliders;
    CustomGrid<PathNode> mapGrid;

    private void Awake()
    {
        PrepareMap();
    }

    private void Start()
    {
        CreateGridMap();
    }

    private void PrepareMap()
    {
        borderCollider = GetComponentInChildren<EdgeCollider2D>();
        obstacleColliders = GetComponentsInChildren<BoxCollider2D>();
        mapGrid = new CustomGrid<PathNode>(cellSize);
    }

    private void CreateGridMap()
    {
        mapGrid.GenerateGridFromEdgeCollider(borderCollider, (CustomGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));
        foreach (var obstacle in obstacleColliders)
        {
            AddObstacle(obstacle.bounds.min, obstacle.bounds.max);
        }

        Pathfinding pf = new Pathfinding(mapGrid);
        List<PathNode> path = pf.FindPath(30, 30, 60, 40);
        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                var startPos = mapGrid.GetWorldPosition(path[i].x, path[i].y);
                var endPos = mapGrid.GetWorldPosition(path[i+1].x, path[i+1].y);
                Debug.DrawLine(startPos, endPos, Color.blue, 10);
            }
        }
        else Debug.Log("Path is null!");

    }

    public void AddObstacle(Vector2 min, Vector2 max)
    {
        mapGrid.GetXY(min, out int minX, out int minY);
        mapGrid.GetXY(max, out int maxX, out int maxY);
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                mapGrid.GetValue(x, y).isWalkable = false;
            }
        }
    }
}
