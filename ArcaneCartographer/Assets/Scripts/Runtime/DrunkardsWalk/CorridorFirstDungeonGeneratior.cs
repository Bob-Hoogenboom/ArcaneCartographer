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

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnds(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        //Instantiate the corridor objects
        visualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, visualizer);
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPos = startPos;
        potentialRoomPositions.Add(currentPos);

        for (int i = 0; i < corridorCount; i++) 
        {
            var path = DrunkardsWalkAlgorithm.DrunkardsWalkCorridor(currentPos, corridorLength);
            currentPos = path[path.Count - 1];
            potentialRoomPositions.Add(currentPos);
            floorPositions.UnionWith(path);
        }
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
