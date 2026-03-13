using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

/// <summary>
/// 1. Idle 상태 : 물체가 감지되지 않아 멈춰있는 상태
/// 2. Running 상태: 센서가 물체를 감지하여 컨베이어가 돌아가는 상태
/// 3. Stop 상태 : 작업자가 수동(스페이스키 누르면)으로 멈추거나 공정이 완료된 상태
/// 4. Error 상태 : 벨트에 이상이 있는 상태
/// </summary>
public class FSMCylinderSystem : MonoBehaviour
{
    enum State
    {
        Idle,
        Running,
        Stop,
        Error
    }
    State state = State.Idle;
    State lastState = State.Idle;
    public CylinderSensor sensor;
    public Transform pusher;
    public Transform target;
    public float speed;
    public bool isEmmergency;
    Vector3 pusherOrigin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("컨베이어를 작동시킵니다...");
        pusherOrigin = pusher.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                print("시스템은 Idle 상태입니다.");

                if (sensor.isDetected)
                {
                    state = State.Running;
                    lastState = State.Idle;
                }
                break;
            case State.Running:
                print("시스템은 Running 상태입니다.");

                // sensor.MovePusher();

                Vector3 dir = target.position - pusher.position;
                float distance = dir.magnitude;

                if (distance < 0.1f || isEmmergency)
                {
                    state = State.Stop;
                    lastState = State.Stop;
                }

                pusher.transform.position += dir.normalized * speed * Time.deltaTime;
                break;
            case State.Stop:
                print("시스템은 Stop 상태입니다.");

                if (!isEmmergency)
                {
                    state = lastState;
                }
                break;
            case State.Error:
                print("시스템은 Error 상태입니다.");

                break;
        }
    }
}
