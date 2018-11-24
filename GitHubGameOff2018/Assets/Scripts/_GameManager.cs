using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour {

    //Game Start
    //  Deal Cards

    //Game Loop
    //  Place Cards to move
    //  Show Preview of moves
    //  Submit Moves / End Turn
    //  enemy AI queues move
    //      based on where the player is currently (pre-move proccess)???
    //  Proccess Player Moves
    //  Pricess enemy moves
    //  Check for win/lose conditions
    //  Deal cards 

    public GameState gameState;
    public enum GameState
    {
        PlayerTurn,
        EnemyTurn
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
