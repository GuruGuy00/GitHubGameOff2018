using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public bool HandleEnemies(_GameManager.GameState gameState)
    {
        bool allEnemiesFinished = false;

        List<IEnemyController> enemiesToProcess = LoadNearbyEnemies();
        
        if (gameState == _GameManager.GameState.EnemyTurn)
        {
            allEnemiesFinished = DoAllEnemyTurns(enemiesToProcess);
        }
        else if (gameState == _GameManager.GameState.EnemyAction)
        {
            allEnemiesFinished = DoAllEnemyActions(enemiesToProcess);
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

    private bool DoAllEnemyTurns(List<IEnemyController> enemies)
    {
        bool allTurnsFinished = true;
        for (int i = 0; i < enemies.Count; i++)
        {
            IEnemyController iEnemy = enemies[i];
            bool result = iEnemy.DoEnemyTurn();
            if (!result)
            {
                allTurnsFinished = false;
            }
        }
        return allTurnsFinished;
    }

    private bool DoAllEnemyActions(List<IEnemyController> enemies)
    {
        bool allActionsFinished = true;
        for (int i = 0; i < enemies.Count; i++)
        {
            IEnemyController iEnemy = enemies[i];
            bool result = iEnemy.DoEnemyAction();
            if (!result)
            {
                allActionsFinished = false;
            }
        }
        return allActionsFinished;
    }

}
