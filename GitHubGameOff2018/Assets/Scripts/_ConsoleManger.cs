using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class _ConsoleManger : MonoBehaviour {


    public TMP_InputField inputText;
    public GameObject AdminPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return)) {
            switch (inputText.text) {
                case "Hello":
                    Hello();
                    break;
                case "It's Over 9000!":
                    EnableAdmin();
                    break;
                case "Instant Transmission":
                    InstantTransmission();
                    break;
                default:
                    break;
            }
        }
    }

    private void InstantTransmission() {
        //ToDo : need to work out how best to do this?
        Debug.Log("Where Do you want to go?");
        inputText.text = "";
    }

    private void EnableAdmin() {
        AdminPanel.SetActive(true);
        inputText.text = "";
    }

    private void Hello() {
        Debug.Log("Hello, What is you name?");
        inputText.text = "";
    }
}
