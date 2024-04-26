using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector3Int> SimpleRandomWalk(Vector3Int startPosition, int walkLength)
    {
        HashSet<Vector3Int> path = new HashSet<Vector3Int>();
        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }
}

public static class Direction2D
{
    public static List<Vector3Int> cardinalDirectionList = new List<Vector3Int>
    {
        new Vector3Int(0, 1), // UP
        new Vector3Int(1, 0), // Right
        new Vector3Int(0, -1), // Down
        new Vector3Int(-1, 0) // Left
    };
    public static Vector3Int GetRandomCardinalDirection()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}