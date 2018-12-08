using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacterController : MonoBehaviour
{
    protected List<MoveInfo> moveList = new List<MoveInfo>();
    protected bool isMoving = false;
    protected bool isProccessingMoves = false;

    protected Vector3 currentVelocity;
    protected float smoothTime = 0.1f;

    public Vector3Int worldLoc;
    public Vector3Int tileLoc;

    protected TileUtils tileUtils;

    void Start()
    {
        tileUtils = TileUtils.Instance;
        worldLoc = Vector3Int.CeilToInt(transform.position);
        tileLoc = tileUtils.GetCellPos(tileUtils.groundTilemap, transform.position);
    }

    public virtual bool ApplyMoves(Vector3Int newPos)
    {
        //ToDo : Play around to see which works the best?
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothTime);
        //transform.position = Vector3.Lerp(transform.position, playerWorldLoc, smoothTime);
        //transform.position = Vector3.MoveTowards(transform.position, playerWorldLoc, 0.0f);

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

    public bool IsMoving()
    {
        return isMoving;
    }

    public bool IsProcessingMoves()
    {
        return isProccessingMoves;
    }
}
