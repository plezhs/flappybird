using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipedie : MonoBehaviour
{
    [SerializeField] private BirdAgentRay birdAgent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision detected | " + other.gameObject.name);
        if(other.gameObject.CompareTag("sq"))
        {
            birdAgent.score();
            Score.score++;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Collision exit | " + other.gameObject.name);
        if(other.gameObject.CompareTag("Pipe"))
        {
            other.gameObject.transform.parent.GetComponent<Move>().des();
        }
    }
}
