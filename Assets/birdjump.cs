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
        // 파이프나 바닥에 충돌하면 에피소드 종료
        birdAgent.die();
    }
}
