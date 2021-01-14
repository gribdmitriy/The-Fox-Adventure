using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    private float windSpeed = -0.5f;

    void Update()
    {
        Vector2 pos = transform.position;
        pos = new Vector2(pos.x + windSpeed  * Time.deltaTime, pos.y);
        transform.position = pos;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.x < (min.x - 0.2f))
        {
            Destroy(gameObject);
        }
    }
}
