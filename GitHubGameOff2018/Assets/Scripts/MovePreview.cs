using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovePreview : MonoBehaviour
{
    public GameObject player;

    //Tile Base - GoTile
    public TileBase goTileBase;
    public TileBase stopTileBase;

    private List<MoveInfo> forcePreviewPoints;
    private TileUtils tileUtils;
    private MoveProcessor moveProcessor;
    private PlayerController playerController;

    private void Awake()
    {
        moveProcessor = GetComponent<MoveProcessor>();
        playerController = player.GetComponent<PlayerController>();
    }

    void Start()
    {
        tileUtils = TileUtils.Instance;
    }

    public void DoPreview()
    {
        ClearTiles();

        List<MoveInfo> points;
        if (playerController.IsProcessingMoves())
        {
            points = forcePreviewPoints;
        }
        else
        {
            points = moveProcessor.processedMoves;
        }

        foreach (MoveInfo move in points)
        {
            Vector3Int tilePos = tileUtils.GetCellPos(tileUtils.previewTilemap, move.movePos);
            TileBase previewTile = goTileBase;
            if (move.isCollision)
            {
                previewTile = stopTileBase;
            }
            tileUtils.SetTile(tileUtils.previewTilemap, tilePos, previewTile);
        }
    }

    private void ClearTiles()
    {
        tileUtils.previewTilemap.ClearAllTiles();
    }

    public void setPreviewPoints(List<MoveInfo> points)
    {
        forcePreviewPoints = points;
    }
}