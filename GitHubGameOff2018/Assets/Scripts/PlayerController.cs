using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PlayerController : MonoBehaviour
{
    //Used the below refrence to get things going
    //https://github.com/Cawotte/SmallWorld_WeeklyJam40

    public int ActionPoints;
    public int UsedActionPoints;

    [Tooltip("Random roll(1-6) when check, else roll 6 all the time")]
    public bool randomRoll = true;

    public TextMeshProUGUI ActionPointText;

    public Vector3Int playerWorldLoc;
    public Vector3Int playerTileLoc;
    private Vector3 startPos;

    public Vector3 LocTest;

    Vector3 currentVelocity;
    public float smoothTime = 0.25f;

    //ToDo: Get better movements tile by tile
    private List<MoveInfo> moveList = new List<MoveInfo>();
    private bool isMoving = false;
    private bool isProccessingMoves = false;

    private TileUtils tileUtils;

    void Start ()
    {
        tileUtils = TileUtils.Instance;
        //ToDo : fix this up, need to read start pos from var
        startPos = transform.position;
        playerWorldLoc = Vector3Int.CeilToInt(startPos);
        playerTileLoc = tileUtils.GetCellPos(tileUtils.groundTilemap, transform.position);
        LocTest = startPos;
    }

    public bool PlayerUpdate()
    {
        bool movesComplete = false;
        Vector3Int newPos = playerWorldLoc;

        CurrentActionPoints();
        
        //Spend Action
        newPos = ProcessMoves(newPos);
        movesComplete = ApplyMoves(newPos);

        playerWorldLoc = newPos;
        playerTileLoc = tileUtils.GetCellPos(tileUtils.groundTilemap, transform.position);

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

    private bool ApplyMoves(Vector3Int newPos)
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
                //ToDo : maybe add an update to the postion so that we are exactly where we wanted to be.
                isProccessingMoves = false;
                return true;
            }
        }
        return false;
    }

    public void setMoveList(List<MoveInfo> moves)
    {
        moveList = moves;
        isProccessingMoves = true;
    }

    public bool IsPlayerMoving()
    {
        return isMoving;
    }

    public bool IsProcessingMoves()
    {
        return isProccessingMoves;
    }

    public void ActionPointRoll()
    {
        if (randomRoll)
        {
            int roll = Random.Range(1, 7);
            ActionPoints += roll;
        }
        else
        {
            ActionPoints += 6;
        }

    }

    public void ConsumeAP()
    {
        ActionPoints -= UsedActionPoints;
    }

    public void CurrentActionPoints()
    {
        ActionPointText.text = (ActionPoints - UsedActionPoints).ToString();
    }
}
