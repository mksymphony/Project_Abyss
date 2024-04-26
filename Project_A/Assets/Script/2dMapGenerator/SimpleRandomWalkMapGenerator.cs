using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalkMapGenerator : AbstractDungeonGenerator
{
    [SerializeField] 
    private SimpleRandomWalkSo _randomWalkParameters;
    
    

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector3Int> floorPosition = RunRandomWalk();
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPosition);
        WallGenerator.CreateWalls(floorPosition,tilemapVisualizer);
    }

    private HashSet<Vector3Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector3Int> floorPosition = new HashSet<Vector3Int>();
        for (int i = 0; i < _randomWalkParameters.interations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, _randomWalkParameters.walkLength);
            floorPosition.UnionWith(path);

            if (_randomWalkParameters.startRandomlyEachIteration)
            {
                currentPosition = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
            }
        }
        return floorPosition;
    }
}

