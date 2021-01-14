using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsScreenController : MonoBehaviour, UIButtonObserver, LevelLoaderObservable
{
    private String nameButton, levelname;
    private bool buttonPressed;
    private List<LevelLoaderObserver> observers;
    private Animator uiAnimator;

    void Awake()
    {
        observers = new List<LevelLoaderObserver>();
        uiAnimator = GetComponent<Animator>();
        AddObserver(GameObject.Find("Game Manager").GetComponent<GameManager>());
    }

    public void UpdateButtonState(bool pressed, String nameButton)
    {
        this.nameButton = nameButton;
        buttonPressed = pressed;

        if(buttonPressed)
        {
            switch(this.nameButton)
            {
                case "MainScreen":
                    uiAnimator.Play("CloseScene");
                    levelname = "MainScreen";
                break;
                case "Level1":
                    uiAnimator.Play("CloseScene");
                    levelname = "Level1";
                break;
                case "Level2":
                    uiAnimator.Play("CloseScene");
                    levelname = "Level2";
                break;
                case "Level3":
                    uiAnimator.Play("CloseScene");
                    levelname = "Level3";
                break;
                case "Level5":
                    //stats open animation
                break;
                case "Level6":
                    //stats close animation
                break;
                case "Level7":
                    //open achievements
                break;
            }
        }
    }

    public void OpenScene()
    {
        uiAnimator.Play("OpenScene");
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
