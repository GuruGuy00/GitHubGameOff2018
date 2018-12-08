using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PlayerController : ICharacterController
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
