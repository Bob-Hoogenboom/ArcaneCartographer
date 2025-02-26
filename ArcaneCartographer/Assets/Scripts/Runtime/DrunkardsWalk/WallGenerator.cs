using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, DungeonVisualizer visualizer)
    {
        var wallPos = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionList);
        foreach (var pos in wallPos) 
        {
            visualizer.PaintSingleWall(pos);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var pos in floorPositions)
        {
            foreach (var direction in directionsList)
            {
                var neightbourPosition = pos + direction;

                //Check if the neighbourTile is not part of thye floor tiles
                if (floorPositions.Contains(neightbourPosition) == false) 
                {
                    //Add wallposition
                    wallPositions.Add(neightbourPosition); 
                }
            }
        }
        return wallPositions;
    }
}
