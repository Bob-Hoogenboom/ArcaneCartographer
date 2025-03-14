using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public static class DrunkardsWalkAlgorithm
{
    public static HashSet<Vector2Int> SimpleDrunkardsWalk(Vector2Int startpos, int walkLength, System.Random seed)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        
        path.Add(startpos);
        Vector2Int previousPos = startpos;

        for (int i = 0; i < walkLength; i++)
        {
            Vector2Int newPos = previousPos + Direction2D.GetRandomCardinalDir(seed);
            path.Add(newPos);
            previousPos = newPos;
        }
        return path;
    }   
    
    public static List<Vector2Int> DrunkardsWalkCorridor(Vector2Int startPos, int corridorLength, System.Random seed)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        Vector2Int direction = Direction2D.GetRandomCardinalDir(seed);
        Vector2Int currentPos = startPos;

        corridor.Add(currentPos);
        for (int i = 0; i < corridorLength;i++) 
        {
            currentPos += direction;
            corridor.Add(currentPos);
        }
        return corridor;
    } 
}


public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //up
        new Vector2Int(1,0), //right
        new Vector2Int(0,-1), //down
        new Vector2Int(-1,0) //left
    };

    public static Vector2Int GetRandomCardinalDir(System.Random seed)
    {
        return cardinalDirectionList[seed.Next(0, cardinalDirectionList.Count)];
    }
}
