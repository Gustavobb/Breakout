using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Menu : MonoBehaviour
{
    GameManager gm;
    // new
    public GameObject ball1;
    public GameObject ball2;

    void OnEnable()
    {
        gm = GameManager.GetInstance();
    }

    public void StartGame()
    {
        ball1.GetComponent<BallController>().reset = true;
        ball2.GetComponent<BallController>().reset = true;
        gm.ChangeState(GameManager.GameState.GAME);
    }
}