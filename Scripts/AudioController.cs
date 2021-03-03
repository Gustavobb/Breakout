using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource engineStartClip;
    public AudioSource engineLoopClip;
    void Start()
    {
        StartCoroutine(playEngineSound());
    }

    IEnumerator playEngineSound()
    {
        yield return new WaitForSeconds(.5f);
        engineStartClip.Play();
        yield return new WaitForSeconds(1.4f);
        engineLoopClip.Play();
    }
}