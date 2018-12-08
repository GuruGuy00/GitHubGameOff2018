using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemyController : MonoBehaviour
{
    //TODO: Remove these once we have actual enemy turns
    [HideInInspector] public float timerMax = 4f;
    [HideInInspector] public float turnChangeTimer = 0f;

    [HideInInspector] public Vector2Int position;
    [HideInInspector] public Vector2Int startPosition;

    public int moveSpeed;
    public int jumpSpeed;

    void Start()
    {
        turnChangeTimer = timerMax;
        position = Vector2Int.CeilToInt(transform.position);
        startPosition = position;
    }

    public abstract bool DoEnemyTurn();
    public abstract bool DoEnemyAction();
}
