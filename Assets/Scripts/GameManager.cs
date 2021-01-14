using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, LevelLoaderObserver
{

    //public static GameManager Instance { get; private set; }

    public bool isLodaded;
    public String currentLevel;

    void Start()
    {
        isLodaded = true;
        currentLevel = SceneManager.GetActiveScene().name;
        if (currentLevel == "MainScreen") {
            GameObject.Find("Canvas").GetComponent<MainScreenController>().OpenScene();
        } else if (currentLevel == "Levels") {
            GameObject.Find("Canvas").GetComponent<LevelsScreenController>().OpenScene();
        } else if (currentLevel == "Level1") {
            GameObject.Find("Canvas").GetComponent<GameplayScreenController>().OpenScene();
        } else if (currentLevel == "Level2") {
            GameObject.Find("Canvas").GetComponent<GameplayScreenController>().OpenScene();
        } else if (currentLevel == "Level3") {
            GameObject.Find("Canvas").GetComponent<GameplayScreenController>().OpenScene();
        }
    }

    public void LoadLevel(String levelname)
    {
        StartLoading(levelname);
        /*switch(levelname)
        {
            case "Levels":
                StartLoading("Levels");
            break;
            case "MainScreen":
                StartLoading("MainScreen");
            break;

        }*/
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    public void StartLoading(String level)
    {
        StartCoroutine(LoadLevelAsync(level));
    }

    IEnumerator LoadLevelAsync(String levelname)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelname);
        while(!operation.isDone)
        {
            yield return null;
        }

        if(operation.isDone)
        {

        }
    }
}
