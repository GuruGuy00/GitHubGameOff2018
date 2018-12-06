using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemyController : MonoBehaviour
{
    public float timerMax = 4f;
    public float turnChangeTimer = 0f;

    void Start()
    {
        turnChangeTimer = timerMax;
    }

    public abstract bool DoEnemyTurn();
    public abstract bool DoEnemyAction();
}
