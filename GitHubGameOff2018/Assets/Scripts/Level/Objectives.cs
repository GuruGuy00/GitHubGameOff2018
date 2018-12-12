using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Objectives : MonoBehaviour {

    private LevelData ld;

    protected TileUtils tileUtils;
    public Tilemap levelTilemap;

    public GameObject player;

    // Use this for initialization
    void Start () {

        tileUtils = TileUtils.Instance;
        ld = SaveSystem.LoadData(SceneManager.GetActiveScene().name);
        
    }
	
	// Update is called once per frame
	void Update () {

        CheckForExit();
	}

    private void CheckForExit()
    {
        //Player Finds exit
        if (tileUtils.IsTileExit(levelTilemap, Vector3Int.CeilToInt(player.transform.position)))
        {
            Debug.Log("Objective Compleate");
            ld.ReachedExit = true;

            //if all enemies clears
            //  ld.ClearedAllEnemies = true;

            //if all enemies clears
            //  ld.ClearedAllEnemies = true;

            //Save level data
            SaveSystem.SaveLevel(ld);
            //return to the wordl map
            SceneManager.LoadScene("WorldMap");
        }

    }


}
