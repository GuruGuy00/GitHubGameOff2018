using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameState gameState;
    public enum GameState
    {
        PlayerTurn,
        EnemyTurn
    }

    private Button[] buttons;

    private void Start()
    {
        gameState = GameState.PlayerTurn;
        buttons = FindObjectsOfType<Button>();
    }

    void Update()
    {
        
    }

    public void StartPlayerTurn()
    {
        gameState = GameState.PlayerTurn;
    }

    public void StartEnemyTurn()
    {
        gameState = GameState.EnemyTurn;
    }

}