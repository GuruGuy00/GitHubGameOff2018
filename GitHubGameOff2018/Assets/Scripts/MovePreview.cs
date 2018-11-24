using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovePreview : MonoBehaviour
{
    public GameObject player;
    //Tile Base - GoTile
    public TileBase goTileBase;

    private List<Vector3Int> forcePreviewPoints;
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

    void Update () {
        ClearTiles();

        List<Vector3Int> points;
        if (playerController.IsProcessingMoves())
        {
            points = forcePreviewPoints;
        }
        else
        {
            points = moveProcessor.processedMoves;
        }

        foreach (Vector3Int movePoint in points)
        {
            Vector3Int tilePos = tileUtils.GetCellPos(tileUtils.previewTilemap, movePoint);
            tileUtils.SetTile(tileUtils.previewTilemap, tilePos, goTileBase);
        }

        //Debug.Log("Move Cord Count " + points.Count);
    }

    private void ClearTiles()
    {
        tileUtils.previewTilemap.ClearAllTiles();
    }

    public void setPreviewPoints(List<Vector3Int> points)
    {
        forcePreviewPoints = points;
    }
}