using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour {

    //Used the below refrence to get things going
    //https://github.com/Cawotte/SmallWorld_WeeklyJam40

    public Tilemap groundTilemap;
    public Vector3Int playerWorldLoc;
    public Vector3Int playerTileLoc;
    private Vector3 startPos;

    public Vector3 LocTest;

    private Vector3Int worldLocToTileLoc = new Vector3Int(1, 6, 0);

    Vector3 currentVelocity;
    public float smoothTime = 0.25f;

    //ToDo: Get better movements tile by tile
    private List<string> moveList = new List<string>();
    private bool isMoving = false;
    private bool isProccessingMoves = false;
    
    void Start () {

        //ToDo : fix this up, need to read start pos from var
        startPos = transform.position;
        playerWorldLoc = Vector3Int.CeilToInt(startPos);
        playerTileLoc = playerWorldLoc + worldLocToTileLoc;

        //LocTest = groundTilemap.CellToWorld(Vector3Int.CeilToInt(startPos));
        //transform.position = groundTilemap.CellToWorld(Vector3Int.CeilToInt(startPos));
        //playerLoc = groundTilemap.CellToWorld(Vector3Int.CeilToInt(startPos));

        LocTest = startPos;
    }
	
	void Update () {
        bool grounded = IsPlayerGrounded();

        Vector3Int newPos = ProcessMoves(playerWorldLoc);

        //TODO: determine how exactly we want to handle gravity
        //newPos = ProcessGravity(newPos, !grounded);

        ApplyMoves(newPos);

        playerWorldLoc = newPos;
        playerTileLoc = playerWorldLoc + worldLocToTileLoc;
    }

    private Vector3Int ProcessMoves(Vector3Int currPos)
    {
        Vector3Int newPos = currPos;

        if (isProccessingMoves)
        {
            if (!isMoving && moveList.Count > 0)
            {
                switch (moveList[0].ToString())
                {
                    case "Right":
                        newPos = MoveRight(newPos);
                        break;
                    case "Left":
                        newPos = MoveLeft(newPos);
                        break;
                    case "DashRight":
                        newPos = DashRight(newPos);
                        break;
                    case "DashLeft":
                        newPos = DashLeft(newPos);
                        break;
                    case "Jump":
                        newPos = Jump(newPos);
                        break;
                    default:
                        Debug.Log("Move " + moveList[0].ToString() + " Not implamented yet!");
                        break;
                }

                Debug.Log("Processing Move : " + moveList[0]);
                moveList.RemoveAt(0);
            }
        }

        return newPos;
    }

    private Vector3Int ProcessGravity(Vector3Int newPos, bool isAirborne)
    {
        if (isAirborne)
        {
            newPos = new Vector3Int((int)newPos.x, (int)newPos.y - 1, 0);
        }
        return newPos;
    }

    private void ApplyMoves(Vector3Int newPos)
    {
        //ToDo : Play around to see which works the best?
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothTime);
        //transform.position = Vector3.Lerp(transform.position, playerWorldLoc, smoothTime);
        //transform.position = Vector3.MoveTowards(transform.position, playerWorldLoc, 0.0f);

        if (Mathf.Approximately(transform.position.x, (float)newPos.x)
            && Mathf.Approximately(transform.position.y, (float)newPos.y))
        {
            isMoving = false;
            if (moveList.Count == 0)
            {
                //ToDo : maybe add an update to the postion so that we are exactly where we wanted to be.
                isProccessingMoves = false;
                
            }
        }
    }

    private bool IsPlayerGrounded()
    {
        //Check the tile underneath our player's current tile location
        Vector3Int checkPos = new Vector3Int((int)playerTileLoc.x, (int)playerTileLoc.y - 1, 0);
        TileBase tb = groundTilemap.GetTile(checkPos);
        //TODO: This will need to be updated if/when we change the tiles
        if (tb != null && (tb.name == "Green" || tb.name == "Gray"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3Int MoveRight(Vector3Int newPos)
    {
        isMoving = true;
        newPos = new Vector3Int((int)newPos.x + 1, (int)newPos.y, 0);
        return newPos;
    }

    public Vector3Int MoveLeft(Vector3Int newPos)
    {
        isMoving = true;
        newPos = new Vector3Int((int)newPos.x - 1, (int)newPos.y, 0);
        return newPos;
    }

    public Vector3Int DashRight(Vector3Int newPos)
    {
        isMoving = true;
        newPos = new Vector3Int((int)newPos.x + 2, (int)newPos.y, 0);
        return newPos;
    }

    public Vector3Int DashLeft(Vector3Int newPos)
    {
        isMoving = true;
        newPos = new Vector3Int((int)newPos.x - 2, (int)newPos.y, 0);
        return newPos;
    }

    public Vector3Int Jump(Vector3Int newPos)
    {
        isMoving = true;
        newPos = new Vector3Int((int)newPos.x, (int)newPos.y + 3, 0);
        return newPos;
    }

    public Vector3Int MoveUp(Vector3Int newPos)
    {
        isMoving = true;
        newPos = new Vector3Int((int)newPos.x, (int)newPos.y + 1, 0);
        return newPos;
    }

    public void setMoveList(List<string> moves)
    {
        moveList = moves;
        isProccessingMoves = true;
    }

    private TileBase getCell(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }
}
