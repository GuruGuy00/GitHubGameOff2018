using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PlayerController : ICharacterController
{
    //Used the below refrence to get things going
    //https://github.com/Cawotte/SmallWorld_WeeklyJam40

    public TextMeshProUGUI ActionPointText;

    public int ActionPoints;
    public int UsedActionPoints;

    [Tooltip("Random roll(1-6) when check, else roll 6 all the time")]
    private bool randomRoll = true;
    private int HP = 100; //TODO: Figure out if we want HP or one-hit death

    void OnEnable()
    {
        EventManager.OnPlayerHit += TakeDamage;
    }

    void OnDisable()
    {
        EventManager.OnPlayerHit -= TakeDamage;
    }

    public bool PlayerUpdate()
    {
        bool movesComplete = false;
        Vector3Int newPos = worldLoc;

        CurrentActionPoints();
        
        //Spend Action
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

    private void TakeDamage()
    {
        HP = 0; //TODO: Receive damage number and lower HP accordingly
        if (IsPlayerDead())
        {
            //TODO: Notify the EventManager that we died?
            //TODO: Play death animation
        }
    }

    public bool IsPlayerDead()
    {
        return HP <= 0;
    }

    public void setMoveList(List<MoveInfo> moves)
    {
        moveList = moves;
        isProccessingMoves = true;
    }
}
