using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class BirdAgent : Agent
{
    [SerializeField] Replay Replay;
    public Rigidbody2D rb;
    public float flapForce = 5f;
    public Transform pipe;
    public float aliveReward = 0.1f;
    public float maxY = 5f;
    public float minY = -5f;

    private Vector2 startPos;

    public override void Initialize()
    {
        startPos = rb.position;
    }

    public override void OnEpisodeBegin()
    {
        // 새 위치 초기화
        rb.velocity = Vector2.zero;
        rb.position = startPos;

        // 파이프 위치 초기화 (간단 예시)
        if (pipe != null)
        {
            pipe.position = new Vector3(5f, Random.Range(-2f, 2f), 0f);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 새의 y 위치
        sensor.AddObservation(rb.position.y);
        // 새의 y 속도
        sensor.AddObservation(rb.velocity.y);

        if (pipe != null)
        {
            // 파이프와의 x 거리
            sensor.AddObservation(pipe.position.x - rb.position.x);
            // 파이프의 y 위치
            sensor.AddObservation(pipe.position.y);
        }
        else
        {
            // 파이프가 없을 때를 대비한 더미 값
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int flap = actions.DiscreteActions[0];
        if (flap == 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // y속도 초기화
            rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
        }

        // 살아있는 동안 보상
        AddReward(aliveReward * Time.fixedDeltaTime);

        // 화면 밖으로 나가면 에피소드 종료
        if (rb.position.y > maxY || rb.position.y < minY)
        {
            AddReward(-1f);
            Replay.ReplayGame();
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pipe") || collision.CompareTag("Ground"))
        {
            AddReward(-1f);
            EndEpisode();
        }
        else if (collision.CompareTag("ScoreZone"))
        {
            AddReward(1f);
        }
    }
}
