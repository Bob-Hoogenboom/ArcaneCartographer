using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrunkardsWalkGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected DrunkardsWalkData drunkardsWalkParameters;

    protected GameManager gameManager;

    [SerializeField]
    protected System.Random rng;

    protected override void RunProceduralGeneration()
    {
        gameManager = GameManager.Instance;

        HashSet<Vector2Int> floorPositions = RunDrunkardsWalk(gameManager, startPos, rng);
        visualizer.Clear();
        visualizer.PaintFloorTiles(floorPositions);

        WallGenerator.CreateWalls(floorPositions, visualizer);
    }

    protected HashSet<Vector2Int> RunDrunkardsWalk(GameManager manager, Vector2Int pos, System.Random rng)
    {
        Vector2Int currentPos = pos;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < manager._iterations; i++)
        {
            HashSet<Vector2Int> path = DrunkardsWalkAlgorithm.SimpleDrunkardsWalk(currentPos, manager._walkLength, rng); // ✅ Pass rng
            floorPositions.UnionWith(path);

            if (manager._randomStart)
            {
                currentPos = floorPositions.ElementAt(rng.Next(0, floorPositions.Count)); // ✅ Use seeded RNG
            }
        }

        return floorPositions;
    }
}
