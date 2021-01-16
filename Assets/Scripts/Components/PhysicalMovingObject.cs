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

    public virtual void Jump(float jump, bool leftInput, bool rightInput)
    {

        if (leftInput && !rightInput)
            _rb.AddForce((Vector2.up + Vector2.left / 15) * jump, ForceMode2D.Impulse);

        if (rightInput && !leftInput)
            _rb.AddForce((Vector2.up + Vector2.right / 15) * jump, ForceMode2D.Impulse);

        if (!rightInput && !leftInput)
            _rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
    }

}
