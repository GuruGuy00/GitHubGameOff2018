using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyController : IEnemyController
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

    /*
    private bool ApplyMoves(Vector3Int newPos)
    {
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothTime);

        if (Mathf.Approximately(transform.position.x, (float)newPos.x)
            && Mathf.Approximately(transform.position.y, (float)newPos.y))
        {
            transform.position = newPos;
            isMoving = false;
            if (moveList.Count == 0)
            {
                isProccessingMoves = false;
                return true;
            }
        }
        return false;
    }
    */
}
