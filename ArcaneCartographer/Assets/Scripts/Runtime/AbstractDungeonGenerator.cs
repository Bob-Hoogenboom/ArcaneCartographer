using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected DungeonVisualizer visualizer = null;
    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;

    public void GenerateDungeon()
    {
        visualizer.Clear();
        RunProceduralGeneration();
    }

    public void DeleteDungeon()
    {
        visualizer.Clear();
    }

    protected abstract void RunProceduralGeneration();
}
