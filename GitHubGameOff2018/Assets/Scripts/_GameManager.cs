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
    private MoveProcessor moveProcessor;
    private MovePreview movePreviewer;
    private TurnIndicatorController turnIndicatorScript;
    private EnemyManager enemyManager;

    private PlayerController playerController;

    //public PlayerController Player
    //{
    //    get { return playerController; }
    //}

    private void Awake()
    {
        //Get all scripts we need to talk to
        deck = FindObjectOfType<Deck>();
        playerController = FindObjectOfType<PlayerController>();
        moveProcessor = FindObjectOfType<MoveProcessor>();
        movePreviewer = FindObjectOfType<MovePreview>();
        turnIndicatorScript = FindObjectOfType<TurnIndicatorController>();
        enemyManager = FindObjectOfType<EnemyManager>();
        //Flag that we need to start the game up
        currentGameState = GameState.StartGame;
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
            case GameState.EnemyAction:
                //Stay in this state while the enemy's moves are processing
                ProcessEnemies();
                break;
            case GameState.EndGame:
                //Player died, display this somehow
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

    private void SetupPlayerTurn()
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

    private void PlayerMovesComplete()
    {
        //Consume Action Points
        currentGameState = GameState.EnemyTurn;
    }

    private void ProcessEnemies()
    {
        if (enemyManager.HandleEnemies(currentGameState, playerController.gameObject))
        {
            if (currentGameState == GameState.EnemyTurn)
            {
                EnemySubmitMoves();
            }
            else if (currentGameState == GameState.EnemyAction)
            {
                EnemyMovesComplete();
            }
        }
    }

    private void EnemySubmitMoves()
    {
        currentGameState = GameState.EnemyAction;
    }

    private void EnemyMovesComplete()
    {
        currentGameState = GameState.PlayerTurn;
        SetupPlayerTurn();
    }

}
