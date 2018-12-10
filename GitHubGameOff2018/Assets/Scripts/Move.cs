using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public GameObject player;
    
    List<string> moveList = new List<string>();
    
    public Transform playCardsParent;

    // Use this for initialization
    void Start () {

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