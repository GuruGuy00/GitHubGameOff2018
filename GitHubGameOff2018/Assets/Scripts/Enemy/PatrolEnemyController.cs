using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyController : IEnemyController
{
    private Vector3Int moveLoc;
    private int moveDir = -1;

    void Start()
    {
        tileUtils = TileUtils.Instance;
        worldLoc = Vector3Int.CeilToInt(transform.position);
        tileLoc = tileUtils.GetCellPos(tileUtils.groundTilemap, transform.position);
    }

    public override bool DoEnemyTurn()
    {
        int xVal = worldLoc.x + (moveDir * moveSpeed);
        moveLoc = new Vector3Int(xVal, worldLoc.y, worldLoc.z);

        MoveInfo move = new MoveInfo();
        move.movePos = worldLoc;
        move = CheckMoveValid(move, moveLoc, true);
        moveList.Add(move);

        return true;
    }

    public override bool DoEnemyAction()
    {
        bool movesComplete = false;
        Vector3Int newPos = worldLoc;

        newPos = ProcessMoves(newPos);
        movesComplete = ApplyMoves(newPos);

        worldLoc = newPos;
        tileLoc = tileUtils.GetCellPos(tileUtils.groundTilemap, transform.position);

        return movesComplete;
    }

    private Vector3Int ProcessMoves(Vector3Int newPos)
    {
        if (!isMoving && moveList.Count > 0)
        {
            isMoving = true;
            newPos = moveList[0].movePos;
            moveList.RemoveAt(0);
        }
        return newPos;
    }

    private MoveInfo CheckMoveValid(MoveInfo move, Vector3Int endPos, bool xAxis)
    {
        int loopSafety = 100;
        Vector3Int moveChecker = move.movePos;
        int x = move.movePos.x;
        while (loopSafety > 0)
        {
            if (moveChecker.x < endPos.x)
            {
                moveChecker.x++;
                if (tileUtils.IsTileSolid(tileUtils.groundTilemap, moveChecker))
                {
                    moveChecker.x--;
                    move.isCollision = true;
                    break;
                }
                loopSafety--;
            }
            else if (moveChecker.x > endPos.x)
            {
                moveChecker.x--;
                if (tileUtils.IsTileSolid(tileUtils.groundTilemap, moveChecker))
                {
                    moveChecker.x++;
                    move.isCollision = true;
                    break;
                }
                loopSafety--;
            }
            else
            {
                break;
            }
        }
        move.movePos = moveChecker;
        return move;
    }
}
