using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnIndicatorController : MonoBehaviour
{
    private TextMeshProUGUI tmp;

	void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }
	
	public void UpdateTurnIndicator(_GameManager.GameState currentState)
    {
        switch (currentState)
        {
            case _GameManager.GameState.PlayerTurn:
                tmp.text = "Player's Turn";
                tmp.color = new Color32(0, 255, 0, 255);
                break;
            case _GameManager.GameState.PlayerAction:
                tmp.text = "Player Moving...";
                tmp.color = new Color32(0, 255, 0, 255);
                break;
            case _GameManager.GameState.EnemyTurn:
                tmp.text = "Enemy's Turn";
                tmp.color = new Color32(255, 0, 0, 255);
                break;
            case _GameManager.GameState.EnemyAction:
                tmp.text = "Enemy Moving...";
                tmp.color = new Color32(255, 0, 0, 255);
                break;
            case _GameManager.GameState.EndGame:
                tmp.text = "GAME OVER";
                tmp.color = new Color32(255, 0, 0, 255);
                break;
        }
    }

}
