using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrunkardsWalkParam_",menuName = "DungeonData/DrunkardsWalkData")]
public class DrunkardsWalkData : ScriptableObject
{
    public int iterations = 10;
    public int walkLength = 10;
    public bool startRandomEachIteration = true;
}
