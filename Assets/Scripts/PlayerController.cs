using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : PhysicalMovingObject, InputsObserver, CollisionObserver, TrapObserver
{
    private Animator _anim;
    private Vector2 prePos, curPos;
    private State _state;
    private Face _face;
    private bool[] inputs, groundCollisions, wallCollisions, curInputs, preInputs;
    private bool glide, isRight, isLeft, alive, deathplayed;
    private int jumpPointer;
    private String currentLevel;
    GameManager gameManager;
    Animator uiAnimator;
    DeathCounter deathcounter;

    [HeaderAttribute("Moving speed")]
    public float moveSpeed;

    [HeaderAttribute("Jump force")]
    public float jumpForce;

    void Start()
    {
        deathcounter = GameObject.Find("DeathCount").GetComponent<DeathCounter>();
        uiAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        currentLevel = SceneManager.GetActiveScene().name;
        deathplayed = false;
        alive = true;
        _anim = GetComponent<Animator>();
        _face = Face.Right;
        _state = State.Jump;
        inputs = new bool[3];
        groundCollisions = new bool[3];
        wallCollisions = new bool[2];
        curInputs = new bool[3];
        preInputs = new bool[3];
        InputController.Instance.AddObserver(this);
    }

    public void UpdateInputs(bool[] inputs)
    {
        this.inputs = inputs;

        if(inputs[1] && _face != Face.Right){
            isLeft = false;
            isRight = true;
            _face = Face.Right;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }else if(inputs[0]  && _face != Face.Left){
            isRight = false;
            isLeft = true;
            _face = Face.Left;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    public void UpdateCollision(bool[] gCollisions, bool[] wCollisions)
    {
        groundCollisions = gCollisions;
        wallCollisions = wCollisions;

        if(groundCollisions[2] && (inputs[0] == inputs[1])){

            _rb.bodyType = RigidbodyType2D.Dynamic;
            _state = State.Stand;
        }
        if(groundCollisions[2] && (inputs[0] != inputs[1])){
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _state = State.Walk;
        }

        if(!groundCollisions[2] && !wallCollisions[0] && !wallCollisions[1] && jumpPointer == 0){
            _rb.bodyType = RigidbodyType2D.Dynamic;
            if(_rb.gravityScale != 1)
                _rb.gravityScale = 1;
            _state = State.Jump;
        }

        if(!groundCollisions[2] && !wallCollisions[0] && !wallCollisions[1] && jumpPointer == 1){
            _rb.bodyType = RigidbodyType2D.Dynamic;
            if(_rb.gravityScale != 1)
                _rb.gravityScale = 1;
            _state = State.Rebound;
        }

        if(!groundCollisions[2] && (wallCollisions[0] || wallCollisions[1]) && curPos.x == prePos.x){
            _state = State.Glide;
        }
    }

    void Update()
    {
        if(alive){
            UpdateCurInputs();
            curPos = _rb.position;
            switch(_state)
            {
                case State.Stand:
                    if(jumpPointer != 0)
                        jumpPointer = 0;
                    if(!glide)
                        glide = true;
                    _anim.Play("Stand");
                    if(groundCollisions[2] && (curInputs[2] && !preInputs[2]))
                        Jump(jumpForce);
                break;
                case State.Walk:
                    if(jumpPointer != 0)
                        jumpPointer = 0;
                    if(!glide)
                        glide = true;
                    if(_rb.gravityScale != 1)
                        _rb.gravityScale = 1;
                    _anim.Play("Walk");

                    if(inputs[0] && !inputs[1] && !groundCollisions[0]){
                        if(inputs[0]  && _face != Face.Left &&  _state != State.Glide){
                            _face = Face.Left;
                            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                        }
                        Move(-moveSpeed);
                    }else if(!inputs[0] && inputs[1] && !groundCollisions[1]){
                        if(inputs[1] && _face != Face.Right){
                            _face = Face.Right;
                            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                        }
                        Move(moveSpeed);
                    }

                    if(groundCollisions[2] && (curInputs[2] && !preInputs[2]))
                        Jump(jumpForce);
                break;
                case State.Jump:
                    if(!glide)
                        glide = true;
                    _anim.Play("Jump");
                    if(_rb.gravityScale != 1)
                        _rb.gravityScale = 1;
                    if(inputs[0] && !inputs[1])
                        Move(-moveSpeed + (-0.5f));
                    else if(!inputs[0] && inputs[1])
                        Move(moveSpeed + 0.5f);
                break;
                case State.Rebound:
                    if(!glide)
                        glide = true;
                    _anim.Play("Jump");
                    if(_rb.gravityScale != 1)
                        _rb.gravityScale = 1;

                        if(inputs[0] && !inputs[1])
                            Move(-0.5f);
                        else if(!inputs[0] && inputs[1])
                            Move(0.5f);
                break;
                case State.Glide:
                    if(!isLeft && wallCollisions[1])
                        _anim.Play("Glide");
                    else if(isLeft && wallCollisions[1])
                        _anim.Play("Glide2");

                    if(!isRight && wallCollisions[0])
                        _anim.Play("Glide");
                    else if(isRight && wallCollisions[0])
                        _anim.Play("Glide2");

                    if(jumpPointer != 1)
                        jumpPointer = 1;
                    if(glide){
                        _rb.bodyType = RigidbodyType2D.Static;
                        _rb.bodyType = RigidbodyType2D.Dynamic;
                        glide = false;
                    }
                    if(_rb.gravityScale != 0.1f)
                        _rb.gravityScale = 0.1f;

                    if(wallCollisions[1] && isLeft && (curInputs[2] && !preInputs[2])){
                        _rb.AddForce((new Vector2(0, 1.15f) + new Vector2(-0.25f, 0)) * jumpForce, ForceMode2D.Impulse);
                        _anim.Play("Jump");
                    } else if(wallCollisions[0] && isRight && (curInputs[2] && !preInputs[2])) {
                        _rb.AddForce((new Vector2(0, 1.15f) + new Vector2(0.25f, 0)) * jumpForce, ForceMode2D.Impulse);
                        _anim.Play("Jump");
                    }

                    if(wallCollisions[0] && !isRight && (curInputs[2] && !preInputs[2]))
                        _rb.AddForce((new Vector2(0, 0.25f) + new Vector2(0.08f, 0)) * jumpForce, ForceMode2D.Impulse);
                    else if(wallCollisions[1] && !isLeft && (curInputs[2] && !preInputs[2]))
                        _rb.AddForce((new Vector2(0, 0.25f) + new Vector2(-0.08f, 0)) * jumpForce, ForceMode2D.Impulse);
                break;
            }

            UpdatePreInputs();
            prePos = curPos;
        }else{
            if(!deathplayed){
                deathcounter.count.countDeath += 1;
                deathcounter.UpdateUI();
                uiAnimator.Play("ExitGameplayLevel");
                Invoke("RestartLevel", 0.9f);
                GetComponent<CapsuleCollider2D>().enabled = false;
                _rb.bodyType = RigidbodyType2D.Static;
                _rb.bodyType = RigidbodyType2D.Dynamic;
                Jump(jumpForce);
            }
            _state = State.Death;
            _anim.Play("Death");
            deathplayed = true;
        }
        //Debug.Log(_state);

    }

    private void RestartLevel()
    {
        gameManager.LoadLevel(currentLevel);
    }

    public void KillPlayer()
    {
        alive = false;
    }

    private void UpdateCurInputs()
    {
        for(int i = 0; i < 3; i++)
            curInputs[i] = inputs[i];
    }

    private void UpdatePreInputs()
    {
        for(int i = 0; i < 3; i++)
            preInputs[i] = curInputs[i];
    }

    public void SetStateWalk()
    {
        _state = State.Walk;
    }

    public void SetStateStand()
    {
        _state = State.Stand;
    }

    private enum State
    {
        Stand,
        Walk,
        Jump,
        Rebound,
        Glide,
        Death
    }

    private enum Face
    {
        Left,
        Right
    }
}
