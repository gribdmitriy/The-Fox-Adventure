using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractableSpike : BaseTrap
{
    [SerializeField] private float timeDelay, timeStart;

    private bool _expects, _attack, _reload, attacked;
    private State _state;
    private Animator _anim;
    private PolygonCollider2D _col;
    private SpriteRenderer _spr;

    void Start()
    {
        _state = State.Disabled;
        Invoke("SetStateExpects", timeStart);
        _expects = false;
        attacked = true;
        _col = GetComponent<PolygonCollider2D>();
        _spr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        switch(_state)
        {
            case State.Expects:
                if(!_expects){
                    if(!attacked){
                        Invoke("SetStateAttack", timeDelay);
                        _expects = true;
                        _attack = false;
                        _reload = false;
                    }else{
                        Invoke("SetStateReload", timeDelay);
                        _expects = true;
                        _attack = false;
                        _reload = false;
                    }
                }
            break;
            case State.Attack:
                _anim.Play("Attack");
            break;
            case State.Reload:
                _anim.Play("Reload");
            break;

        }

        Debug.Log(_state);
    }

    public void ToAttack()
    {
        _col.enabled = true;
        _expects = false;
        _attack = true;
        _reload = false;
        attacked = true;
    }

    public void Reload()
    {
        _col.enabled = false;
        _expects = false;
        _attack = false;
        _reload = true;
        attacked = false;
    }

    public void SetStateAttack()
    {
        _state = State.Attack;
    }

    public void SetStateReload()
    {
        _state = State.Reload;
    }

    public void SetStateExpects()
    {
        _state = State.Expects;
    }

    public void HideSprite()
    {
        _spr.enabled = false;
    }

    public void ShowSprite()
    {
        _spr.enabled = true;
    }

    enum SpikePosition
    {
        Left,
        Right,
        Bottom,
        Top
    }

    enum State
    {
        Attack,
        Reload,
        Expects,
        Disabled
    }
}
