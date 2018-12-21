using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemyController : ICharacterController
{
    //TODO: Remove these once we have actual enemy turns
    protected float timerMax = 4f;
    protected float turnChangeTimer = 0f;
    
    protected Vector3Int startPosition;

    public int moveSpeed;
    public int jumpSpeed;

    void Start()
    {
        turnChangeTimer = timerMax;
        startPosition = worldLoc;
    }

    public abstract bool DoEnemyTurn(GameObject player);
    public abstract bool DoEnemyAction(GameObject player);
}
