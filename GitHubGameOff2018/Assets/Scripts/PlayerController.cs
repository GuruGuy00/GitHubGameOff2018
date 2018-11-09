using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour {

    //Used the below refrence to get things going
    //https://github.com/Cawotte/SmallWorld_WeeklyJam40

    public Tilemap groundTilemap;
    public Vector3 playerLoc;
    public Vector3 LocTest;
    private Vector3 startPos;

    public Vector3Int newPos;

    private float moveTime = 1f;

    Vector3 currentVelocity;
    public float smoothTime = 0.25f;

    //ToDo: Get better movements tile by tile
    private List<string> moveList = new List<string>();
    private bool isMoving = false;
    private bool isProccessingMoves = false;

    // Use this for initialization
    void Start () {

        //ToDo : fix this up, need to read start pos from var
        startPos = transform.position;

        //LocTest = groundTilemap.CellToWorld(Vector3Int.CeilToInt(startPos));
        //transform.position = groundTilemap.CellToWorld(Vector3Int.CeilToInt(startPos));
        //playerLoc = groundTilemap.CellToWorld(Vector3Int.CeilToInt(startPos));

        LocTest = startPos;
        transform.position = startPos;
        playerLoc = startPos;

        newPos = Vector3Int.CeilToInt(playerLoc);
        
    }
	
	// Update is called once per frame
	void Update () {

        if (isProccessingMoves)
        {
            if (!isMoving)
            {
                switch (moveList[0].ToString())
                {
                    case "Right":
                        MoveRight();
                        break;
                    case "Left":
                        MoveLeft();
                        break;
                    case "Jump":
                        Debug.Log("JUMP DOES NOT WORK YET!!!");
                        break;
                    default:
                        Debug.Log("Move " + moveList[0].ToString() + " Not implamented yet!");
                        break;
                }

                Debug.Log("Processing Move : " + moveList[0]);
                moveList.RemoveAt(0);

            }

            //ToDo : Play around to see which works the best?
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothTime);
            //transform.position = Vector3.Lerp(transform.position, newPos, smoothTime);
            //transform.position = Vector3.MoveTowards(transform.position, newPos, 0.0f);


            if ( Mathf.Approximately(transform.position.x ,(float)newPos.x) && Mathf.Approximately(transform.position.y, (float)newPos.y) )
            {
                isMoving = false;
                if (moveList.Count == 0)
                {
                    //ToDo : maybe add an update to the postion so that we are exactly where we wanted to be.
                    isProccessingMoves = false;
                }
            }
        }

    }

    public void MoveRight()
    {
        isMoving = true;
        newPos = new Vector3Int((int)playerLoc.x + 1, (int)playerLoc.y, 0);
        playerLoc = newPos;
        //playerLoc = groundTilemap.CellToWorld(newPos);
        
    }

    public void MoveLeft()
    {
        isMoving = true;
        
        newPos = new Vector3Int((int)playerLoc.x - 1, (int)playerLoc.y, 0);
        playerLoc = newPos;
        //playerLoc = groundTilemap.CellToWorld(newPos);

    }

    public void MoveUp()
    {
        isMoving = true;

        newPos = new Vector3Int((int)playerLoc.x, (int)playerLoc.y + 1, 0);
        playerLoc = newPos;
        //playerLoc = groundTilemap.CellToWorld(newPos);

    }

    private TileBase getCell(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }

    public void setMoveList(List<string> moves)
    {
        moveList = moves;
        isProccessingMoves = true;
    }
    
}
