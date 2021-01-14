using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrap : MonoBehaviour
{
    public bool _trapIsActive;
    [SerializeField] private bool selfDestructible;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player" && selfDestructible){
            col.gameObject.GetComponent<PlayerController>().KillPlayer();
            Destroy(gameObject);
        }else if (col.gameObject.name == "Player" && !selfDestructible) {
            col.gameObject.GetComponent<PlayerController>().KillPlayer();
        }
        
        if(col.gameObject.tag == "Ground" && selfDestructible)
            Destroy(gameObject);

    }
}
