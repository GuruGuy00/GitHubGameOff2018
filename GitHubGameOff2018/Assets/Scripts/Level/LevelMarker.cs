using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMarker : MonoBehaviour {

    public string levelName;

    public bool ReachedExit;
    public bool ClearedAllEnemies;
    public bool ClearedAllPickUps;

    public Sprite[] levelIndicator = new Sprite[3];

    public string LevelToLoad;

    public GameObject player;

    private bool isOnLevel = false;

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
        LoadPlayerData();
        LoadLevel();
    }

    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && isOnLevel)
        {
            PlayerData playerData = new PlayerData(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            SaveSystem.SavePlayerData(playerData, "01");

            SceneManager.LoadScene(LevelToLoad);
            //SaveLevel();

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("OnTriggerEnter2D - Triggered " + levelName);
        isOnLevel = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnLevel = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("OnTriggerStay2D - Triggered " + levelName);
        
    }

    public void SaveLevel()
    {
        LevelData ld = new LevelData(levelName, false,false,false);
        SaveSystem.SaveLevel(ld);
    }

    public void LoadLevel()
    {
        LevelData ld = SaveSystem.LoadData(levelName);
        ReachedExit = ld.ReachedExit;
        ClearedAllEnemies = ld.ClearedAllEnemies;
        ClearedAllPickUps = ld.ClearedAllPickUps;

    }

    public void LoadPlayerData()
    {
        PlayerData playerData = SaveSystem.LoadPlayerData("01");

        player.transform.position = new Vector3(playerData.WorldMapPosX, playerData.WorldMapPosY, playerData.WorldMapPosZ);
    }

}
