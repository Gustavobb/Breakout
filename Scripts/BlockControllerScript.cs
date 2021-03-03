using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControllerScript : MonoBehaviour
{
    #region Public Variables
    public GameObject Block;
    public GameObject PlatformPU;
    public bool descend;
    public int points = 0;
    #endregion

    #region Variables
    #endregion

    #region Components
    public GameManager gm;
    public GameObject endGameCollider;
    #endregion

    #region Unity Functions
    void Start() 
    {
        descend = false;
        gm = GameManager.GetInstance();
        GameManager.changeStateDelegate += SpawnBlockStart;
    } 

    void Update()
    {
        HandleEndgame();
        if (gm.gameState == GameManager.GameState.MENU) DestroyChilds();
    }
    #endregion

    #region Handlers
    public void HandleEndgame()
    {
        if (endGameCollider.GetComponent<EndGameCollider>().entered) Reset();
    }
    #endregion

    #region Other Functions
    void Reset() 
    {
        gm.ChangeState(GameManager.GameState.ENDGAME);
        descend = false;
        endGameCollider.GetComponent<EndGameCollider>().entered = false;
        DestroyChilds();
    }

    void DestroyChilds()
    {
        foreach (Transform child in transform.parent)
        {
            if (child != transform)
                GameObject.Destroy(child.gameObject);
        }
    }
    #endregion

    #region Spawner
    public void SpawnBlock(int rows, Transform transform, GameObject PUPlat, GameObject block)
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < rows; j++) {
                Vector3 position = new Vector3(-5.5f + 1f * i, (3.75f + .5f) - .5f * j);

                int spawnChance = Random.Range(0, 3);
                if (spawnChance > 0)
                {
                    Instantiate(block, position, Quaternion.identity, transform);
                }
                else
                {
                    int spawnChancePUPlatform = Random.Range(0, 4);
                    if (spawnChancePUPlatform == 0) Instantiate(PUPlat, position, Quaternion.identity, transform);
                }
            }
        }
    }

    public void SpawnBlockStart()
    {
        if (gm.gameState == GameManager.GameState.GAME && gm.oldGameState != GameManager.GameState.PAUSE)
            SpawnBlock(8, transform.parent, PlatformPU, Block);
    }
    #endregion
}
