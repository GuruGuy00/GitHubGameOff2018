using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Objectives : MonoBehaviour {

    //Check to enable the objectives?
    //Each level should have 3 objectives? or does more or variable per level make more sense 
    public bool reachExit = false;
    public bool findHiddenExit = false;
    public bool clearAllEnemies = false;
    public bool clearInXMoves = false;
    // Add more varations/Types?

    protected TileUtils tileUtils;
    public Tilemap levelTilemap;

    public GameObject player;

    // Use this for initialization
    void Start () {
        tileUtils = TileUtils.Instance;

    }
	
	// Update is called once per frame
	void Update () {
        if (reachExit)
        {
            CheckForExit();
        }
	}

    private void CheckForExit()
    {
        //Player Finds exit
        if (tileUtils.IsTileExit(levelTilemap, Vector3Int.CeilToInt(player.transform.position)))
        {
            Debug.Log("Objective Compleate");
        }
        //  Update Level Data that level has been compleated
        //  Exit current level retrun to over World
    }


}
