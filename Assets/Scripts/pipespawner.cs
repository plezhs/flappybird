using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipespawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float pipeMinY = -1f;
    public float pipeMaxY = 4.5f;
    public float pipeSpawnInterval = 3.5f;
    public GameObject spawnPipeX;
    private List<GameObject> pipes = new List<GameObject>();
    private float timer = 0f;
    private bool isSpawned = false;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSpawned){
            SpawnPipe();
            // GameObject sq = pipes[pipes.Count - 1].transform.Find("square").gameObject;
            // gameController.setScoreUpYRange(sq.transform.position.y, sq.transform.position.y + 2.513688f);
            isSpawned = true;
        }else{
            timer += Time.deltaTime;
            if (timer >= pipeSpawnInterval)
            {
                SpawnPipe();
                timer = 0f;
            }
        }
    }

    private void SpawnPipe()
    {
        float yPos = Random.Range(pipeMinY, pipeMaxY) + spawnPipeX.transform.position.y;
        float xPos = spawnPipeX.transform.position.x - 3.5f;
        GameObject pipe = Instantiate(pipePrefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
        pipe.transform.parent = this.transform;
        pipes.Add(pipe);
    }

    public void ResetPipes()
    {
        // Reset the environment here, including any pipes or obstacles
        for (int i = pipes.Count - 1; i >= 0; i--)
        {
            var pipe = pipes[i];
            pipes.RemoveAt(i);
            Destroy(pipe.gameObject);
        }
        timer = 0f;
        isSpawned = false;
    }

}
