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
            gameController.setScoreUpYRange(0, 0);
            gameController.setScoreUpYRange(transform.position.y, transform.position.y + 2.513688f);
            // Debug.Log(transform.position.y + " | " + transform.position.y + 2.513688f);
            // Debug.Log(transform.parent.name + " | " + transform.name);
            // Debug.Log(transform.localPosition.y);
        }
    }
}
