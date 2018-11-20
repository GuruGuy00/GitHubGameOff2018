using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public GameObject player; 
    PlayerController playerController;
    List<string> moveList = new List<string>();
    
    public Transform playCardsParents;

    GameObject deck;

	// Use this for initialization
	void Start () {
        playerController = player.GetComponent<PlayerController>();
        deck = GameObject.FindGameObjectWithTag("Deck");
	}
	
	// Update is called once per frame
	void Update () {
		
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
        CardDisplay[] cardsToPlay = playCardsParents.GetComponentsInChildren<CardDisplay>();
        foreach (CardDisplay cardInfo in cardsToPlay)
        {
            //ToDo : larp this or something to make it look nice
            moveList.Add(cardInfo.card.moveName);

        }
        deck.GetComponent<Deck>().DiscardCards();
        playerController.setMoveList(moveList);

    }

}
