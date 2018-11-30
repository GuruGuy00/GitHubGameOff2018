using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public GameObject player;
    //PlayerController playerController;        //KD Comment out to clear warning
    List<string> moveList = new List<string>();
    
    public Transform playCardsParent;

    //GameObject deck;                          //KD Comment out to clear warning
    //Deck deckScript;                          //KD Comment out to clear warning

    //private MoveProcessor moveProcessor;      //KD Comment out to clear warning
    //private MovePreview movePreviewer;        //KD Comment out to clear warning

    // Use this for initialization
    void Start () {
        //playerController = player.GetComponent<PlayerController>();   //KD Comment out to clear warning
        //deck = GameObject.FindGameObjectWithTag("Deck");              //KD Comment out to clear warning
        //deckScript = deck.GetComponent<Deck>();                       //KD Comment out to clear warning
        //moveProcessor = GetComponent<MoveProcessor>();                //KD Comment out to clear warning
        //movePreviewer = GetComponent<MovePreview>();                  //KD Comment out to clear warning
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

}