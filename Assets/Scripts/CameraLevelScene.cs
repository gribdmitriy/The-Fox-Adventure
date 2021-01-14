using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLevelScene : MonoBehaviour
{
    public float speed;

    private Vector2 startPos;
    private Camera cam;

    private float targetPos;

    private void Start()
    {
        cam = GetComponent<Camera>();
        transform.position = new Vector3(9.5f, 0, -10);
        targetPos = transform.position.x;
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) startPos = cam.ScreenToWorldPoint(Input.mousePosition);
        else if (Input.GetMouseButton(0))
        {
            float pos = cam.ScreenToWorldPoint(Input.mousePosition).x - startPos.x;
            targetPos = Mathf.Clamp(transform.position.x - pos, 9.5f, 25.3f);
            
        }

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPos, speed * Time.deltaTime), transform.position.y, transform.position.z);

    }
}
