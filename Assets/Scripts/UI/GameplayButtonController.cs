using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayButtonController : MonoBehaviour
{
    [HideInInspector]
    public bool stateLeft, stateRight, stateUp;

    [Header("Sprite for pressed button")]
    public Sprite _pressedImage;

    [HeaderAttribute("Sprite for not pressed button")]
    public Sprite _notPressedImage;

    public GameplayButtonObserver _observer;
    private Image _img;

    void Start()
    {
        _img = GetComponent<Image>();
        _observer = GameObject.Find("Input Controller").GetComponent<InputController>();
        _img.sprite = _notPressedImage;
    }



    public void EnterButton ()
    {
        if(_img.sprite != _pressedImage)
            _img.sprite = _pressedImage;

        switch(gameObject.name)
        {
            case "Left button":
              stateLeft = true;
              _observer.UpdateStateLeftButton(stateLeft);
            break;
            case "Right button":
              stateRight = true;
              _observer.UpdateStateRightButton(stateRight);
            break;
            case "Up button":
              stateUp = true;
              _observer.UpdateStateUpButton(stateUp);
            break;
        }
    }

    public void ExitButton ()
    {
        if(_img.sprite != _notPressedImage)
            _img.sprite = _notPressedImage;

        switch(gameObject.name)
        {
            case "Left button":
              stateLeft = false;
              _observer.UpdateStateLeftButton(stateLeft);
            break;
            case "Right button":
              stateRight = false;
              _observer.UpdateStateRightButton(stateRight);
            break;
            case "Up button":
              stateUp = false;
              _observer.UpdateStateUpButton(stateUp);
            break;
        }
    }
}
