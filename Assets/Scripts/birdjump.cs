using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class birdjump : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpPower;
    // Start is called before the first frame update
    public BirdAgent birdAgent;
    public pipespawner pipespawner;
    public GameController gameController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpPower;// (0,3)
        }
    }

    // private void OnCollisionEnter2D(Collision2D ohter)
    // {
    //     if(Score.score > Score.BestScore)
    //     {
    //         Score.BestScore = Score.score; 
    //     }
    //     SceneManager.LoadScene(1);
    // }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ResetEnvironment();
        // 파이프나 바닥에 충돌하면 에피소드 종료
        birdAgent.die();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("ScoreZone"))
        {
            Score.score+=1;
            birdAgent.score();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("ScoreZone"))
        {
            birdAgent.scoreext();
        }
    }

    public void ResetEnvironment()
    {

        pipespawner.ResetPipes();

        this.transform.localPosition = gameController.transform.localPosition;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.rotation = 0f;

        Score.score = 0;
    }
}
