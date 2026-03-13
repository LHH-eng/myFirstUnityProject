using UnityEngine;

/// <summary>
/// NPC의 상태에 따라 NPC를 이동시킨다.
/// 속성: 상태 enum[Idle(대기상태), Walk, Run, Stop]
/// Run : 플레이어와의 거리가 5~10m 사이 Run
/// Walk : 플레이어와의 거리 5m 이내 Walk
/// Stop : SensingArea에 들어오면 멈춘다
/// Idle : SensingArea에
/// </summary>
public class FSMmanager : MonoBehaviour
{
    enum State
    {
        Idle,
        Walk,
        Run,
        Stop
    }

    State currentState = State.Idle;
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public Transform player;
    public DangerZone dangerZone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                print("상태 - Idle");

                // Player가 NPC와의 1m 거리 이내로 들어왔을 때
                Vector3 dir = player.position - transform.position;
                float distance = dir.magnitude;

                if(distance < 5 && distance > 1)
                {
                    currentState = State.Walk;
                }
                else if(distance > 5 && distance < 10)
                {
                    currentState = State.Run;
                }


                // DangerZone에 들어왔을 때
                if (dangerZone.isDetected == true)
                    currentState = State.Stop;

                break;

            case State.Walk:
                print("상태 - Walk");

                dir = player.position - transform.position;
                distance = dir.magnitude;

                if(distance > 5 && distance < 10)
                {
                    currentState = State.Run;
                }
                else if(distance > 10)
                {
                    currentState = State.Idle;
                }
                else if (distance > 1f)
                {
                    currentState = State.Idle;
                }

                transform.position += dir * walkSpeed * Time.deltaTime;

                // DangerZone에 들어왔을 때
                if (dangerZone.isDetected == true)
                    currentState = State.Stop;

                // 긴급 경보를 확인했을 때
                //currentState = State.Run;

                // 다른 물체에 부딪혔을 때
                //currentState = State.Stop;
                break;

            case State.Run:
                print("상태 - Run");

                dir = player.position - transform.position;
                distance = dir.magnitude;

                if (distance < 5)
                {
                    currentState = State.Walk;
                }
                else if (distance > 10)
                {
                    currentState = State.Idle;
                }

                transform.position += dir * runSpeed * Time.deltaTime;

                // DangerZone에 들어왔을 때
                if (dangerZone.isDetected == true)
                    currentState = State.Stop;

                // 다른 물체에 부딪혔을 때
                //currentState = State.Stop;
                break;

            case State.Stop:
                print("상태 - Stop");

                // Player가 NPC에 가까이 다가갔을 때
                //currentState = State.Idle;
                break;
        }
    }
}
