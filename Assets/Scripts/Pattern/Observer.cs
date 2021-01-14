using System;

public interface UIButtonObservable
﻿{
    void AddObserver(UIButtonObserver uib);﻿
    void RemoveObserver(UIButtonObserver uib);
    void PressedButton();
    void ReleasedButton();
    void ClickButton();
﻿}

public interface UIButtonObserver
{
    void UpdateButtonState(bool pressed, String nameButton);
}

public interface GameplayButtonObserver
{
    void UpdateStateLeftButton(bool pressedLeft);
    void UpdateStateRightButton(bool pressedRight);
    void UpdateStateUpButton(bool pressedUp);
}

public interface InputsObservable
{
    void AddObserver(InputsObserver io);
    void RemoveObserver(InputsObserver io);
    void NotifyObservers();
}

public interface InputsObserver
{
    void UpdateInputs(bool[] inputs);
}

public interface LevelLoaderObservable
{
    void AddObserver(LevelLoaderObserver llo);
    void RemoveObserver(LevelLoaderObserver llo);
    void NotifyObservers();
}

public interface LevelLoaderObserver
{
    void LoadLevel(String levelname);
}

public interface CollisionObservable
{
    void AddObserver(CollisionObserver co);
    void RemoveObserver(CollisionObserver co);
    void NotifyObservers();
}

public interface CollisionObserver
{
    void UpdateCollision(bool[] gCollisions, bool[] wCollisions);
}

public interface TrapObservable
{
    void AddObserver(TrapObserver to);
    void RemoveObserver(TrapObserver to);
    void NotifyObservers();
}

public interface TrapObserver
{
    void KillPlayer();
}
