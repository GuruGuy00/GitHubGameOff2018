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
    
    public GameState currentGameState;
    public enum GameState
    {
        StartGame,
        PlayerTurn,
        PlayerAction,
        EnemyTurn,
        EnemyAction,
        EndGame
    }

    private Deck deck;
    private PlayerController playerController;
    private EnemyController enemyController;
    private MoveProcessor moveProcessor;
    private MovePreview movePreviewer;

    //private bool playerTurnSetup = false; //commented out to clear Warning
    private bool debugMessage = false;

    private void Awake()
    {
        //Get all scripts we need to talk to
        deck = FindObjectOfType<Deck>();
        playerController = FindObjectOfType<PlayerController>();
        enemyController = FindObjectOfType<EnemyController>();
        moveProcessor = FindObjectOfType<MoveProcessor>();
        movePreviewer = FindObjectOfType<MovePreview>();
        //Flag that we need to start the game up
        currentGameState = GameState.StartGame;
    }

    void Start()
    {

    }

    void Update()
    {
        switch (currentGameState)
        {
            case GameState.StartGame:
                StartGame();
                break;
            case GameState.PlayerTurn:
                if (debugMessage)
                {
                    debugMessage = false;
                    Debug.Log("Waiting for Player moves...");
                }
                //Allow the player to play and rearrange cards
                break;
            case GameState.PlayerAction:
                if (debugMessage)
                {
                    debugMessage = false;
                    Debug.Log("Processing Player moves...");
                }
                //Stay in this state while the player's moves are processing
                break;
            case GameState.EnemyTurn:
                if (debugMessage)
                {
                    debugMessage = false;
                    Debug.Log("Waiting for Enemy moves...");
                }
                //Run enemy pathfinding, decision-making, command processing, etc.
                break;
            case GameState.EnemyAction:
                if (debugMessage)
                {
                    debugMessage = false;
                    Debug.Log("Processing Enemy moves...");
                }
                //Stay in this state while the enemy's moves are processing
                break;
            case GameState.EndGame:
                //Stay in this state while the enemy's moves are processing
                break;
        }
    }

    private void StartGame()
    {
        Debug.Log("Starting the game...");
        debugMessage = true;
        //Deal cards to our player
        deck.Deal();
        playerController.ActionPointRoll();
        //The player will get the first turn
        currentGameState = GameState.PlayerTurn;
    }

    public void SetupPlayerTurn()
    {
        deck.Deal();
        playerController.ActionPointRoll();
    }

    public void PlayerSubmitMoves()
    {
        Debug.Log("Player submitted moves!");
        debugMessage = true;
        //Set the move list for Previewer and PlayeController
        playerController.ConsumeAP();
        List<MoveInfo> moves = new List<MoveInfo>(moveProcessor.processedMoves);
        playerController.setMoveList(moves);
        movePreviewer.setPreviewPoints(moves);
        //Discard all played cards
        deck.DiscardCards();
        //Flip ourselves to PlayerAction so the player can process moves
        currentGameState = GameState.PlayerAction;
    }

    public void PlayerMovesComplete()
    {
        Debug.Log("Player moves completed!");
        debugMessage = true;
        //Consume Action Points
        
        currentGameState = GameState.EnemyTurn;
    }

    public void EnemySubmitMoves()
    {
        Debug.Log("Enemy submitted moves!");
        debugMessage = true;
        //TODO: Remove this.  This is just to test turn changing basics
        enemyController.turnChangeTimer = enemyController.timerMax;
        currentGameState = GameState.EnemyAction;
    }

    public void EnemyMovesComplete()
    {
        Debug.Log("Enemy moves completed!");
        debugMessage = true;
        //TODO: Remove this.  This is just to test turn changing basics
        enemyController.turnChangeTimer = enemyController.timerMax;
        currentGameState = GameState.PlayerTurn;
        SetupPlayerTurn();
    }

}
