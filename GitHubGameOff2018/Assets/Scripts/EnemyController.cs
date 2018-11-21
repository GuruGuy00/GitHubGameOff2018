using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Vector3 enemyLoc;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

}