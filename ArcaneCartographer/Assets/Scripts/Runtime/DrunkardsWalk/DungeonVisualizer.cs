using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonVisualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject FloorPrefab;
    [SerializeField]
    private GameObject wallPrefab;

    private List<GameObject> gameObjects = new List<GameObject>();

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, FloorPrefab);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, GameObject floorTile)
    {
        foreach (var pos in positions) 
        {
            PaintSingleTile(pos, floorTile);
        }
    }

    private void PaintSingleTile(Vector2Int pos, GameObject floorTile)
    {
        Vector3 worldSpace = new Vector3(pos.x, 0, pos.y);
        GameObject currentTile = Instantiate(floorTile, worldSpace, Quaternion.identity);
        gameObjects.Add(currentTile);    

    }

    public void Clear()
    {
        foreach(var tile in gameObjects)
        {
            if (Application.isEditor)
            {
                DestroyImmediate(tile);
            }
            else
            {
                Destroy(tile);
            }
        }
        gameObjects.Clear();
    }

    internal void PaintSingleWall(Vector2Int pos)
    {
        PaintSingleTile(pos, wallPrefab);
    }
}
