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
            shootThisTurn = true;
        }
        return true;
    }

    public override bool DoEnemyAction()
    {
        //Actually perform the shoot move
        if (shootThisTurn)
        {
            Vector3 position = new Vector3(transform.position.x - 1, transform.position.y, 0f);
            GameObject newProjectile = GameObject.Instantiate(projectile, position, Quaternion.identity);
            ProjectileEnemyController pScript = newProjectile.GetComponent<ProjectileEnemyController>();
            pScript.moveDirection = aimDirection;
        }
        return true;
    }
}
