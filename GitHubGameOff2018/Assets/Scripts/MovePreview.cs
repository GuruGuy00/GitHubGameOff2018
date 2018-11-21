using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovePreview : MonoBehaviour {

    //PlayerController
    public GameObject player;
    PlayerController playerController;

    //Level/Ground TileMap
    public Tilemap groundTilemap;
    //Preview tilemap
    public Tilemap previewTilemap;

    //Tile Base - GoTile
    public TileBase goTileBase;

    //Tile Base - Block Tile
    public TileBase blockedTileBase;

    public Transform playCardsParents;

    private Vector3Int playerCurentTile;
    public Vector3Int lastPos;

    public List<MoveCords> moveCords = new List<MoveCords>();

    // Use this for initialization
    void Start () {
        
    }
    private void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
        lastPos = playerController.playerTileLoc;
    }
    // Update is called once per frame
    void Update () {


        //groundTilemap.SetTile(new Vector3Int(playerCurentTile.x + 1, playerCurentTile.y, playerCurentTile.z), goTileBase);
        

        CardDisplay[] cardsToPlay = playCardsParents.GetComponentsInChildren<CardDisplay>();
        moveCords.Clear();

        foreach (CardDisplay cardInfo in cardsToPlay)
        {
            if (moveCords.Count == 0)
            {
                MoveCords mc = new MoveCords();
                mc.startPos = playerController.playerTileLoc;
                mc.endPos = cardInfo.card.moveTo + playerController.playerTileLoc;
                moveCords.Add(mc);
            }
            else
            {
                MoveCords mc = new MoveCords();
                mc.startPos = moveCords[moveCords.Count-1].endPos;
                mc.endPos = cardInfo.card.moveTo + moveCords[moveCords.Count - 1].endPos;
                moveCords.Add(mc);
            }

            //Vector3Int tilePos = cardInfo.card.moveTo + lastPos;
            //groundTilemap.SetTile(tilePos, goTileBase);
            //lastPos = tilePos;
        }

        foreach (MoveCords moveCord in moveCords)
        {
            groundTilemap.SetTile(moveCord.endPos, goTileBase);
        }

        Debug.Log("Move Cord Count " + moveCords.Count);

    }
}

public class MoveCords
{
    public Vector3Int startPos;
    public  Vector3Int endPos;

    public MoveCords() { }
}

