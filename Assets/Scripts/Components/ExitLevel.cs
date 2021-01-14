using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    ProgressController progressController;
    GameManager gameManager;
    Animator uiAnimator;
    GameplayScreenController gameScreenController;

    void Start()
    {
        gameScreenController = GameObject.Find("Canvas").GetComponent<GameplayScreenController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        progressController = GameObject.Find("Progress Controller").GetComponent<ProgressController>();

        uiAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player"){
            switch(gameManager.currentLevel)
            {
                case "MainScreen":
                    uiAnimator.Play("CloseDialogScene");
                    progressController.ChangeProgress(1);
                break;
                case "Level1":
                    gameScreenController.SetNextLevel("Level2");
                    uiAnimator.Play("ExitGameplayLevel");
                    progressController.ChangeProgress(2);
                break;
                case "Level2":
                    gameScreenController.SetNextLevel("Level3");
                    uiAnimator.Play("ExitGameplayLevel");
                    progressController.ChangeProgress(3);
                break;
                case "Level3":
                    gameScreenController.SetNextLevel("Levels");
                    uiAnimator.Play("ExitGameplayLevel");
                    progressController.ChangeProgress(4);
                break;
            }
        }
    }
}
