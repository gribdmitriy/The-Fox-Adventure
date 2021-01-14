using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayScreenController : MonoBehaviour, UIButtonObserver, LevelLoaderObservable
{
    private String nameButton, levelname;
    private bool buttonPressed;
    private List<LevelLoaderObserver> observers;
    private Animator uiAnimator;
    private GameObject PausePanel;

    void Awake()
    {
        observers = new List<LevelLoaderObserver>();
        uiAnimator = GetComponent<Animator>();
        AddObserver(GameObject.Find("Game Manager").GetComponent<GameManager>());
    }

    void Start()
    {
        PausePanel = GameObject.Find("PausePanel");
        PausePanel.SetActive(false);
    }

    public void UpdateButtonState(bool pressed, String nameButton)
    {
        this.nameButton = nameButton;
        buttonPressed = pressed;

        if(buttonPressed)
        {
            switch(this.nameButton)
            {
                case "Pause":
                    Invoke("Pause", 0.5f);
                    PausePanel.SetActive(true);
                    uiAnimator.Play("PauseGame");
                break;
                case "Unpause":
                    Unpause();
                    uiAnimator.Play("UnpauseGame");
                    PausePanel.SetActive(false);
                break;
                case "MainScreen":
                    Unpause();
                    uiAnimator.Play("CloseGameplayLevel");
                    levelname = "MainScreen";
                    PausePanel.SetActive(false);
                break;
                case "Restart":
                    Unpause();
                    uiAnimator.Play("CloseGameplayLevel");
                    levelname = SceneManager.GetActiveScene().name;
                    PausePanel.SetActive(false);
                break;
                case "Achievements":
                    //open achievements

                break;
            }
        }
    }

    public void SetNextLevel(String lvl)
    {
        levelname = lvl;
    }

    private void Pause()
    {
        GameObject.Find("Game Manager").GetComponent<GameManager>().PauseGame();
    }

    private void Unpause()
    {
        GameObject.Find("Game Manager").GetComponent<GameManager>().UnpauseGame();
    }

    public void OpenScene()
    {
        uiAnimator.Play("OpenGameplayLevel");
    }

    public void AddObserver(LevelLoaderObserver llo)
    {
        observers.Add(llo);
    }

    public void RemoveObserver(LevelLoaderObserver llo)
    {
        observers.Remove(llo);
    }

    public void NotifyObservers()
    {
        foreach(LevelLoaderObserver llo in observers)
        {
            llo.LoadLevel(levelname);
        }
    }

}
