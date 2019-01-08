using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveHelper : MonoBehaviour
{

    public List<GameObject> cards;
    public Transform playedCards;

    public void CreateCard(string cardType)
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
