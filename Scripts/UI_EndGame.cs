using UnityEngine;
using UnityEngine.UI;

public class UI_EndGame : MonoBehaviour
{
    public Text message;

    GameManager gm;

    void OnEnable()
    {
        gm = GameManager.GetInstance();
    }

    public void GoBack()
    {
        gm.ChangeState(GameManager.GameState.MENU);
    }
}