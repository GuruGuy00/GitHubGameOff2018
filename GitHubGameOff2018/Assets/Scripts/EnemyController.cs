using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    //TODO: Remove this.  This is just to test turn changing basics
    [HideInInspector] public float timerMax = 4f;
    [HideInInspector] public float turnChangeTimer = 0f;

    void Start()
    {
        turnChangeTimer = timerMax;
    }

    public bool EnemyTurn()
    {
        turnChangeTimer -= Time.deltaTime;
        if (turnChangeTimer < 0)
        {
            turnChangeTimer = timerMax;
            return true;
        }
        return false;
    }

    public bool EnemyAction()
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