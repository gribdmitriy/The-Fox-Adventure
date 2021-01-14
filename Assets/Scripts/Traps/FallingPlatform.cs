using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
      void OnCollisionEnter2D(Collision2D coll) {
          if (coll.gameObject.name == "Player")
              Invoke("FallPlatform", 0.5f);
      }

      void FallPlatform()
      {
          Rigidbody2D _rb = GetComponent<Rigidbody2D>();
          _rb.bodyType = RigidbodyType2D.Dynamic;
          _rb.gravityScale = 0.2f;
      }
}
