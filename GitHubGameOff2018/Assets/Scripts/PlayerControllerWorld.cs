using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerWorld : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.transform.position = this.transform.position + Vector3.up;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.position = this.transform.position + Vector3.right;
        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.position = this.transform.position + Vector3.down;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.position = this.transform.position + Vector3.left;
        }

    }
}
