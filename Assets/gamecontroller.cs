using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // private Rigidbody2D rb;
    // [SerializeField] private GameObject bird1;
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private Transform pipeSpawnPoint;
    [SerializeField] private float pipeSpawnInterval = 2f;
    [SerializeField] private float pipeMinY = -1f;
    [SerializeField] private float pipeMaxY = 4.5f;

    private List<GameObject> pipes = new List<GameObject>();
    private float timer = 0f;

    [SerializeField] private GameObject bird1;
    private Vector3 birdStartPos;

    private void Start()
    {
        birdStartPos = bird1.transform.position;
    }

    // rb = objectBird1.GetComponent<Rigidbody2D>();

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= pipeSpawnInterval)
        {
            SpawnPipe();
            timer = 0f;
        }
    }

    private void SpawnPipe()
    {
        float yPos = Random.Range(pipeMinY, pipeMaxY);
        GameObject pipe = Instantiate(pipePrefab, new Vector3(pipeSpawnPoint.position.x, yPos, 0f), Quaternion.identity);
        pipes.Add(pipe);
    }

    public void ResetEnvironment()
    {
        // 기존 파이프 제거
        foreach (GameObject pipe in pipes)
        {
            Destroy(pipe);
            Debug.Log("pipedie");
        }
        pipes.Clear();
        timer = 0f;

        // bird1 위치 및 속도 초기화
        bird1.transform.position = birdStartPos;
        Rigidbody2D rb = bird1.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        Score.score = 0;

    }

    public Vector2 GetNearestPipePosition(float agentX)
    {
        Vector2 nearestPos = Vector2.zero;
        float minDistance = float.MaxValue;

        foreach (GameObject pipe in pipes)
        {
            float distance = pipe.transform.position.x - agentX;
            if (distance > 0 && distance < minDistance)
            {
                minDistance = distance;
                nearestPos = pipe.transform.position;
            }
        }

        return nearestPos;
    }
}
