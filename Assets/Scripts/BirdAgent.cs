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
        // pipePrefab.transform.position = new Vector3(4.01f, Random.Range(pipeMinY, pipeMaxY), 0f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 기존 관측 정보
        sensor.AddObservation(transform.localPosition.y);
        sensor.AddObservation(rb.velocity.y);
        
        // 점수 인정 부분 높이 범위
        float minY, maxY, pibotX;
        (minY, maxY, pibotX) = gameController.getScoreUpYRange();
        float avgY = (minY + maxY) / 2;
        //높이차이
        sensor.AddObservation(avgY-objectBird1.transform.position.y);
        //파이프와의 거리
        sensor.AddObservation(pibotX-objectBird1.transform.position.x);
        // 점프 여부
        // sensor.AddObservation(isJumping ? 1f : -1f);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0]; // 점프 여부 확인
        if (action == 1)    // 점프 버튼 눌렀으면
        {
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            AddReward(-0.005f);
        }else{
            isJumping = false;
        }
        
        float agentY = objectBird1.transform.position.y; //새 높이 가져옴
        (float minY, float maxY, float pibotX) = gameController.getScoreUpYRange(); //점수 인정 부분 높이 범위 가져옴
        // Debug.Log("minY: " + minY + " maxY: " + maxY);
        // Debug.Log("agentY: " + agentY);
        
        if (agentY >= minY && agentY <= maxY) //새 높이가 점수 인정 부분 높이 범위 내에 있으면
        {
            AddReward(0.05f); // 범위 내에 있을 때 추가 보상
            Debug.Log("detected | " + objectBird1.transform.parent.name);
        }else{
            AddReward(-0.05f); // 범위 밖에 있을 때 보상 감소
            Debug.Log("not detected | " + objectBird1.transform.parent.name);
        }

        // 살아있는 동안 작은 보상
        // AddReward(0.001f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        actionsOut.DiscreteActions.Array[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    public void die(){
        AddReward(-1.0f);
        EndEpisode();
        // Debug.Log("aa!!!!");
    }

    public void score(){
        AddReward(1.0f);
        // Debug.Log("success!!!!");
    }
}