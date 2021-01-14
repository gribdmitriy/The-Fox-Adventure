using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : BaseTrap
{
    [SerializeField] private float _delayFalling;
    private Rigidbody2D _rb;

    private void Start()
    {
        _trapIsActive = true;
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Static;
    }

    private void FixedUpdate()
    {
        if(Physics2D.Raycast(transform.position, Vector2.down, 5f, 1 << LayerMask.NameToLayer("Player")) && _trapIsActive){
            Invoke("FallSpike", _delayFalling);
            _trapIsActive = false;
        }
    }

    private void FallSpike()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.mass = 0.3f;
        _rb.gravityScale = 0.5f;
    }


}
