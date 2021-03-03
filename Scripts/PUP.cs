using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUP : MonoBehaviour
{
    public GameObject blockController;
    Vector3 target;
    bool wasDescending = false;

    void Start() 
    {
        target = transform.position + new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        if (blockController.GetComponent<BlockControllerScript>().descend)
        {
            wasDescending = true;
            transform.position = Vector3.MoveTowards(transform.position, target, .5f * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.001f)
                blockController.GetComponent<BlockControllerScript>().descend = false;
        } 
        
        else if (wasDescending)
        {
            target = transform.position + new Vector3(0, -0.5f, 0);
            wasDescending = false;
        }
    }
}
