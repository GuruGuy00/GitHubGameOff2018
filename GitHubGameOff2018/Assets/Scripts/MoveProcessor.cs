using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveProcessor : MonoBehaviour
{
    public GameObject player;
    public Transform playCardsParent;

    [HideInInspector] public List<MoveInfo> processedMoves;
    [HideInInspector] public List<MoveInfo> previewMoves;

    private TileUtils tileUtils;
    private PlayerController playerController;

    void Start ()
    {
        processedMoves = new List<MoveInfo>();
        previewMoves = new List<MoveInfo>();
        tileUtils = TileUtils.Instance;
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        bool isJumping = false;
        processedMoves.Clear();

        //Get the player's current position
        Vector3Int referencePos = playerController.playerWorldLoc;

        //Process all cards that are currently in the Play section
        CardDisplay[] cardsToPlay = playCardsParent.GetComponentsInChildren<CardDisplay>();
        foreach (CardDisplay cardInfo in cardsToPlay)
        {
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
    }

    private MoveInfo ProcessMove(Vector3Int currPos, Card card)
    {
        MoveInfo moveToReturn = new MoveInfo();
        moveToReturn.movePos = currPos;
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

    private MoveInfo ProcessMoveRight(MoveInfo move)
    {
        Vector3Int checkPos = new Vector3Int((int)move.movePos.x + 1, (int)move.movePos.y, 0);
        move = CheckMoveValid(move, checkPos, true, false);
        return move;
    }

    private MoveInfo ProcessMoveLeft(MoveInfo move)
    {
        Vector3Int checkPos = new Vector3Int((int)move.movePos.x - 1, (int)move.movePos.y, 0);
        move = CheckMoveValid(move, checkPos, true, false);
        return move;
    }

    private MoveInfo ProcessDashRight(MoveInfo move)
    {
        Vector3Int checkPos = new Vector3Int((int)move.movePos.x + 2, (int)move.movePos.y, 0);
        move = CheckMoveValid(move, checkPos, true, false);
        return move;
    }

    private MoveInfo ProcessDashLeft(MoveInfo move)
    {
        Vector3Int checkPos = new Vector3Int((int)move.movePos.x - 2, (int)move.movePos.y, 0);
        move = CheckMoveValid(move, checkPos, true, false);
        return move;
    }

    private MoveInfo ProcessJump(MoveInfo move)
    {
        Vector3Int checkPos = new Vector3Int((int)move.movePos.x, (int)move.movePos.y + 3, 0);
        move = CheckMoveValid(move, checkPos, false, true);
        return move;
    }

    private MoveInfo ProcessMoveUp(MoveInfo move)
    {
        Vector3Int checkPos = new Vector3Int((int)move.movePos.x, (int)move.movePos.y + 1, 0);
        move = CheckMoveValid(move, checkPos, false, true);
        return move;
    }

    private MoveInfo ProcessMoveDown(MoveInfo move)
    {
        Vector3Int checkPos = new Vector3Int((int)move.movePos.x, (int)move.movePos.y - 1, 0);
        move = CheckMoveValid(move, checkPos, false, true);
        return move;
    }

    private MoveInfo CheckMoveValid(MoveInfo move, Vector3Int endPos, bool xAxis, bool yAxis)
    {
        int loopSafety = 100;
        Vector3Int moveChecker = move.movePos;
        if (xAxis)
        {
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
        }
        else if (yAxis)
        {
            int y = move.movePos.y;
            while (loopSafety > 0)
            {
                if (moveChecker.y < endPos.y)
                {
                    moveChecker.y++;
                    if (tileUtils.IsTileSolid(tileUtils.groundTilemap, moveChecker))
                    {
                        moveChecker.y--;
                        move.isCollision = true;
                        break;
                    }
                    loopSafety--;
                }
                else if (moveChecker.y > endPos.y)
                {
                    moveChecker.y--;
                    if (tileUtils.IsTileSolid(tileUtils.groundTilemap, moveChecker))
                    {
                        moveChecker.y++;
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
        }
        move.movePos = moveChecker;
        return move;
    }

    private bool IsPlayerGrounded(Vector3Int playerPos)
    {
        Vector3Int checkPos = new Vector3Int((int)playerPos.x, (int)playerPos.y - 1, 0);
        return tileUtils.IsTileSolid(tileUtils.groundTilemap, checkPos);
    }

}
