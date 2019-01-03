using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyController : IEnemyController
{
    [HideInInspector] public Vector3Int moveDirection;

    private MoveInfo lastMove;

    public void Init(Vector3Int aimDirection)
    {
        tileUtils = TileUtils.Instance;
        worldLoc = Vector3Int.CeilToInt(transform.position);
        tileLoc = tileUtils.GetCellPos(tileUtils.groundTilemap, transform.position);
        moveDirection = aimDirection;
    }

    public override bool DoEnemyTurn(GameObject player)
    {
        Vector3Int intPos = Vector3Int.CeilToInt(transform.position);
        //Find the location we want to move towards
        int xVal = intPos.x + (moveDirection.x * moveDistance);
        Vector3Int moveLoc = new Vector3Int(xVal, intPos.y, 0);
        //Simulate moving to this location, stopping if we hit a wall OR will walk off a platform.
        List<MoveInfo> validMoves = CheckMoveValid(worldLoc, moveLoc, player, true);
        moveList.AddRange(validMoves);
        return true;
    }

    public override bool DoEnemyAction(GameObject player)
    {
        bool movesComplete = false;
        Vector3Int newPos = worldLoc;

        MoveInfo currMove = ProcessMoves(newPos);
        movesComplete = ApplyMoves(currMove.movePos);

        if (movesComplete)
        {
            lastMove = null;
            if (currMove.isCollision || currMove.hitPlayer)
            {
                Destroy(gameObject);
            }
        }

        worldLoc = currMove.movePos;
        tileLoc = tileUtils.GetCellPos(tileUtils.groundTilemap, transform.position);

        return movesComplete;
    }

    private MoveInfo ProcessMoves(Vector3Int newPos)
    {
        MoveInfo m = new MoveInfo();
        if (!isMoving && moveList.Count > 0)
        {
            isMoving = true;
            m = moveList[0];
            moveList.RemoveAt(0);
            lastMove = m;
        }
        else
        {
            if (lastMove == null)
            {
                Debug.LogError("Last Move Is Null!");
            }
            m = lastMove;
        }
        return m;
    }

    private List<MoveInfo> CheckMoveValid(Vector3Int startPos, Vector3Int endPos, GameObject player, bool xAxis)
    {
        List<MoveInfo> moves = new List<MoveInfo>();
        int moveCounter = 0;
        Vector3Int moveChecker = startPos;

        //Keep track of a current move which will be our final move point.
        //This will change as we hit collisions.
        MoveInfo finalMovePoint = CreateMovePoint(endPos, false);

        //Iterate until we've moved our entire move speed
        while (moveCounter < moveDistance)
        {
            moveChecker += moveDirection;
            bool hitWall = tileUtils.IsTileSolid(tileUtils.groundTilemap, moveChecker);
            bool hitPlayer = Vector3Int.CeilToInt(player.transform.position) == moveChecker;
            //Check if we are going outside of camera range, hit a wall, or are going to fall
            if (hitWall || hitPlayer)
            {
                finalMovePoint.movePos = moveChecker;
                finalMovePoint.isCollision = true;
                finalMovePoint.hitPlayer = hitPlayer;
                break;
            }
            moveCounter++;
        }
        //Add our final movement point to the move list
        moves.Add(finalMovePoint);
        return moves;
    }

    private MoveInfo CreateMovePoint(Vector3Int movePos, bool isCollision)
    {
        MoveInfo mi = new MoveInfo();
        mi.isCollision = isCollision;
        mi.movePos = movePos;
        return mi;
    }
}