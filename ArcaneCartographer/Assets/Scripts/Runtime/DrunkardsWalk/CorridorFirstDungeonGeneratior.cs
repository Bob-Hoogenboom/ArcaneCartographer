using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGeneratior : DrunkardsWalkGenerator
{
    [SerializeField]
    private int corridorLength = 14;
    [SerializeField]
    private int corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float roomPercent = 0.8f;

    [SerializeField]
    [Range(1,3)]
    private int corridorWidth = 1;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnds(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        for (int i = 0; i < corridorCount; i++) 
        {
            if(corridorWidth == 1) corridors[i] = corridors[i];
            else if(corridorWidth == 2) corridors[i] = IncreaseCorridorSizeByOne(corridors[i]);
            else if (corridorWidth == 3) corridors[i] = CorridorBrush3X3(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
            
        }

        //Instantiate the corridor objects
        visualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, visualizer);
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPos = startPos;
        potentialRoomPositions.Add(currentPos);

        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();    

        for (int i = 0; i < corridorCount; i++) 
        {
            var corridor = DrunkardsWalkAlgorithm.DrunkardsWalkCorridor(currentPos, corridorLength);
            corridors.Add(corridor);
            currentPos = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPos);
            floorPositions.UnionWith(corridor);
        }
        return corridors;
    }

    private List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previewDirection = Vector2Int.zero;

        for (int i = 1; i < corridor.Count; i++)
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i - 1];
            if (previewDirection != Vector2Int.zero && directionFromCell != previewDirection)
            {
                for (int x = -1; x < 2; x++)
                {
                    for(int y = -1; y < 2; y++) 
                    {
                        newCorridor.Add(corridor[i -1] + new Vector2Int(x, y));
                    }
                }
                previewDirection = directionFromCell;
            }
            else
            {
                //Add a single cell in teh direction +90 degrees (right of the cell)
                Vector2Int newCorridorTileOffset = GetRotatedDirectionFrom(directionFromCell);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
            }
        }
        return newCorridor;
    }

    public List<Vector2Int> CorridorBrush3X3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();

        for(int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y)); 
                }
            }
        }
        return newCorridor;
    }

    private Vector2Int GetRotatedDirectionFrom(Vector2Int dir)
    {
        if (dir == Vector2Int.up)
            return Vector2Int.right;
        if (dir == Vector2Int.right)
            return Vector2Int.down;
        if (dir == Vector2Int.down)
            return Vector2Int.left;
        if (dir == Vector2Int.left)
            return Vector2Int.up;

        return Vector2Int.zero;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        //Randomly sort the potentialRooms
        //Create a 'Global Unique IDentifier'(GUID) for each potential room
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomCount).ToList();

        foreach (var roomPos in roomsToCreate)
        {
            var roomFloor = RunDrunkardsWalk(drunkardsWalkParameters, roomPos);
            roomPositions.UnionWith(roomFloor); //Avoid repetitions in collection
        }
        return roomPositions;
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var pos in floorPositions)
        {
            int neightboursCount = 0;
            foreach (var dir in Direction2D.cardinalDirectionList)
            {
                if (floorPositions.Contains(pos + dir))
                {
                    neightboursCount++;
                }
            }

            if (neightboursCount == 1)
            {
                deadEnds.Add(pos);
            }
        }
        return deadEnds;
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var pos in deadEnds)
        {
            if(roomFloors.Contains(pos) == false)
            {
                var room = RunDrunkardsWalk(drunkardsWalkParameters, pos);
                roomFloors.UnionWith(room);
            }
        }
    }
}
