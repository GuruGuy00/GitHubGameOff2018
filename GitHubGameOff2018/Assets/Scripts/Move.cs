using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public GameObject player; 
    PlayerController playerController;
    List<string> moveList = new List<string>();
    
    public Transform playCardsParent;

    GameObject deck;
    Deck deckScript;

	// Use this for initialization
	void Start () {
        playerController = player.GetComponent<PlayerController>();
        deck = GameObject.FindGameObjectWithTag("Deck");
        deckScript = deck.GetComponent<Deck>();
    }

    public void AddLeftMove()
    {
        moveList.Add("Left");
        Debug.Log("Submitted : Move Left");
    }

    public void AddRightMove()
    {
        moveList.Add("Right");
        Debug.Log("Submitted : Move Right");
    }

    public void AddJumpMove()
    {
        moveList.Add("Jump");
        Debug.Log("Submitted : Jump");
    }

    public void AddMove(string move)
    {
        moveList.Add(move);
        Debug.Log("Submitted : " + move);
    }

    public void SubmitMoves()
    {
        CardDisplay[] cardsToPlay = playCardsParent.GetComponentsInChildren<CardDisplay>();
        foreach (CardDisplay cardInfo in cardsToPlay)
        {
            //ToDo : larp this or something to make it look nice
            moveList.Add(cardInfo.card.moveName);

        }
        deckScript.DiscardCards();
        playerController.setMoveList(moveList);
        deckScript.Deal(); //ToDo : maybe move else where for when the moves are compleated
    }

}
