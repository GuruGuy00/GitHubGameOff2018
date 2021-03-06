﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyController : IEnemyController
{
    private Vector3Int moveLoc;
    private int moveDir = -1;
    private MoveInfo lastMove;

    public override bool DoEnemyTurn(GameObject player)
    {
        //Find the location we want to move to
        int xVal = worldLoc.x + (moveDir * moveDistance);
        moveLoc = new Vector3Int(xVal, worldLoc.y, worldLoc.z);
        //Simulate moving to this location, stopping if we hit a wall OR will walk off a platform.
        List<MoveInfo> validMoves = CheckMoveValid(worldLoc, moveLoc, player);
        moveList.AddRange(validMoves);
        return true;
    }

    public override bool DoEnemyAction(GameObject player)
    {
        bool movesComplete = false;
        Vector3Int newPos = worldLoc;

        MoveInfo currMove = null;
        currMove = ProcessMoves(newPos, currMove);
        movesComplete = ApplyMoves(currMove.movePos);

        if (movesComplete)
        {
            lastMove = null;
            //Tell the Event Manager to notify all subscribers of the PlayerHit event.
            if (currMove.hitPlayer)
            {
                eventManager.HitPlayer();
            }
        }

        worldLoc = currMove.movePos;
        tileLoc = tileUtils.GetCellPos(tileUtils.groundTilemap, transform.position);

        return movesComplete;
    }

    private MoveInfo ProcessMoves(Vector3Int newPos, MoveInfo currMove)
    {
        if (!isMoving && moveList.Count > 0)
        {
            isMoving = true;
            currMove = moveList[0];
            lastMove = currMove;
            moveList.RemoveAt(0);
        }
        else
        {
            if (lastMove == null)
            {
                Debug.LogError("Last Move Is Null!");
            }
            currMove = lastMove;
        }
        return currMove;
    }

    private List<MoveInfo> CheckMoveValid(Vector3Int startPos, Vector3Int endPos, GameObject player)
    {
        List<MoveInfo> movePoints = new List<MoveInfo>();
        int moveCounter = 0;

        //Check both the location we move AND the ground underneath it
        Vector3Int moveChecker = startPos;
        Vector3Int groundChecker = new Vector3Int(moveChecker.x, moveChecker.y - 1, moveChecker.z);
        
        //Handle iteration when moveCheck.x < endPos.x and vice versa w/o repeating code
        int iterateDirection = 1;
        if (moveChecker.x > endPos.x)
        {
            iterateDirection = -1;
        }

        //Keep track of a current move which will be our final move point.
        //This will change as we hit collisions.
        MoveInfo finalMovePoint = CreateMovePoint(endPos, false);

        //Iterate until we've moved our entire move speed
        int loopSafety = 100;
        while (loopSafety > 0 && (moveCounter < moveDistance || moveChecker.x != endPos.x))
        {
            moveChecker.x += iterateDirection;
            groundChecker.x = moveChecker.x;
            bool hitWall = tileUtils.IsTileSolid(tileUtils.groundTilemap, moveChecker);
            bool willFall = !tileUtils.IsTileSolid(tileUtils.groundTilemap, groundChecker);
            bool inCameraView = enemyManager.IsLocInCameraView(moveChecker);
            bool hitPlayer = Vector3Int.CeilToInt(player.transform.position) == moveChecker;
            //Check if we hit the player
            if (hitPlayer)
            {
                finalMovePoint.movePos = moveChecker;
                finalMovePoint.isCollision = true;
                finalMovePoint.hitPlayer = hitPlayer;
                break;
            }
            //Check if we are going outside of camera range, hit a wall, or are going to fall
            else if (hitWall || willFall || !inCameraView)
            {
                //Reverse our direction of movement and iteration
                iterateDirection = -iterateDirection;
                moveDir = -moveDir;
                //Move back to our last position and create a collision point
                moveChecker.x += iterateDirection;
                movePoints.Add(CreateMovePoint(moveChecker, true));
                //Move back another space and create another point
                endPos.x = moveChecker.x + (iterateDirection * (moveDistance - moveCounter));
                finalMovePoint.movePos = new Vector3Int(endPos.x, moveChecker.y, moveChecker.z);
            }
            moveCounter++;
            loopSafety--; 
        }
        //Add our final movement point to the move list
        movePoints.Add(finalMovePoint);
        return movePoints;
    }

    private MoveInfo CreateMovePoint(Vector3Int movePos, bool isCollision)
    {
        MoveInfo mi = new MoveInfo();
        mi.isCollision = isCollision;
        mi.movePos = movePos;
        return mi;
    }
}
