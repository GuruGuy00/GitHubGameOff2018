using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData {

    public string LevelName;
    public int TotalObjective;
    public int CompleatedObjectives;

    public LevelData(string levelName, int totalObjective, int compleatedObjectives)
    {
        this.LevelName = levelName;
        this.TotalObjective = totalObjective;
        this.CompleatedObjectives = compleatedObjectives;
    }

}
