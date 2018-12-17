using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    [SerializeField]
    public float WorldMapPosX;
    public float WorldMapPosY;
    public float WorldMapPosZ;

    public PlayerData() { }

    public PlayerData(float worldMapPosX, float worldMapPosY, float worldMapPosZ)
    {
        WorldMapPosX = worldMapPosX;
        WorldMapPosY = worldMapPosY;
        WorldMapPosZ = worldMapPosZ;
    }


}
