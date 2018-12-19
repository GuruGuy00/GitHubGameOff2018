using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        //FindObjectOfType<GameStatus>().ResetGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene("Options");
    }

    public void LoadLevelSelectScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadSaveSlotScene()
    {
        SceneManager.LoadScene("SlotSelection");
    }

    public void LoadWorlMap(string saveSlot)
    {
        GameData.SaveSlot = saveSlot;
        Debug.Log("SaveSlot Set as : " + saveSlot);
        SceneManager.LoadScene("WorldMap");
    }

    public void ResetSlotData(string saveSlot)
    {

        //ToDo : writ esome code to delete all the files in this save slots folder
    }
}
