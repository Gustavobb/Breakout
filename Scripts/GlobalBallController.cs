using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBallController : MonoBehaviour
{
    public GameObject ball1Go;
    public GameObject ball2Go;
    BallController ball1, ball2;
    public bool outOffBounds = false;
    public bool waited = false;
    public GameObject Block;
    public GameObject PlatformPu;

    void Start()
    {
        ball1 = ball1Go.GetComponent<BallController>();
        ball2 = ball2Go.GetComponent<BallController>();
    }

    void Update()
    {
        if (outOffBounds)
        {
            ball1.HandleOutOfBounds();
            ball2.HandleOutOfBounds();
            ball1.blockController.GetComponent<BlockControllerScript>().descend = true;
            ball1.ppscript.GetComponent<PPScript>().ActivateAnimation();
            outOffBounds = false;
        }

        if (waited)
        {
            ball1.blockController.GetComponent<BlockControllerScript>().SpawnBlock(1, GameObject.Find("BCParent/").transform, PlatformPu, Block);
            waited = false;
        }
    }
}
