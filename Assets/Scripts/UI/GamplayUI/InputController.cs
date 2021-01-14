using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour, GameplayButtonObserver, InputsObservable
{
    public static InputController Instance { get; private set; }
    public List<InputsObserver> _observers;
    public bool keyboardInput;

    [HideInInspector]
    public bool[] gameplayKeyState;

    void Awake()
    {
        Instance = this;
        _observers = new List<InputsObserver>();
        gameplayKeyState = new bool[3];
    }

    public void UpdateStateLeftButton(bool _state)
    {
        gameplayKeyState[0] = _state;
        NotifyObservers();
    }

    public void UpdateStateRightButton(bool _state)
    {
        gameplayKeyState[1] = _state;
        NotifyObservers();
    }

    public void UpdateStateUpButton(bool _state)
    {
        gameplayKeyState[2] = _state;
        NotifyObservers();
    }

    public void AddObserver(InputsObserver io)
    {
        _observers.Add(io);
    }

    public void RemoveObserver(InputsObserver io)
    {
        _observers.Remove(io);
    }

    public void NotifyObservers()
    {
        foreach(InputsObserver io in _observers)
        {
            io.UpdateInputs(gameplayKeyState);
        }
    }

    void Update()
    {

        if(keyboardInput){
            if(Input.GetKey(KeyCode.A)) {
                UpdateStateLeftButton(true);
            } else {
                UpdateStateLeftButton(false);
            }

            if(Input.GetKey(KeyCode.D))
            {
                UpdateStateRightButton(true);
            } else {
                UpdateStateRightButton(false);
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                UpdateStateUpButton(true);
            } else  {
                UpdateStateUpButton(false);
            }
        }
    }
}
