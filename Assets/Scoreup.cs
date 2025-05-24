using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Scoreup : MonoBehaviour
{
    public BirdAgent birdAgent;
    public GameController gameController;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bird"))
        {
            Score.score+=1;
            birdAgent.score();
        }else if(other.CompareTag("det"))
        {
            gameController.setScoreUpYRange(other.transform.position.y+1f, other.transform.position.y + 1.513688f);
            Debug.Log("collided");
        }
    }
}
