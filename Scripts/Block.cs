using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    #region Variables
    Vector3 target;
    bool wasDescending = false;
    public int randomLife;
    #endregion
    
    #region Components
    public GameObject blockController;
    public GameObject platform;
    public GameObject nText;
    public GameObject cameraShake;
    #endregion

    #region Unity functions
    void Start() 
    {
        target = transform.position + new Vector3(0, -0.5f, 0);
        int min = platform.GetComponent<PlatformController>().nBall;
        int max = platform.GetComponent<PlatformController>().nBall * 6;
        max = max > 255 ? 255 : max;

        randomLife = Random.Range(min, max);
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), 1f);
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
            //blockController.GetComponent<BlockControllerScript>().SpawnBlock(1);
            target = transform.position + new Vector3(0, -0.5f, 0);
            wasDescending = false;
        }
    }
    #endregion

    #region Other functions
    #endregion

    #region Triggers
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            randomLife --;
            GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), 1f);

            if (randomLife == 0)
            {
                Destroy(gameObject);
            }
        }
    }
    #endregion
}
