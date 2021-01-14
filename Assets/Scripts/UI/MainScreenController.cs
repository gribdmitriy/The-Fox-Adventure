using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenController : MonoBehaviour, UIButtonObserver, LevelLoaderObservable
{
    private String nameButton, levelname;
    private bool buttonPressed;
    private List<LevelLoaderObserver> observers;
    private Animator uiAnimator;
    private GameObject PausePanel;
    private ProgressController progressController;

    void Awake()
    {
        observers = new List<LevelLoaderObserver>();
        uiAnimator = GetComponent<Animator>();
        AddObserver(GameObject.Find("Game Manager").GetComponent<GameManager>());
        progressController = GameObject.Find("Progress Controller").GetComponent<ProgressController>();
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
                case "TapToPlay":
                    if(!PlayerPrefs.HasKey("Progress")){
                        uiAnimator.Play("HideInterface");
                        GameObject.Find("Main Camera").GetComponent<Animator>().Play("CameraDown");
                    }else{
                        uiAnimator.Play("CloseMainScene");
                    }
                    levelname = "Levels";
                break;
                case "Settings":
                    Invoke("Pause", 0.5f);
                    PausePanel.SetActive(true);
                    uiAnimator.Play("Pause");
                break;
                case "CloseSettings":
                    Unpause();
                    PausePanel.SetActive(false);
                    uiAnimator.Play("Unpause");
                break;
                case "GooglePlayGames":
                    Debug.Log("GooglePlayGames Clicked");
                break;
                case "Stats":
                    //stats open animation
                break;
                case "CloseStats":
                    //stats close animation
                break;
                case "Achievements":
                    //open achievements
                break;
            }
        }
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
        uiAnimator.Play("OpenMainScene");
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
