using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyController : IEnemyController
{
    public GameObject projectile;

    private Vector3Int aimDirection = new Vector3Int(-1, 0, 0);

    private int fireRate = 2;
    private int fireCounter = 0;
    private bool shootThisTurn = false;

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
        //Figure out if we want to shoot this round
        fireCounter++;
        if (fireCounter >= fireRate)
        {
            fireCounter = 0;
            SpawnProjectile();
        }
        return true;
    }

    public override bool DoEnemyAction()
    {
        if (shootThisTurn)
        {
            SpawnProjectile();
        }
        return true;
    }

    private void SpawnProjectile()
    {
        //Create and shoot a new projectile
        GameObject newProjectile = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
        ProjectileEnemyController pScript = newProjectile.GetComponent<ProjectileEnemyController>();
        //Set some variables - set it to hidden until the Action phase
        pScript.Init(aimDirection);
        //Tell the EnemyManager we spawned a new enemy
        enemyManager.HandleEnemySpawn(pScript);
    }
}
