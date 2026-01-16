using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipedie : MonoBehaviour
{
    [SerializeField] private BirdAgentRay birdAgent;

    // Start is called before the first frame update
    void Start()
    {
        Score.RegisterAgent(birdAgent.transform.parent.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log("Collision detected | " + other.gameObject.name);
        if(other.gameObject.CompareTag("sq"))
        {
            birdAgent.score();
            Score.AddScore(birdAgent.transform.parent.name, 1);
            if (Score.BestScore < Score.getScore(birdAgent.transform.parent.name)){
                Score.setBestScore(birdAgent.transform.parent.name);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // Debug.Log("Collision exit | " + other.gameObject.name);
        if(other.gameObject.CompareTag("Pipe"))
        {
            other.gameObject.transform.parent.GetComponent<Move>().des();
        }
    }
}
