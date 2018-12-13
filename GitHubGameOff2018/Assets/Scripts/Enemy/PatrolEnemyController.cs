using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyController : IEnemyController
{
    private Vector3Int moveLoc;
    private int moveDir = -1;

    private Camera cam;
    private Plane[] planes;
    private BoxCollider2D objCollider;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("LevelCamera").GetComponent<Camera>();
        tileUtils = TileUtils.Instance;
        worldLoc = Vector3Int.CeilToInt(transform.position);
        tileLoc = tileUtils.GetCellPos(tileUtils.groundTilemap, transform.position);
    }

    public override bool DoEnemyTurn()
    {
        //Find the location we want to move to
        int xVal = worldLoc.x + (moveDir * moveSpeed);
        moveLoc = new Vector3Int(xVal, worldLoc.y, worldLoc.z);
        //Simulate moving to this location, stopping if we hit a wall OR will walk off a platform.
        List<MoveInfo> validMoves = CheckMoveValid(worldLoc, moveLoc, true);
        moveList.AddRange(validMoves);
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

    private List<MoveInfo> CheckMoveValid(Vector3Int startPos, Vector3Int endPos, bool xAxis)
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
        while (loopSafety > 0 && (moveCounter < moveSpeed || moveChecker.x != endPos.x))
        {
            moveChecker.x += iterateDirection;
            groundChecker.x = moveChecker.x;
            bool hitWall = tileUtils.IsTileSolid(tileUtils.groundTilemap, moveChecker);
            bool willFall = !tileUtils.IsTileSolid(tileUtils.groundTilemap, groundChecker);
            bool inCameraView = IsInCameraView(moveChecker);
            if (!inCameraView)
            {
                Debug.Log("");
            }
            //Check if we are going outside of camera range, hit a wall, or are going to fall
            if (hitWall || willFall || !inCameraView)
            {
                //Reverse our direction of movement and iteration
                iterateDirection = -iterateDirection;
                moveDir = -moveDir;
                //Move back to our last position and create a collision point
                moveChecker.x += iterateDirection;
                movePoints.Add(CreateMovePoint(moveChecker, true));
                //Move back another space and create another point
                endPos.x = moveChecker.x + (iterateDirection * (moveSpeed - moveCounter));
                finalMovePoint.movePos = new Vector3Int(endPos.x, moveChecker.y, moveChecker.z);
            }
            moveCounter++;
            loopSafety--; 
        }
        //Add our final movement point to the move list
        movePoints.Add(finalMovePoint);
        return movePoints;
    }

    private bool IsInCameraView(Vector3Int testLoc)
    {
        //Get a list of planes from the camera's frustum
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        //Get our child sprite's box collider
        Vector3 checkLoc = new Vector3(testLoc.x + 0.5f, testLoc.y + 0.5f, testLoc.z);
        Bounds b = new Bounds(checkLoc, new Vector3Int(1, 1, 1));
        //Check if the bounds of the collider are within the camera's frustrum
        return GeometryUtility.TestPlanesAABB(planes, b);
    }

    private MoveInfo CreateMovePoint(Vector3Int movePos, bool isCollision)
    {
        MoveInfo mi = new MoveInfo();
        mi.isCollision = isCollision;
        mi.movePos = movePos;
        return mi;
    }
}
