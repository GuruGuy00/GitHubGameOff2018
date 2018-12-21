using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInfo
{
    public Vector3Int movePos;
    public bool isJump = false;
    public bool isCollision = false;
    public bool hitPlayer = false;
    public int ActionPointCost = 0;
}
