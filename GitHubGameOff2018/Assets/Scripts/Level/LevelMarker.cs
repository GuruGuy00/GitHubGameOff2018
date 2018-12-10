﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMarker : MonoBehaviour {

    public string levelName;

    public int totalObjectives;
    public int compleatedObjectives;

    public Sprite[] levelIndicator = new Sprite[3];

    public string LevelToLoad;

    //OnLoad - 
    // If compleated Objectives = 0 Use levelIndicator 0
    // IF compleated Objectives = total objectives Use levelIndicator 2
    // ELSE use levelIndicator 1

    //When Player lands on this object load the scene to play or splash screen to show level detail?
    //Do we do a onCollision enter, to tigger the load level? or something else

    // Use this for initialization
    private void Awake()
    {
        //Load for save data?
        LoadLevel();
    }

    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Load The level
        SceneManager.LoadScene(LevelToLoad);
        //Debug.Log("OnTriggerEnter2D - Triggered" + levelName);
        //SaveLevel();
    }

    public void SaveLevel()
    {
        LevelData ld = new LevelData(levelName, totalObjectives, compleatedObjectives);
        SaveSystem.SaveLevel(ld);
    }

    public void LoadLevel()
    {
        LevelData ld = SaveSystem.LoadData(levelName);
        totalObjectives = ld.TotalObjective;
        compleatedObjectives = ld.CompleatedObjectives; 
    }

}
