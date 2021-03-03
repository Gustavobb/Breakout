using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    #region Public Variables
    [Range(0, 15)]
    public float velocity = 10.0f;
    #endregion

    #region Variables
    Vector3 direction;
    float dirX;
    public bool reset;
    bool waited;
    float dirY;
    #endregion

    #region Components
    GameManager gm;
    GlobalBallController globalBallController;
    public SpriteRenderer lineRenderer;
    public GameObject arrowObject;
    public GameObject blockController;
    public GameObject ppscript;
    public GameObject globalBallControllerGo;
    #endregion

    #region Unity Functions
    void Start() 
    {
        globalBallController = globalBallControllerGo.GetComponent<GlobalBallController>();
        waited = true;
        reset = false;
        lineRenderer.enabled = false;
        arrowObject.GetComponent<SpriteRenderer>().enabled = false;
        gm = GameManager.GetInstance();
    }
    
    void Update()
    {
        if (gm.gameState == GameManager.GameState.PAUSE) return;
        else if (gm.gameState != GameManager.GameState.GAME) 
        {
            ResetAll();
            return;
        }

        if (reset && waited)
        {
            arrowObject.GetComponent<SpriteRenderer>().enabled = true;
            lineRenderer.enabled = true;

            if (Input.GetKey("space")) 
            {
                lineRenderer.enabled = false;
                arrowObject.GetComponent<SpriteRenderer>().enabled = false;
                reset = false;
                waited = false;
                direction = new Vector3(arrowObject.transform.position.x - transform.position.x, arrowObject.transform.position.y - transform.position.y).normalized;
            }
        }

        transform.Translate(direction * Time.deltaTime * velocity);
    }
    #endregion

    #region Handlers
    void HandleRandomMovement()
    {
        dirX = Random.Range(-5.0f, 5.0f);
        dirY = Random.Range(1.0f, 5.0f);
        direction = new Vector3(dirX, dirY).normalized;
    }

    public void HandleOutOfBounds()
    {
        reset = true;
        GameObject.FindGameObjectWithTag("Platform").transform.localScale = new Vector3(2f, .2f, 1);
        StartCoroutine(Wait(2f));
    }
    #endregion

    #region Triggers
    void OnTriggerEnter2D(Collider2D col) 
    {
        bool play = true;
        if (col.tag == "BoundrieForbidden") globalBallController.outOffBounds = true;
        else if (col.tag == "BoundrieNormal") direction = new Vector3(direction.x, -direction.y);
        else if (col.tag == "BoundrieNormalL" || col.tag == "BoundrieNormalR") direction = new Vector3(-direction.x, direction.y);
        else if (col.tag == "BlockLeftRight") 
        {
            direction = new Vector3(-direction.x, direction.y);
            gm.points ++;
        }
        else if (col.tag == "BlockUpDown") 
        {
            direction = new Vector3(direction.x, -direction.y);
            gm.points ++;
        }
        else if (col.tag == "Platform") direction = new Vector3(Random.Range(-1f, 1f) + direction.x, -direction.y).normalized;
        else if (col.tag == "PlatformPU") 
        {
            if (col.gameObject != null) Destroy(col.gameObject);
            if (GameObject.FindGameObjectWithTag("Platform").transform.localScale.x < 4.5f)
                GameObject.FindGameObjectWithTag("Platform").transform.localScale = new Vector3(GameObject.FindGameObjectWithTag("Platform").transform.localScale.x + .5f, .2f, 1);
        }
        else play = false;

        if (play) GetComponent<AudioSource>().Play();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "End" && !reset) 
        {
            StartCoroutine(Wait(1f));
        }
    }
    #endregion

    #region Other Functions
    void ResetAll()
    {
        reset = false;
        waited = true;
        if (gm.gameState == GameManager.GameState.MENU) 
            gm.points = 0;
        transform.position = new Vector3(0, GameObject.FindGameObjectWithTag("Platform").transform.position.y + .5f, 0);
        direction = new Vector3(0, 0, 0);
    }
    #endregion

   #region Coroutine
   IEnumerator Wait(float waitTime)
   {
        transform.position = new Vector3(0, GameObject.FindGameObjectWithTag("Platform").transform.position.y + .5f, 0);
        direction = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(waitTime);
        if (reset && gm.gameState != GameManager.GameState.ENDGAME) 
        {
            globalBallController.waited = true;
            waited = true;
        } else HandleRandomMovement();
   }
   #endregion
}
