using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MoveProcessor : MonoBehaviour
{
    public GameObject player;
    public Transform playCardsParent;
    public Button submitButton;

    [HideInInspector] public List<MoveInfo> processedMoves;

    private TileUtils tileUtils;
    private PlayerController playerController;
    public int usedActionPoints = 0;

    void Start ()
    {
        processedMoves = new List<MoveInfo>();
        tileUtils = TileUtils.Instance;
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {

    }

    public List<MoveInfo> ProcessPlayedCards()
    {
        bool isJumping = false;
        int usableActionPoints = playerController.ActionPoints;
        playerController.UsedActionPoints = 0;
        processedMoves.Clear();

        //Get the player's current position
        Vector3Int referencePos = playerController.worldLoc;

        //Process all cards that are currently in the Play section
        CardDisplay[] cardsToPlay = playCardsParent.GetComponentsInChildren<CardDisplay>();
        foreach (CardDisplay cardInfo in cardsToPlay)
        {
            if (usableActionPoints >= cardInfo.card.actionCost)
            {
                //Debug.Log("We Have Actions points to use");
                submitButton.interactable = true;
                usableActionPoints -= cardInfo.card.actionCost;
                playerController.UsedActionPoints += cardInfo.card.actionCost;
            }
            else
            {
                //ToDo : Move last card back to the hand/alrt player that they are out of action point
                //Maybe disable the submit moves buttons/ tint card a color
                submitButton.interactable = false;
                //Debug.Log("We are out of action points");
            }

            MoveInfo move = ProcessMove(referencePos, cardInfo.card);
            if (move.isJump)
            {
                isJumping = true;
            }
            referencePos = move.movePos;
            processedMoves.Add(move);
        }

        //Add a fall move if we're airborne and didn't jump
        bool isGrounded = IsPlayerGrounded(referencePos);
        if (!isGrounded && !isJumping)
        {
            MoveInfo gravity = ProcessGravity(referencePos);
            processedMoves.Add(gravity);
        }

        return processedMoves;
    }

    private MoveInfo ProcessMove(Vector3Int currPos, Card card)
    {
        //ToDo : CardData has Vector3Int of the relative move postion
        // Use that data to set the new postion.

        MoveInfo moveToReturn = new MoveInfo();
        moveToReturn.movePos = currPos;
        
        moveToReturn.ActionPointCost = card.actionCost;

        switch (card.moveName)
        {
            case "Right":
                moveToReturn = ProcessMoveRight(moveToReturn);
                break;
            case "Left":
                moveToReturn = ProcessMoveLeft(moveToReturn);
                break;
            case "DashRight":
                moveToReturn = ProcessDashRight(moveToReturn);
                break;
            case "DashLeft":
                moveToReturn = ProcessDashLeft(moveToReturn);
                break;
            case "Jump":
                moveToReturn = ProcessJump(moveToReturn);
                moveToReturn.isJump = true;
                break;
            default:
                moveToReturn = ProccessGenericMove(moveToReturn, card);
                break;
        }
        return moveToReturn;
    }

    private MoveInfo ProcessGravity(Vector3Int currPos)
    {
        MoveInfo fallMove = new MoveInfo();
        fallMove.movePos = currPos;
        fallMove = ProcessMoveDown(fallMove);
        return fallMove;
    }

    private MoveInfo ProccessGenericMove(MoveInfo move, Card card)
    {
        bool xAxisChange = false;
        bool yAxisChange = false;
        Vector3Int checkPos = move.movePos + card.moveTo;

        //Check to see if its an xAxis or yAxis move
        if (move.movePos.x != checkPos.x)
        {
            xAxisChange = true;
        }

        move = CheckMoveValid(move, checkPos, xAxisChange);
        return move;
    }

    private MoveInfo ProcessMoveRight(MoveInfo move)
    {

        Vector3Int checkPos = new Vector3Int((int)move.movePos.x + 1, (int)move.movePos.y, 0);
        move = CheckMoveValid(move, checkPos, true);
        return move;
    }

    private MoveInfo ProcessMoveLeft(MoveInfo move)
    {
        Vector3Int checkPos = new Vector3Int((int)move.movePos.x - 1, (int)move.movePos.y, 0);
        move = CheckMoveValid(move, checkPos, true);
        return move;
    }

    private MoveInfo ProcessDashRight(MoveInfo move)
    {
        Vector3Int checkPos = new Vector3Int((int)move.movePos.x + 2, (int)move.movePos.y, 0);
        move = CheckMoveValid(move, checkPos, true);
        return move;
    }

    private MoveInfo ProcessDashLeft(MoveInfo move)
    {
        Vector3Int checkPos = new Vector3Int((int)move.movePos.x - 2, (int)move.movePos.y, 0);
        move = CheckMoveValid(move, checkPos, true);
        return move;
    }

    private MoveInfo ProcessJump(MoveInfo move)
    {
        Vector3Int checkPos = new Vector3Int((int)move.movePos.x, (int)move.movePos.y + 3, 0);
        move = CheckMoveValid(move, checkPos, false);
        return move;
    }

    private MoveInfo ProcessMoveUp(MoveInfo move)
    {
        Vector3Int checkPos = new Vector3Int((int)move.movePos.x, (int)move.movePos.y + 1, 0);
        move = CheckMoveValid(move, checkPos, false);
        return move;
    }

    private MoveInfo ProcessMoveDown(MoveInfo move)
    {
        Vector3Int checkPos = new Vector3Int((int)move.movePos.x, (int)move.movePos.y - 1, 0);
        move = CheckMoveValid(move, checkPos, false);
        return move;
    }

    private MoveInfo CheckMoveValid(MoveInfo move, Vector3Int endPos, bool xAxis)
    {
        int loopSafety = 100;
        Vector3Int moveChecker = move.movePos;
        //Loop until we hit something, reach our end point, or reach our loop limit
        while (loopSafety > 0)
        {
            bool reachedEndPoint = (xAxis && moveChecker.x == endPos.x) || (!xAxis && moveChecker.y == endPos.y);
            bool add = (xAxis && moveChecker.x < endPos.x) || (!xAxis && moveChecker.y < endPos.y); 
            if (!reachedEndPoint)
            {
                moveChecker = CheckMoveCollision(moveChecker, move, xAxis, add);
                if (move.isCollision)
                {
                    break;
                }
            }
            else
            {
                break;
            }
            loopSafety--;
        }
        move.movePos = moveChecker;
        return move;
    }

    private Vector3Int CheckMoveCollision(Vector3Int moveChecker, MoveInfo move, bool xAxis, bool add)
    {
        //Update the move checker
        if (xAxis && add) { moveChecker.x++; }
        else if (xAxis && !add) { moveChecker.x--; }
        else if (!xAxis && add) { moveChecker.y++; }
        else if (!xAxis && !add) { moveChecker.y--; }
        //Check if the move checker is on a solid tile
        if (tileUtils.IsTileSolid(tileUtils.groundTilemap, moveChecker))
        {
            move.isCollision = true;
            //Reverse our move if we hit something
            if (xAxis && add) { moveChecker.x--; }
            else if (xAxis && !add) { moveChecker.x++; }
            else if (!xAxis && add) { moveChecker.y--; }
            else if (!xAxis && !add) { moveChecker.y++; }
        }
        return moveChecker;
    }

    private bool IsPlayerGrounded(Vector3Int playerPos)
    {
        Vector3Int checkPos = new Vector3Int((int)playerPos.x, (int)playerPos.y - 1, 0);
        return tileUtils.IsTileSolid(tileUtils.groundTilemap, checkPos);
    }
}
