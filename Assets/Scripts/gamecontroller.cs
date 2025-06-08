using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float scoreZoneMinY;
    private float scoreZoneMaxY;
    private float scoreZonePibotX;
    [SerializeField] private GameObject bird1;
    private Vector3 birdStartPos;
    public pipespawner pipespawner;

    private void Start()
    {
        birdStartPos = bird1.transform.position;
        rb = bird1.GetComponent<Rigidbody2D>();
    }

    // rb = objectBird1.GetComponent<Rigidbody2D>();

    private void Update()
    {
        
    }
    
    public (float, float, float) getScoreUpYRange()
    {
        return (scoreZoneMinY, scoreZoneMaxY, scoreZonePibotX);
    }
    public void setScoreUpYRange(float minY, float maxY)
    {
        scoreZoneMinY = minY;
        scoreZoneMaxY = maxY;
    }
    public void setpibotX(float pibotX)
    {
        scoreZonePibotX = pibotX;
    }
}
