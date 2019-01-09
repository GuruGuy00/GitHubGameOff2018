using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Camera cam;
    private List<IEnemyController> enemyCache = null;
    private List<IEnemyController> spawnedEnemies = null;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("LevelCamera").GetComponent<Camera>();
    }

    public bool HandleEnemies(_GameManager.GameState gameState, GameObject player)
    {
        bool allEnemiesFinished = false;

        if (enemyCache == null)
        {
            enemyCache = LoadEnemiesToProcess();
        }
        
        if (gameState == _GameManager.GameState.EnemyTurn)
        {
            allEnemiesFinished = DoAllEnemyTurns(player);
        }
        else if (gameState == _GameManager.GameState.EnemyAction)
        {
            allEnemiesFinished = DoAllEnemyActions(player);
        }

        if (allEnemiesFinished)
        {
            enemyCache = null;
        }

        return allEnemiesFinished;
    }

    //Load a list of enemies whose turns/actions we want to process
    public List<IEnemyController> LoadEnemiesToProcess()
    {
        IEnemyController[] enemyControllers = FindObjectsOfType<IEnemyController>();
        List<IEnemyController> nearbyEnemies = FindEnemiesInView(enemyControllers);
        return nearbyEnemies;
    }

    //Process a list of enemies and remove ones not in the camera view
    private List<IEnemyController> FindEnemiesInView(IEnemyController[] allEnemyControllers)
    {
        List<IEnemyController> enemiesInView = new List<IEnemyController>();
        foreach (IEnemyController enemyCon in allEnemyControllers)
        {
            if (IsLocInCameraView(enemyCon.transform.position))
            {
                enemiesInView.Add(enemyCon);
            }
        }
        return enemiesInView;
    }

    //Process all enemy turns in the enemy cache.
    //Remove enemies from the cache that finished.
    private bool DoAllEnemyTurns(GameObject player)
    {
        List<IEnemyController> enemiesToRemove = new List<IEnemyController>();
        foreach (IEnemyController enemy in enemyCache)
        {
            if (enemy.DoEnemyTurn(player))
            {
                enemiesToRemove.Add(enemy);
            }
        }
        if (spawnedEnemies != null && spawnedEnemies.Count > 0)
        {
            enemyCache.AddRange(spawnedEnemies);
            foreach (IEnemyController enemy in spawnedEnemies)
            {
                if (enemy.DoEnemyTurn(player))
                {
                    enemiesToRemove.Add(enemy);
                }
            }
            spawnedEnemies = null;
        }
        enemyCache.RemoveAll(x => enemiesToRemove.Contains(x));
        return enemyCache.Count == 0;
    }

    //Process all enemy actions in the enemy cache.
    //Remove enemies from the cache that finished.
    private bool DoAllEnemyActions(GameObject player)
    {
        List<IEnemyController> enemiesToRemove = new List<IEnemyController>();
        foreach (IEnemyController enemy in enemyCache)
        {
            bool result = enemy.DoEnemyAction(player);
            if (result)
            {
                enemiesToRemove.Add(enemy);
            }
        }
        enemyCache.RemoveAll(x => enemiesToRemove.Contains(x));
        return enemyCache.Count == 0;
    }

    //Used to handle when an enemy spawns another enemy
    public void HandleEnemySpawn(IEnemyController spawnedEnemy)
    {
        if (spawnedEnemies == null)
        {
            spawnedEnemies = new List<IEnemyController>();
        }
        spawnedEnemies.Add(spawnedEnemy);
    }

    //Check if a given location is within view of the camera
    public bool IsLocInCameraView(Vector3 enemyLoc)
    {
        //Get a list of planes from the camera's frustum
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        //Create bounds to check within our camera view
        Vector3 testLoc = new Vector3(enemyLoc.x + 0.5f, enemyLoc.y + 0.5f, enemyLoc.z);
        Bounds b = new Bounds(testLoc, new Vector3Int(1, 1, 1));
        //Check if the bounds of the collider are within the camera's frustrum
        return GeometryUtility.TestPlanesAABB(planes, b);
    }
}
