using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPScript : MonoBehaviour
{
    public void ActivateAnimation()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<Animator>().SetTrigger("BallFall");
    }
}
