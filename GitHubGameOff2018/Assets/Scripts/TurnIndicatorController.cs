using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnIndicatorController : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    private _GameManager gm;    

	void Start ()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        gm = FindObjectOfType<_GameManager>();
    }
	
	void Update ()
    {
        switch (gm.currentGameState)
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
        }
    }

}
