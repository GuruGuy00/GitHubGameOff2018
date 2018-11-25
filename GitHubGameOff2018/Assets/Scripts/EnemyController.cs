using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    private _GameManager gm;

    //TODO: Remove this.  This is just to test turn changing basics
    [HideInInspector] public float timerMax = 4f;
    [HideInInspector] public float turnChangeTimer = 0f;

    void Start()
    {
        gm = FindObjectOfType<_GameManager>();
        turnChangeTimer = timerMax;
    }

    private void Update()
    {
        //TODO: Remove this.  This is just to test turn changing basics
        if (gm.currentGameState == _GameManager.GameState.EnemyTurn)
        {
            turnChangeTimer -= Time.deltaTime;
            if (turnChangeTimer < 0)
            {
                turnChangeTimer = timerMax;
                gm.EnemySubmitMoves();
            }
        }
        if (gm.currentGameState == _GameManager.GameState.EnemyAction)
        {
            turnChangeTimer -= Time.deltaTime;
            if (turnChangeTimer < 0)
            {
                turnChangeTimer = timerMax;
                gm.EnemyMovesComplete();
            }
        }
    }

}