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

    private MoveInfo lastMove;

    private int hitPoints = 75;
    public int HP
    {
        get { return hitPoints; }
    }


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

        //Spend Action
        MoveInfo currMove = null;
        currMove = ProcessMoves(newPos, currMove);
        movesComplete = ApplyMoves(currMove.movePos, currMove);

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

    protected override bool ApplyMoves(Vector3Int newPos, MoveInfo currMove = default(MoveInfo))
    {
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothTime);

        if (Mathf.Abs(transform.position.x - (float)newPos.x) < 0.001
            && Mathf.Abs(transform.position.y - (float)newPos.y) < 0.001)
        {
            transform.position = newPos;
            isMoving = false;
            if (currMove.hitEnemy)
            {
                hitPoints -= 25;
            }
            if (moveList.Count == 0)
            {
                isProccessingMoves = false;
                return true;
            }
        }
        return false;
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
        hitPoints = 0;
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

    public void SetMoveList(List<MoveInfo> moves)
    {
        moveList = moves;
        isProccessingMoves = true;
    }
}
