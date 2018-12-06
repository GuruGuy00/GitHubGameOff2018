using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : IEnemyController
{
    public override bool DoEnemyTurn()
    {
        turnChangeTimer -= Time.deltaTime;
        if (turnChangeTimer < 0)
        {
            turnChangeTimer = timerMax;
            return true;
        }
        return false;
    }

    public override bool DoEnemyAction()
    {
        turnChangeTimer -= Time.deltaTime;
        if (turnChangeTimer < 0)
        {
            turnChangeTimer = timerMax;
            return true;
        }
        return false;
    }
}