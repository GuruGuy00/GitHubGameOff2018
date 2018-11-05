using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public GameObject player; 
    PlayerController playerController;
    List<string> moveList = new List<string>();

	// Use this for initialization
	void Start () {
       
        playerController = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddLeftMove()
    {

        moveList.Add("Left");
    }

    public void AddRightMove()
    {

        moveList.Add("Right");
    }

    public void AddJumpMove()
    {
        moveList.Add("Jump");

    }

    public void SubmitMoves()
    {
        foreach (string move in moveList)
        {

            Debug.Log(move);
            switch (move)
            {
                case "Right":
                    playerController.MoveRight();
                    break;
                case "Left":
                    playerController.MoveLeft();
                    break;
                case "Jump":
                    playerController.MoveUp();
                    playerController.MoveUp();
                    playerController.MoveUp();
                    playerController.MoveRight();
                    break;
                default:
                    break;
            }
         
        }

        moveList.Clear();
    }

}
