using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //Player Hit event
    public delegate void PlayerHitAction();
    public static event PlayerHitAction OnPlayerHit;
    public void HitPlayer()
    {
        OnPlayerHit();
    }

    /*
    //Player Died event
    public delegate void PlayerDiedAction();
    public static event PlayerDiedAction OnPlayerDeath;
    public void PlayerDied()
    {
        OnPlayerDeath();
    }
    */
}
