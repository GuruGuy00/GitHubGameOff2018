using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject menuPanel; 

    private Deck deck;
    private PlayerController playerController;
    private EnemyController enemyController;
    private MoveProcessor moveProcessor;
    private MovePreview movePreviewer;
    private TurnIndicatorController turnIndicatorScript;

    private void Awake()
    {
        //Get all scripts we need to talk to
        deck = FindObjectOfType<Deck>();
        playerController = FindObjectOfType<PlayerController>();
        enemyController = FindObjectOfType<EnemyController>();
        moveProcessor = FindObjectOfType<MoveProcessor>();
        movePreviewer = FindObjectOfType<MovePreview>();
        turnIndicatorScript = FindObjectOfType<TurnIndicatorController>();
        //Flag that we need to start the game up
        currentGameState = GameState.StartGame;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPanel.SetActive(!menuPanel.activeInHierarchy);
        }

        switch (currentGameState)
        {
            case GameState.StartGame:
                StartGame();
                break;
            case GameState.PlayerTurn:
                //Allow the player to play and rearrange cards
                movePreviewer.DoPreview();
                break;
            case GameState.PlayerAction:
                //Stay in this state while the player's moves are processing
                if (playerController.PlayerUpdate())
                {
                    PlayerMovesComplete();
                }
                else
                {
                    movePreviewer.DoPreview();
                }
                break;
            case GameState.EnemyTurn:
                //Run enemy pathfinding, decision-making, command processing, etc.
                if (enemyController.EnemyTurn())
                {
                    EnemySubmitMoves();
                }
                break;
            case GameState.EnemyAction:
                //Stay in this state while the enemy's moves are processing
                if (enemyController.EnemyAction())
                {
                    EnemyMovesComplete();
                }
                break;
            case GameState.EndGame:
                //Stay in this state while the enemy's moves are processing
                break;
        }

        //Keep our turn indicator updating every frame
        turnIndicatorScript.UpdateTurnIndicator(currentGameState);
    }

    private void StartGame()
    {
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
        //Consume Action Points
        currentGameState = GameState.EnemyTurn;
    }

    public void EnemySubmitMoves()
    {
        //TODO: Remove this.  This is just to test turn changing basics
        enemyController.turnChangeTimer = enemyController.timerMax;
        currentGameState = GameState.EnemyAction;
    }

    public void EnemyMovesComplete()
    {
        //TODO: Remove this.  This is just to test turn changing basics
        enemyController.turnChangeTimer = enemyController.timerMax;
        currentGameState = GameState.PlayerTurn;
        SetupPlayerTurn();
    }

}
