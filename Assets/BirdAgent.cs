using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class BirdAgent : Agent
{
    private GameController gameController;

    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float pipeMinY = -1f;
    [SerializeField] private float pipeMaxY = 4.5f;

    private Rigidbody2D rb;

    private bool isJumping = false;

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
        transform.position = new Vector3(-1.95f, 0f, 0f);
        // pipePrefab.transform.position = new Vector3(4.01f, Random.Range(pipeMinY, pipeMaxY), 0f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 기존 관측 정보
        sensor.AddObservation(transform.position.y);
        sensor.AddObservation(rb.velocity.y);

        // 가장 가까운 파이프의 위치
        Vector2 nearestPipePos = gameController.GetNearestPipePosition(transform.position.x);
        sensor.AddObservation(nearestPipePos.x);
        sensor.AddObservation(nearestPipePos.y);

        // 점프 여부
        sensor.AddObservation(isJumping ? 1f : -1f);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0]; // 점프 여부 확인
        if (action == 1)    // 점프 버튼 눌렀으면
        {
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
        }else{
            isJumping = false;            
        }
        
        float agentY = objectBird1.transform.position.y; //새 높이 가져옴
        (float minY, float maxY) = gameController.getScoreUpYRange(); //점수 인정 부분 높이 범위 가져옴
        Debug.Log("minY: " + minY + " maxY: " + maxY);
        Debug.Log("agentY: " + agentY);
        
        if (agentY >= minY && agentY <= maxY) //새 높이가 점수 인정 부분 높이 범위 내에 있으면
        {
            AddReward(0.01f); // 범위 내에 있을 때 추가 보상
            Debug.Log("detected");
        }else{
            AddReward(-0.01f); // 범위 밖에 있을 때 보상 감소
            Debug.Log("not detected");
        }

        // 살아있는 동안 작은 보상
        AddReward(0.01f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        actionsOut.DiscreteActions.Array[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    public void die(){
        AddReward(-1f);
        gameController.ResetEnvironment();
        EndEpisode();
        // Debug.Log("aa!!!!");
    }

    public void score(){
        AddReward(1f);
        // Debug.Log("success!!!!");
    }
}