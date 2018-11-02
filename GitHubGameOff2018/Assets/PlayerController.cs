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

    // Use this for initialization
    void Start () {

        LocTest = groundTilemap.CellToWorld(new Vector3Int(5, 5, 0));
        transform.position = groundTilemap.CellToWorld(new Vector3Int(5, 5, 0));
        playerLoc = groundTilemap.CellToWorld(new Vector3Int(5, 5, 0));

    }
	
	// Update is called once per frame
	void Update () {

        Vector3Int newPos =  Vector3Int.CeilToInt(playerLoc);

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
            newPos = new Vector3Int((int)playerLoc.x - 1, (int)playerLoc.y, 0);
        }

        playerLoc = groundTilemap.CellToWorld(newPos);
        transform.position = playerLoc;

        Debug.Log(getCell(groundTilemap, playerLoc));

    }

    private TileBase getCell(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }
}
