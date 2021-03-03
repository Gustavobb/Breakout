using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    #region Public variables
    [Range(1, 30)]
    public float speed;
    public int nBall = 1;
    #endregion

    #region Variables
    Vector3 velocity;
    int inputX;
    float targetVelocityX;
    float velocityXSmoothing;
    float accelerationTime = .1f;
    #endregion

    #region Components
    GameManager gm;
    public PlayerController playerController;
    #endregion

    #region Unity functions
    void Start()
    {
        gm = GameManager.GetInstance();
    }

    void Update()
    {
        if (gm.gameState == GameManager.GameState.PAUSE) return;
        else if (gm.gameState == GameManager.GameState.ENDGAME)
        {
            transform.position = new Vector3(0, -3.6f, 0);
            velocity = new Vector3(0, 0, 0);
            targetVelocityX = 0;
            return;
        }
        HandleInput();
        HandlePause();
        Move();
    }
    #endregion

    #region Handlers
    void HandleInput()
    {
        int left = Input.GetKey("left") ? 1 : 0;
        int right = Input.GetKey("right") ? 1 : 0;
        inputX = 1 * right + left * -1;
    }

    void HandlePause() 
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME) 
            gm.ChangeState(GameManager.GameState.PAUSE);
    }
    #endregion

    #region Triggers
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "BoundrieNormalL" || other.tag == "BoundrieNormalR")
        {
            float scale = other.tag == "BoundrieNormalL" ? transform.localScale.x : -transform.localScale.x;
            if (playerController.DuplicatePlayer())
                Instantiate(gameObject, new Vector3(-transform.position.x + scale, transform.position.y, transform.position.z), Quaternion.identity);
        }
        else if (other.tag == "MaxPosition")
        {
            playerController.DestroyPlayer();
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
    }
    #endregion

    #region Other Functions
    void Move()
    {
        targetVelocityX = inputX * speed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTime);
        transform.Translate(velocity * Time.deltaTime);
    }
    #endregion
}
