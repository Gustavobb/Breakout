using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCollider : MonoBehaviour
{
    public bool entered = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "BlockUpDown") entered = true;
    }
}
