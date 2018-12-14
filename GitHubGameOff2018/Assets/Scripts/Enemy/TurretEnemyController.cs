using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyController : IEnemyController
{
    private EnemyManager enemyManager;

    void Start()
    {
        enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        tileUtils = TileUtils.Instance;
        worldLoc = Vector3Int.CeilToInt(transform.position);
        tileLoc = tileUtils.GetCellPos(tileUtils.groundTilemap, transform.position);
    }

    public override bool DoEnemyTurn()
    {
        return true;
    }

    public override bool DoEnemyAction()
    {
        return true;
    }
}
