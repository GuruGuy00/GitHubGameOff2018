using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileUtils : MonoBehaviour
{
    public static TileUtils Instance { get; private set; }

    public Tilemap groundTilemap;
    public Tilemap previewTilemap;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsTileSolid(Tilemap tilemap, Vector3Int worldPos)
    {
        Vector2 tileLoc = new Vector2(worldPos.x, worldPos.y);
        TileBase tb = tilemap.GetTile(GetCellPos(tilemap, tileLoc));
        if (tb != null && tb.name.ToUpper().Contains("_SOLID"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetTile(Tilemap tilemap, Vector3Int tileLoc, TileBase tb)
    {
        tilemap.SetTile(tileLoc, tb);
    }

    public Vector3Int GetCellPos(Tilemap tilemap, Vector3Int cellWorldPos)
    {
        return GetCellPos(tilemap, new Vector2(cellWorldPos.x, cellWorldPos.y));
    }

    public Vector3Int GetCellPos(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.WorldToCell(cellWorldPos);
    }

}
