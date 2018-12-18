using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {

    public string SaveSlot;

    public GameData() { }

    public GameData(string saveSlot)
    {
        SaveSlot = saveSlot;
    }


}
