using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class RandomGeneratorEditor : Editor
{
    AbstractDungeonGenerator generator;
    private void Awake()
    {
        generator = (AbstractDungeonGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Generate Dungeon"))
        {
            generator.GenerateDungeon();
        }

        if (GUILayout.Button("Delete Dungeon"))
        {
            generator.DeleteDungeon();
        }
    }
}
