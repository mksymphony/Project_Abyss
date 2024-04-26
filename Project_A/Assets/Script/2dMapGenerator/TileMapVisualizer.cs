using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap _flootTilemap;
    [SerializeField] 
    private Tilemap _wallTilemap;
    
    [SerializeField]
    private TileBase _floorTile;
    [SerializeField] 
    private TileBase _wallTop;

    public void PaintFloorTiles(IEnumerable<Vector3Int> FloorPosition)
    {
        PaintTiles(FloorPosition, _flootTilemap,_floorTile);
    }
    public void Clear()
    {
        _flootTilemap.ClearAllTiles();
        _wallTilemap.ClearAllTiles();
    }
    private void PaintTiles(IEnumerable<Vector3Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap,tile,position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector3Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition,tile);
    }

    internal void PaintSingleBsicWall(Vector3Int position)
    {
        PaintSingleTile(_wallTilemap,_wallTop,position);
    }
}
