using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemyController : ICharacterController
{
    protected Vector3Int startPosition;

    [SerializeField] protected MoveType moveType = MoveType.Smooth;
    public enum MoveType
    {
        Smooth,
        Constant
    }

    public int moveDistance;
    public float moveSpeed = 1f;
    public float moveTime = 0.1f;
    public int jumpSpeed;

    //Abstract methods to be defined by concrete enemy classes
    public abstract bool DoEnemyTurn(GameObject player);
    public abstract bool DoEnemyAction(GameObject player);

    void Start()
    {
        startPosition = worldLoc;
    }

    public override bool ApplyMoves(Vector3Int newPos)
    {
        if (moveType == MoveType.Smooth)
        {
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, moveTime);
        }
        else if (moveType == MoveType.Constant)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPos, moveSpeed);
        }

        if (Mathf.Abs(transform.position.x - (float)newPos.x) < 0.001
            && Mathf.Abs(transform.position.y - (float)newPos.y) < 0.001)
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
}