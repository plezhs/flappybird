using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class birdjump : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpPower;
    // Start is called before the first frame update
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

    private void OnCollisionEnter2D(Collision2D ohter)
    {
        if(Score.score > Score.BestScore)
        {
            Score.BestScore = Score.score; 
        }
        SceneManager.LoadScene(1);
    }
}
