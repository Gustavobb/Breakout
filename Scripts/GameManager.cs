using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
   public enum GameState { MENU, GAME, PAUSE, ENDGAME };
   public GameState gameState { get; private set; }
   public GameState oldGameState { get; private set; }
   public delegate void ChangeStateDelegate();
   public static ChangeStateDelegate changeStateDelegate;
   public int points;
   
   static GameManager _instance;

   public static GameManager GetInstance()
   {
       if(_instance == null) _instance = new GameManager();

       return _instance;
   }

   GameManager()
   {
       points = 0;
       gameState = GameState.MENU;
   }
    
   public void ChangeState(GameState nextState)
   {
       GameObject.FindGameObjectsWithTag("MainCamera") [0].GetComponent<CameraShake>().ShakeCR(.15f, .1f);
       if (nextState == GameState.GAME && gameState != GameState.PAUSE) Reset();
       oldGameState = gameState;
       gameState = nextState;
       changeStateDelegate();
   }

   private void Reset()
   {
       points = 0;
   } 
}
