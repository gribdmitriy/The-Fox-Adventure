using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, UIButtonObservable
{
    private bool pressed;
    private List<UIButtonObserver> observers;
    private String nameButton;

    public enum ScreenController
    {
        MainScreenController,
        DialogScreenController,
        LevelsScreenController,
        GameplayScreenController
    }

    public ScreenController screenController;

    void Start()
    {
        pressed = false;
        nameButton = gameObject.name;
       
        observers = new List<UIButtonObserver>();

        if(screenController == ScreenController.MainScreenController){
            AddObserver(GameObject.Find("Canvas").GetComponent<MainScreenController>());
        }else if (screenController == ScreenController.DialogScreenController) {
            AddObserver(GameObject.Find("Canvas").GetComponent<DialogScreenController>());
        }else if (screenController == ScreenController.LevelsScreenController) {
            AddObserver(GameObject.Find("Canvas").GetComponent<LevelsScreenController>());

            switch(nameButton)
            {
                case "Level2":
                    if(PlayerPrefs.GetInt("Progress") >= 2)
                        UnlockButton();
                    else
                        LockButton();
                break;
                case "Level3":
                    if(PlayerPrefs.GetInt("Progress") >= 3)
                        UnlockButton();
                    else
                        LockButton();
                break;
            }
        }else if (screenController == ScreenController.GameplayScreenController) {
            AddObserver(GameObject.Find("Canvas").GetComponent<GameplayScreenController>());
        }
    }

    public void AddObserver(UIButtonObserver uib)
    {
        observers.Add(uib);
    }

    public void RemoveObserver(UIButtonObserver uib)
    {
        observers.Remove(uib);
    }

    public void PressedButton()
    {
        pressed = true;
        foreach(UIButtonObserver uib in observers)
        {
            uib.UpdateButtonState(pressed, nameButton);
        }
    }

    public void ReleasedButton()
    {
        pressed = false;
        foreach(UIButtonObserver uib in observers)
        {
            uib.UpdateButtonState(pressed, nameButton);
        }
    }

    private void OnMouseDown()
    {
        ClickButton();
    }

    private void OnMouseUp()
    {
        ReleasedButton();
    }

    public void ClickButton()
    {
        pressed = true;
        foreach(UIButtonObserver uib in observers)
        {
            uib.UpdateButtonState(pressed, nameButton);
        }
    }

    public void LockButton()
    {
        GetComponent<Button>().interactable = false;
    }

    public void UnlockButton()
    {
        GetComponent<Button>().interactable = true;
    }
}
