using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrunkardsWalkGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected DrunkardsWalkData drunkardsWalkParameters;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunDrunkardsWalk(drunkardsWalkParameters, startPos);
        visualizer.Clear();
        visualizer.PaintFloorTiles(floorPositions);

        WallGenerator.CreateWalls(floorPositions, visualizer);
    }

    protected HashSet<Vector2Int> RunDrunkardsWalk(DrunkardsWalkData parameters, Vector2Int pos)
    {
        Vector2Int currentPos = pos;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            HashSet<Vector2Int> path = DrunkardsWalkAlgorithm.SimpleDrunkardsWalk(currentPos, parameters.walkLength);
            floorPositions.UnionWith(path);
            if (parameters.startRandomEachIteration)
            {
                currentPos = floorPositions.ElementAt(Random.Range(0,floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
