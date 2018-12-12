using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData {

    public string LevelName;
    //
    public bool ReachedExit;
    public bool ClearedAllEnemies;
    public bool ClearedAllPickUps;


    public LevelData(string levelName, bool reachedExit, bool clearedAllEnemies, bool clearedAllPickUps)
    {
        this.LevelName = levelName;
        
        this.ReachedExit = reachedExit;
        this.ClearedAllEnemies = clearedAllEnemies;
        this.ClearedAllPickUps = clearedAllPickUps;
    }

    

}
