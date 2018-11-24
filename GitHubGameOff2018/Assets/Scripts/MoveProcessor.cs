using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveProcessor : MonoBehaviour
{
    public GameObject player;
    public Transform playCardsParent;

    [HideInInspector] public List<Vector3Int> processedMoves;
    [HideInInspector] public List<Vector3Int> previewMoves;

    private TileUtils tileUtils;
    private PlayerController playerController;

    void Start ()
    {
        processedMoves = new List<Vector3Int>();
        previewMoves = new List<Vector3Int>();
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
            processedMoves.Add(move.movePos);
        }

        //Add a fall move if we're airborne and didn't jump
        bool isGrounded = IsPlayerGrounded(referencePos);
        if (!isGrounded && !isJumping)
        {
            MoveInfo fallMove = ProcessGravity(referencePos);
            processedMoves.Add(fallMove.movePos);
        }
    }

    private MoveInfo ProcessMove(Vector3Int currPos, Card card)
    {
        MoveInfo moveToReturn = new MoveInfo();
        switch (card.moveName)
        {
            case "Right":
                moveToReturn.movePos = ProcessMoveRight(currPos);
                break;
            case "Left":
                moveToReturn.movePos = ProcessMoveLeft(currPos);
                break;
            case "DashRight":
                moveToReturn.movePos = ProcessDashRight(currPos);
                break;
            case "DashLeft":
                moveToReturn.movePos = ProcessDashLeft(currPos);
                break;
            case "Jump":
                moveToReturn.movePos = ProcessJump(currPos);
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
        fallMove.movePos = ProcessMoveDown(currPos);
        return fallMove;
    }

    private Vector3Int ProcessMoveRight(Vector3Int pos)
    {
        Vector3Int checkPos = new Vector3Int((int)pos.x + 1, (int)pos.y, 0);
        pos = CheckMoveValid(pos, checkPos, true, false);
        return pos;
    }

    private Vector3Int ProcessMoveLeft(Vector3Int pos)
    {
        Vector3Int checkPos = new Vector3Int((int)pos.x - 1, (int)pos.y, 0);
        pos = CheckMoveValid(pos, checkPos, true, false);
        return pos;
    }

    private Vector3Int ProcessDashRight(Vector3Int pos)
    {
        Vector3Int checkPos = new Vector3Int((int)pos.x + 2, (int)pos.y, 0);
        pos = CheckMoveValid(pos, checkPos, true, false);
        return pos;
    }

    private Vector3Int ProcessDashLeft(Vector3Int pos)
    {
        Vector3Int checkPos = new Vector3Int((int)pos.x - 2, (int)pos.y, 0);
        pos = CheckMoveValid(pos, checkPos, true, false);
        return pos;
    }

    private Vector3Int ProcessJump(Vector3Int pos)
    {
        Vector3Int checkPos = new Vector3Int((int)pos.x, (int)pos.y + 3, 0);
        pos = CheckMoveValid(pos, checkPos, false, true);
        return pos;
    }

    private Vector3Int ProcessMoveUp(Vector3Int pos)
    {
        Vector3Int checkPos = new Vector3Int((int)pos.x, (int)pos.y + 1, 0);
        pos = CheckMoveValid(pos, checkPos, false, true);
        return pos;
    }

    private Vector3Int ProcessMoveDown(Vector3Int pos)
    {
        Vector3Int checkPos = new Vector3Int((int)pos.x, (int)pos.y - 1, 0);
        pos = CheckMoveValid(pos, checkPos, false, true);
        return pos;
    }

    private Vector3Int CheckMoveValid(Vector3Int startPos, Vector3Int endPos, bool xAxis, bool yAxis)
    {
        int loopSafety = 100;
        Vector3Int moveChecker = startPos;
        if (xAxis)
        {
            int x = startPos.x;
            while (loopSafety > 0)
            {
                if (moveChecker.x < endPos.x)
                {
                    moveChecker.x++;
                    if (tileUtils.IsTileSolid(tileUtils.groundTilemap, moveChecker))
                    {
                        moveChecker.x--;
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
            int y = startPos.y;
            while (loopSafety > 0)
            {
                if (moveChecker.y < endPos.y)
                {
                    moveChecker.y++;
                    if (tileUtils.IsTileSolid(tileUtils.groundTilemap, moveChecker))
                    {
                        moveChecker.y--;
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
        return moveChecker;
    }

    private bool IsPlayerGrounded(Vector3Int playerPos)
    {
        Vector3Int checkPos = new Vector3Int((int)playerPos.x, (int)playerPos.y - 1, 0);
        return tileUtils.IsTileSolid(tileUtils.groundTilemap, checkPos);
    }

}
