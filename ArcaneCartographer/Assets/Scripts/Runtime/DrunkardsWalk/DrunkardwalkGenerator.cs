using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrunkardsWalkGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected DrunkardsWalkData drunkardsWalkParameters;

    [SerializeField]
    protected System.Random rng;

    protected override void RunProceduralGeneration()
    {


        HashSet<Vector2Int> floorPositions = RunDrunkardsWalk(drunkardsWalkParameters, startPos, rng);
        visualizer.Clear();
        visualizer.PaintFloorTiles(floorPositions);

        WallGenerator.CreateWalls(floorPositions, visualizer);
    }

    protected HashSet<Vector2Int> RunDrunkardsWalk(DrunkardsWalkData parameters, Vector2Int pos, System.Random rng)
    {
        Vector2Int currentPos = pos;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < parameters.iterations; i++)
        {
            HashSet<Vector2Int> path = DrunkardsWalkAlgorithm.SimpleDrunkardsWalk(currentPos, parameters.walkLength, rng); // ✅ Pass rng
            floorPositions.UnionWith(path);

            if (parameters.startRandomEachIteration)
            {
                currentPos = floorPositions.ElementAt(rng.Next(0, floorPositions.Count)); // ✅ Use seeded RNG
            }
        }

        return floorPositions;
    }
}
