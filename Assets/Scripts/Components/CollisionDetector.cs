using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour, CollisionObservable
{
    public List<CollisionObserver> _observers;

    [HideInInspector]
    public bool[] groundCollisions, wallCollisions;

    void Start()
    {
        _observers = new List<CollisionObserver>();
        groundCollisions = new bool[3];
        wallCollisions = new bool[2];
        AddObserver(gameObject.transform.parent.GetComponent<PlayerController>());
    }

    void FixedUpdate()
    {
        if(Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.4f), Vector2.left, 0.18f, 1 << LayerMask.NameToLayer("Ground")) ||
        Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.2f), Vector2.left, 0.18f, 1 << LayerMask.NameToLayer("Ground")) ||
        Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, 0.18f, 1 << LayerMask.NameToLayer("Ground")) ||
        Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.2f), Vector2.left, 0.18f, 1 << LayerMask.NameToLayer("Ground")) ||
        Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.4f), Vector2.left, 0.18f, 1 << LayerMask.NameToLayer("Ground"))){
            groundCollisions[0] = true;
            NotifyObservers();
        }else{
            groundCollisions[0] = false;
            NotifyObservers();
        }

        if(Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.4f), Vector2.right, 0.18f, 1 << LayerMask.NameToLayer("Ground")) ||
        Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.2f), Vector2.right, 0.18f, 1 << LayerMask.NameToLayer("Ground")) ||
        Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 0.18f, 1 << LayerMask.NameToLayer("Ground")) ||
        Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.2f), Vector2.right, 0.18f, 1 << LayerMask.NameToLayer("Ground")) ||
        Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.4f), Vector2.right, 0.18f, 1 << LayerMask.NameToLayer("Ground"))){
            groundCollisions[1] = true;
            NotifyObservers();
        }else{
            groundCollisions[1] = false;
            NotifyObservers();
        }
        if(Physics2D.Raycast(transform.position, Vector2.down, 0.55f, 1 << LayerMask.NameToLayer("Ground")) ||
        Physics2D.Raycast(new Vector2(transform.position.x - 0.089f, transform.position.y), Vector2.down, 0.55f, 1 << LayerMask.NameToLayer("Ground")) ||
        Physics2D.Raycast(new Vector2(transform.position.x + 0.089f, transform.position.y), Vector2.down, 0.55f, 1 << LayerMask.NameToLayer("Ground")))
        {
            groundCollisions[2] = true;
            NotifyObservers();
        }
        else
        {
            groundCollisions[2] = false;
            NotifyObservers();
        }

        if(Physics2D.Raycast(transform.position, Vector2.left, 0.18f, 1 << LayerMask.NameToLayer("Wall"))){
            wallCollisions[0] = true;
            NotifyObservers();
        }
        else{
            wallCollisions[0] = false;
            NotifyObservers();
        }
        if(Physics2D.Raycast(transform.position, Vector2.right, 0.18f, 1 << LayerMask.NameToLayer("Wall"))){
            wallCollisions[1] = true;
            NotifyObservers();
        }
        else{
            wallCollisions[1] = false;
            NotifyObservers();
        }
    }

    public void AddObserver(CollisionObserver co)
    {
        _observers.Add(co);
    }

    public void RemoveObserver(CollisionObserver co)
    {
        _observers.Remove(co);
    }

    public void NotifyObservers()
    {
        foreach(CollisionObserver co in _observers)
        {
            co.UpdateCollision(groundCollisions, wallCollisions);
        }
    }
}
