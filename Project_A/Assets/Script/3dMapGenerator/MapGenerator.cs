using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width;
    public int height;

    public int smoothValue;

    [Range(0, 100)] [SerializeField] private int _randomFillPercent;
    private int[,] _map;

    [SerializeField] private string _seed;
    [SerializeField] private bool _useRandomSeed;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateMap();
        }
    }

    private void GenerateMap()
    {
        _map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < smoothValue; i++)
        {
            SmoothMap();
        }

        MeshGenerator meshGen = GetComponent<MeshGenerator>();
        meshGen.GenerateMesh(_map,1);
    }

    private void RandomFillMap()
    {
        if (_useRandomSeed)
        {
            _seed = Time.time.ToString();
        }

        System.Random pseudoRandom = new System.Random(_seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    _map[x, y] = 1;
                }
                else
                {
                    _map[x, y] = (pseudoRandom.Next(0, 100) < _randomFillPercent) ? 1 : 0;
                }
            }
        }
    }

    private void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                {
                    _map[x, y] = 1;
                }
                else if (neighbourWallTiles <= 4)
                    _map[x, y] = 0;
            }
        }
    }

    private int GetSurroundingWallCount(int gridX, int gridY)
    {
        var wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += _map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }

    private void OnDrawGizmos()
    {
        /*if (_map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.color = (_map[x, y] == 1) ? Color.black : Color.white;
                    var pos = new Vector3(-width / 2 + x + 0.5f, 0, -height / 2 + y + 0.5f);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }*/
    }
}