using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Platform
{
    [SerializeField] private float IdleTime;
    [SerializeField] private List<Transform> checkpoints;
    [SerializeField] private float speed = 1.0F;
    [SerializeField] private Type type;
    [SerializeField] private int indexStartPoint;
    [SerializeField] private bool ascending;
   
    private Transform transform;
    private State state;
    private Transform startMarker, endMarker;
    private float startTime;
    private float journeyLength;
    private bool completed;
    



    private void Start()
    {
        state = State.Moving;
        transform = gameObject.GetComponent<Transform>();
        startMarker = checkpoints[indexStartPoint];

        if (checkpoints.Count - 1 > indexStartPoint && indexStartPoint >= 0 && ascending)
            endMarker = checkpoints[indexStartPoint + 1];
        else
        {
            ascending = false;
            endMarker = checkpoints[checkpoints.Count - 2];
        }
            

        if (checkpoints.Count > indexStartPoint && indexStartPoint > 0 && !ascending)
            endMarker = checkpoints[indexStartPoint - 1];
        else
        {
            ascending = true;
            endMarker = checkpoints[1];
        }
            
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        completed = false;
        transform.position = startMarker.position;
    }

    private void SwitchCheckpoints()
    {
        if(type == Type.Reversible)
        {
            if(ascending)
            {
                for (int i = 0; i < checkpoints.Count; i++)
                {
                    if (startMarker == checkpoints[i])
                    {
                        if (checkpoints.Count > i + 1)
                        {
                            startMarker = checkpoints[i + 1];
                            if (checkpoints.Count > i + 2)
                            {
                                endMarker = checkpoints[i + 2];
                            }
                            else
                            {
                                ascending = false;
                                endMarker = checkpoints[checkpoints.Count - 2];
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                for (int i = checkpoints.Count - 1; i >= 0; i--)
                {
                    if (startMarker == checkpoints[i])
                    {
                        if (0 <= i - 1)
                        {
                            startMarker = checkpoints[i - 1];
                            if (0 <= i - 2)
                            {
                                endMarker = checkpoints[i - 2];
                            }
                            else
                            {
                                ascending = true;
                                endMarker = checkpoints[1];
                            }
                        }
                        break;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < checkpoints.Count; i++)
            {
                if (startMarker == checkpoints[i])
                {
                    if (checkpoints.Count > i + 1)
                    {
                        startMarker = checkpoints[i + 1];
                        if (checkpoints.Count > i + 2)
                        {
                            endMarker = checkpoints[i + 2];
                        }
                        else
                        {
                            endMarker = checkpoints[0];
                        }
                    }
                    else
                    {
                        startMarker = checkpoints[0];
                        endMarker = checkpoints[1];
                    }
                    break;
                }
            }
        }

        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        state = State.Moving;
        completed = false;

    }

    private void Move()
    {
        if(!completed)
        {
            if(transform.position.x == endMarker.position.x && transform.position.y == endMarker.position.y)
                completed = true;
            
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
        }
        else
        {
            state = State.Idle;
            Invoke("SwitchCheckpoints", IdleTime);
        }
    }

    private void Update()
    {
        switch(state)
        {
            case State.Idle:

                break;

            case State.Moving:
                Move();
                break;

        }
    }

    enum State
    {
        Moving,
        Idle
    }

    enum Type
    {
        Circular,
        Reversible
    }

}