using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
	public GameObject _fly;
	public GameObject[] _waypoints;
	public float flightSpeed;
	private Vector2 previousPos, currentPos, direction;
	private int n;
	private DirState _state = DirState.upleft;

    void Start()
    {
    	n = 0;
    }

    void Update()
    {
    	if(Vector2.Distance(_fly.transform.position, _waypoints[n].transform.position) < 0.1f)
    	{
    		if(n < 2){
    			n++;
    		}else
    		{
    			n = 3;
    		}
    		if(n > 2)
    			n = 0;
    	}

		if(currentPos != null)
    		previousPos = currentPos;

    	float step = flightSpeed * Time.deltaTime;
    	_fly.transform.position = Vector2.MoveTowards(_fly.transform.position, _waypoints[n].transform.position, step);
		
		currentPos = _fly.transform.position;
        
		if(previousPos != null)
		{
	        if(currentPos.x > previousPos.x && currentPos.y > previousPos.y)
	    	{
	    		if(_state == DirState.dawnleft)
	    		{
	    			_fly.transform.localScale = new Vector3(-_fly.transform.localScale.x, -_fly.transform.localScale.y, _fly.transform.localScale.z);
	    			_state = DirState.upright;
	    		}
	    		if(_state == DirState.dawnright)
	    		{
	    			_fly.transform.localScale = new Vector3(_fly.transform.localScale.x, -_fly.transform.localScale.y, _fly.transform.localScale.z);
	    			_state = DirState.upright;
	    		}
	    		if(_state == DirState.upleft)
	    		{
	    			_fly.transform.localScale = new Vector3(-_fly.transform.localScale.x, _fly.transform.localScale.y, _fly.transform.localScale.z);
	    			_state = DirState.upright;
	    		}
	    	}
	    	if(currentPos.x < previousPos.x && currentPos.y > previousPos.y)
	    	{
	    		if(_state == DirState.dawnleft)
	    			_fly.transform.localScale = new Vector3(_fly.transform.localScale.x, -_fly.transform.localScale.y, _fly.transform.localScale.z);

	    		if(_state == DirState.dawnright)
	    			_fly.transform.localScale = new Vector3(-_fly.transform.localScale.x, -_fly.transform.localScale.y, _fly.transform.localScale.z);

	    		if(_state == DirState.upright)
	    			_fly.transform.localScale = new Vector3(-_fly.transform.localScale.x, _fly.transform.localScale.y, _fly.transform.localScale.z);

				_state = DirState.upleft;
	    	}
	    	if(currentPos.x > previousPos.x && currentPos.y < previousPos.y)
	    	{
	    		if(_state == DirState.dawnleft)
	    		{
	    			_fly.transform.localScale = new Vector3(-_fly.transform.localScale.x, _fly.transform.localScale.y, _fly.transform.localScale.z);
	    			_state = DirState.dawnright;
	    		}
	    		if(_state == DirState.upleft)
	    		{
	    			_fly.transform.localScale = new Vector3(-_fly.transform.localScale.x, -_fly.transform.localScale.y, _fly.transform.localScale.z);
	    			_state = DirState.dawnright;
	    		}
	    		if(_state == DirState.upright)
	    		{
	    			_fly.transform.localScale = new Vector3(_fly.transform.localScale.x, -_fly.transform.localScale.y, _fly.transform.localScale.z);
	    			_state = DirState.dawnright;
	    		}
	    	}
	    	if(currentPos.x < previousPos.x && currentPos.y < previousPos.y)
	    	{
	    		if(_state == DirState.dawnright)
	    		{
	    			_fly.transform.localScale = new Vector3(-_fly.transform.localScale.x, _fly.transform.localScale.y, _fly.transform.localScale.z);
	    			_state = DirState.dawnleft;
	    		}
	    		if(_state == DirState.upleft)
	    		{
	    			_fly.transform.localScale = new Vector3(_fly.transform.localScale.x, -_fly.transform.localScale.y, _fly.transform.localScale.z);
	    			_state = DirState.dawnleft;
	    		}
	    		if(_state == DirState.upright)
	    		{
	    			_fly.transform.localScale = new Vector3(-_fly.transform.localScale.x, -_fly.transform.localScale.y, _fly.transform.localScale.z);
	    			_state = DirState.dawnleft;
	    		}
	    	}
    	}


    }

    private enum DirState
    {
    	upleft,
    	upright,
    	dawnleft,
    	dawnright
    }
}
