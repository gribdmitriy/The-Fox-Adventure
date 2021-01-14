using UnityEngine;

public class PhysicalMovingObject : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D _rb;
    public enum Rigidbody2d
    {
        Player,
        Enemy
    }

    public Rigidbody2d _enumRB;

    void Awake()
    {
        if(_enumRB == Rigidbody2d.Player)
            _rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    public virtual void Move(float speed)
    {
        //Vector2 pos = player.transform.position;
        //pos += new Vector2(speed, 0f) * Time.deltaTime;
        //player.transform.position = pos;

        Vector2 pos = _rb.position;
        pos += new Vector2(speed, 0f) * Time.deltaTime;
        _rb.position = pos;

    }

    public virtual void Jump(float jump)
    {

        //_rb.velocity = new Vector2(0f, jump);
        _rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
    }

}
