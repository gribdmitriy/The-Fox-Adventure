using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private BoxCollider2D trigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collider.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collider.enabled = true;
    }
}
