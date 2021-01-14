using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private DialogScene dialogscene;

    void Start()
    {
        dialogscene = GameObject.Find("Dialog Scene").GetComponent<DialogScene>();
    }

    void Update()
    {
        if(transform.position.y == -4.17f)
        {
            dialogscene.scenestarted = true;
        }
    }
}
