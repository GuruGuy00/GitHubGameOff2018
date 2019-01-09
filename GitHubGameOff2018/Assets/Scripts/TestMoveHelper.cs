using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveHelper : MonoBehaviour
{

    public List<GameObject> cards;
    public Transform playedCards;
    public PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();    
    }

    public void CreateCard(string cardType)
    {
        if (cardType == "AP")
        {
            if (playerController != null)
            {
                playerController.ActionPoints += 6;
            }   
        }
        else
        {
            foreach (GameObject card in cards)
            {
                CardDisplay cd = card.GetComponent<CardDisplay>();
                if (cd.card.moveName == cardType)
                {
                    Instantiate(card, playedCards);
                }
            }
        }
    }
}
