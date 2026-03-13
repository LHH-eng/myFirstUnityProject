using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 충격을 받으면 알람을 울린다.
/// 충돌 확인의 조건: 확인을 하고자 하는 물체의 RigidBody + Collider
/// </summary>
public class WallScript : MonoBehaviour
{
    public int collisionCount = 10;

    // 이벤트 메서드: 이벤트가 있을 때 실행됨

    // 충돌되는 순간 확인하는 메서드
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.transform.name + "충돌 시작!");

        collisionCount--;

        if (collisionCount <= 0)
            Destroy(gameObject);

    }

    // 충돌중일 때 실행되는 메서드
    private void OnCollisionStay(Collision collision)
    {
        print(collision.transform.name + "감지중...");
    }

    // 충돌에서 벗어날때 실행되는 메서드
    private void OnCollisionExit(Collision collision)
    {
        print(collision.transform.name + "충돌 종료");
    }
}
