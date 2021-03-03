using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour
{
  GameManager gm;

  private void OnEnable()
  {
      gm = GameManager.GetInstance();
  }
 
  public void ReturnGame()
  {
      gm.ChangeState(GameManager.GameState.GAME);
  }

  public void RestartGame()
  {
      gm.ChangeState(GameManager.GameState.MENU);
  }
}