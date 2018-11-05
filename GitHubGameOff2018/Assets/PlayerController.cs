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

    public Vector3Int newPos;

    private float moveTime = 1f;

    Vector3 currentVelocity;
    public float smoothTime = 0.25f;

    //ToDo: Get better movements tile by tile

    // Use this for initialization
    void Start () {

        //ToDo : fix this up, need to read start pos from var
        LocTest = groundTilemap.CellToWorld(new Vector3Int(-8, -4, 0));
        transform.position = groundTilemap.CellToWorld(new Vector3Int(-8, -4, 0));
        playerLoc = groundTilemap.CellToWorld(new Vector3Int(-8, -4, 0));

        newPos = Vector3Int.CeilToInt(playerLoc);
        
    }
	
	// Update is called once per frame
	void Update () {

        //Vector3Int newPos =  Vector3Int.CeilToInt(playerLoc);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            newPos = new Vector3Int((int)playerLoc.x, (int)playerLoc.y + 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            newPos = new Vector3Int((int)playerLoc.x + 1, (int)playerLoc.y, 0);
            
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            newPos = new Vector3Int((int)playerLoc.x, (int)playerLoc.y - 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            newPos = new Vector3Int((int)playerLoc.x - 1, (int)playerLoc.y, 0); ;
        }

        playerLoc = groundTilemap.CellToWorld(newPos);
        //transform.position = playerLoc;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothTime);

        Debug.Log(getCell(groundTilemap, playerLoc));

    }

    public void MoveRight()
    {
        //newPos = Vector3Int.CeilToInt(playerLoc);
        newPos = new Vector3Int((int)playerLoc.x + 1, (int)playerLoc.y, 0);
        playerLoc = groundTilemap.CellToWorld(newPos);
        //transform.position = playerLoc;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothTime);
    }

    public void MoveLeft()
    {
        //Vector3Int newPos = Vector3Int.CeilToInt(playerLoc);
        newPos = new Vector3Int((int)playerLoc.x - 1, (int)playerLoc.y, 0);
        playerLoc = groundTilemap.CellToWorld(newPos);
        //transform.position = playerLoc;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothTime);
    }

    public void MoveUp()
    {
        //Vector3Int newPos = Vector3Int.CeilToInt(playerLoc);
        newPos = new Vector3Int((int)playerLoc.x, (int)playerLoc.y + 1, 0);
        playerLoc = groundTilemap.CellToWorld(newPos);
        //transform.position = playerLoc;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothTime);

    }

    private TileBase getCell(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }

    
}
