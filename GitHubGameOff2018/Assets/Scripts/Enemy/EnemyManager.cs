using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<IEnemyController> enemyCache = null;

    public bool HandleEnemies(_GameManager.GameState gameState)
    {
        bool allEnemiesFinished = false;

        if (enemyCache == null)
        {
            enemyCache = LoadNearbyEnemies();
        }
        
        if (gameState == _GameManager.GameState.EnemyTurn)
        {
            allEnemiesFinished = DoAllEnemyTurns();
        }
        else if (gameState == _GameManager.GameState.EnemyAction)
        {
            allEnemiesFinished = DoAllEnemyActions();
        }

        if (allEnemiesFinished)
        {
            enemyCache = null;
        }

        return allEnemiesFinished;
    }

    //TODO: This should probably only find enemies within a certain range of the players
    private List<IEnemyController> LoadNearbyEnemies()
    {
        IEnemyController[] enemyControllers = FindObjectsOfType<IEnemyController>();
        List<IEnemyController> nearbyEnemies = new List<IEnemyController>(enemyControllers);
        return nearbyEnemies;
    }

    private bool DoAllEnemyTurns()
    {
        List<IEnemyController> enemiesToRemove = new List<IEnemyController>();
        foreach (IEnemyController enemy in enemyCache)
        {
            bool result = enemy.DoEnemyTurn();
            if (result)
            {
                enemiesToRemove.Add(enemy);
            }
        }
        enemyCache.RemoveAll(x => enemiesToRemove.Contains(x));
        return enemyCache.Count == 0;
    }

    private bool DoAllEnemyActions()
    {
        List<IEnemyController> enemiesToRemove = new List<IEnemyController>();
        foreach (IEnemyController enemy in enemyCache)
        {
            bool result = enemy.DoEnemyAction();
            if (result)
            {
                enemiesToRemove.Add(enemy);
            }
        }
        enemyCache.RemoveAll(x => enemiesToRemove.Contains(x));
        return enemyCache.Count == 0;
    }

}
