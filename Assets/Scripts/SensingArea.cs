using UnityEngine;

/// <summary>
/// 물체가 나 자신을 통과하면 알린다.
/// 센싱의 조건: Collider의 isTrigger = ON
///              다른 물체에는 RigidBody(몸, 강체)
/// 1. 근접센서: 앞으로 지나가는 모든 물체를 감지
/// 2. 금속센서: 금속만 감지
/// </summary>
public class SensingArea : MonoBehaviour
{
    public bool isDetected = false;

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name + "센싱시작!");

        isDetected = true;

        if (other.tag == "금속")
            print(other.gameObject.name + "는 금속입니다!");
    }

    private void OnTriggerStay(Collider other)
    {
        print(other.gameObject.name + "센싱중~....");
    }

    private void OnTriggerExit(Collider other)
    {
        print(other.gameObject.name + "센싱종료!");

        isDetected = false;
    }
}
