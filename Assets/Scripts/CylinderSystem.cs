using UnityEngine;
using UnityEngine.InputSystem;

public class CylinderSystem : MonoBehaviour
{
    public Transform prefab;
    public Transform sensingAreaAPos;    // SensingArea A를 Inspector에서 드래그 연결
    public float spawnHeight = 2f;    // SensingArea A 위로 얼마나 높이 생성할지
    public Transform pusher;          // 보라색 큐브
    public SensingArea sensingAreaA;  // Inspector에서 SensingArea A 연결

    private float t = 0f;             // Lerp 진행도 (0~1)
    public float moveDistance = 3f;   // 푸셔 이동 거리
    public float pushSpeed = 2f;      // 푸셔 이동 속도
    private float waitTimer = 0f;     // 대기 타이머
    public float waitTime = 3f;       // 대기 시간

    private Vector3 startPos;         // 푸셔 시작 위치
    private Vector3 endPos;           // 푸셔 목표 위치

    void Start()
    {
        // 게임 시작시 푸셔 시작위치와 목표위치 고정
        startPos = pusher.position;
        endPos = startPos + Vector3.forward * moveDistance;
    }

    void Update()
    {
        // 스페이스키 누르면 프리펩 생성
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transform objectA = Instantiate(prefab);
            // SensingArea A 위치에서 spawnHeight만큼 위에 생성
            objectA.position = sensingAreaAPos.position + Vector3.up * spawnHeight;
        }

        // 물체가 감지되는 동안 대기 타이머 증가
        if (sensingAreaA.isDetected)
        {
            waitTimer += Time.deltaTime;
        }

        // 대기 시간이 지나면 푸셔 이동 시작
        if (waitTimer >= waitTime)
        {
            // t를 pushSpeed 속도로 증가 (1/pushSpeed = 이동 완료까지 걸리는 시간)
            t += Time.deltaTime * pushSpeed;
            // t를 0~1 사이로 제한
            t = Mathf.Clamp01(t);
            // startPos ~ endPos 사이를 t값에 따라 이동
            pusher.position = Vector3.Lerp(startPos, endPos, t);

            // 이동 완료시 초기화
            if (t >= 1f)
            {
                waitTimer = 0f;  // 대기 타이머 초기화
                t = 0f;          // Lerp 진행도 초기화
                Debug.Log("푸셔 이동 완료!");
            }
        }
    }
}