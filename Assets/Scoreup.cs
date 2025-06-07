using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Scoreup : MonoBehaviour
{
    public BirdAgent birdAgent;
    public GameController gameController;
    public GameObject square;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bird"))
        {
            Score.score+=1;
            birdAgent.score();
        }else if(other.CompareTag("det"))
        {
            gameController.setScoreUpYRange(square.transform.position.y, square.transform.position.y + 2.513688f);
            // Debug.Log(transform.position.y + " | " + transform.position.y + 2.513688f);
            // Debug.Log(transform.parent.name + " | " + transform.name);
            // Debug.Log(transform.localPosition.y);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("det"))
        {
            gameController.setpibotX(square.transform.position.x);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Bird"))
        {
            birdAgent.scoreext();
        }
    }
}
