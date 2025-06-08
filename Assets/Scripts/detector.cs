using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detector : MonoBehaviour
{
    public BirdAgent birdAgent;
    public GameController gameController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("ScoreZone")){
            Transform par = other.transform.parent;
            Transform sq = par.transform.Find("square");
            gameController.setScoreUpYRange(sq.transform.position.y, sq.transform.position.y + 2.513688f);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("ScoreZone"))
        {
            gameController.setpibotX(other.transform.position.x);
        }
    }
}
