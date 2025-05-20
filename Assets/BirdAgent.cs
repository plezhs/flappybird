using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class BirdAgent : Agent
{
    private GameController gameController;

    [SerializeField] private float jumpForce = 4f;

    private Rigidbody2D rb;

    // object-bird1 GameObject를 인스펙터에서 할당하거나, Start()에서 찾을 수 있습니다.
    public GameObject objectBird1;

    public override void Initialize()
    {
        if (objectBird1 != null)
        {
            rb = objectBird1.GetComponent<Rigidbody2D>();
        }
        else
        {
            Debug.LogError("objectBird1가 할당되지 않았습니다.");
        }
        gameController = FindObjectOfType<GameController>();
    }

    public override void OnEpisodeBegin()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector3(-1f, 0f, 0f);
        gameController.ResetEnvironment();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 에이전트의 y 위치와 y 속도
        sensor.AddObservation(transform.position.y);
        sensor.AddObservation(rb.velocity.y);

        // 가장 가까운 파이프의 위치 정보
        Vector2 nearestPipePos = gameController.GetNearestPipePosition(transform.position.x);
        sensor.AddObservation(nearestPipePos.x - transform.position.x); // x 거리
        sensor.AddObservation(nearestPipePos.y - transform.position.y); // y 거리
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0];
        if (action == 1)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        // 살아있는 동안 작은 보상
        AddReward(0.01f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        actionsOut.DiscreteActions.Array[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 파이프나 바닥에 충돌하면 에피소드 종료
        SetReward(-1f);
        EndEpisode();
        Debug.Log("lk;jalakjsfdjlfk;adlkjfwe");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 파이프 사이를 통과하면 보상
        AddReward(1f);
    }
}
