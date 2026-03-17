using System.Collections;
using UnityEngine;

/// <summary>
/// PLC의 출력신호(ex Y20)를 받아 솔레노이드에 신호가 들어온다.
/// 솔레노이드 신호에 따라 실린더 로드가 전진, 후진한다.
/// 속성: 후방LS(리미트스위치)의 PLC 신호, 전방LS(리미트스위치)의 PLC 신호, LS0의 SL1의 MeshRenderer
///       SOL0(솔레노이드신호)의 PLC신호, SOL1(솔레노이드신호)의 PLC신호
///       실린더 Rod의 Transform, 앞방향 maxPos, 뒷방향 minPos(이동 축의 최소,최대값)
///       실리더 이동속도(공압), return스피드(단동 솔레노이드에서만 사용)
///       단동, 복동 열거형
/// </summary>
public class Cylinder_MPS : MonoBehaviour
{
    public enum SolenoidType
    {
        단동형,
        복동형
    }

    [Header("PLC 신호들")]
    public bool backSignal_LS;
    public bool frontSignal_LS;
    public bool backSignal_SOL;
    public bool frontSignal_SOL;

    [Header("기타 설정들")]
    public SolenoidType solenoidType = SolenoidType.단동형;
    public Transform rod;
    public float maxPos;
    public float minPos;
    public float speed = 2f; // 공압 조절 밸브에 따른 속도
    public float returnSpeed = 3f; // 단동 솔레노이드의 복귀 속도
    public MeshRenderer mrBackLS;
    public MeshRenderer mrFrontLS;
    public bool isMoving;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveForwardBySignal());
        StartCoroutine(MoveBackwardBySignal());
    }

    // Update is called once per frame
    void Update()
    {
        // 키 입력으로 PLC Mock(Mockup_테스트제품) 신호주기
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            backSignal_LS = !backSignal_LS; // 버튼을 누를때마다 토글 - backSignal_LS
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            frontSignal_LS = !frontSignal_LS; // 버튼을 누를때마다 토글 - frontSignal_LS
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            backSignal_SOL = !backSignal_SOL; // 버튼을 누를때마다 토글 - backSignal_SOL
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            frontSignal_SOL = !frontSignal_SOL; // 버튼을 누를때마다 토글 - frontSignal_SOL

            if(frontSignal_SOL)
            {
                Vector3 dir = new Vector3(maxPos, rod.localPosition.y, rod.localPosition.z);
                StartCoroutine(MoveCylinder(dir));
            }
            else
            {
                Vector3 dir = new Vector3(minPos, rod.localPosition.y, rod.localPosition.z);
                StartCoroutine(MoveCylinder(dir));
            }
        }
    }

    // PLC 신호는 Logic에 의해 계속 켜져있음 -> 
    IEnumerator MoveCylinder(Vector3 to) // 벡터로 만들어준다음 넣어주기
    {
        if (!isMoving)
        {
            isMoving = true;

            // 앞방향으로
            while (true) 
            {
                Vector3 dir = to - rod.localPosition; // 방향 정하기
                float distance = dir.magnitude; // 거리1

                if (distance < 0.1f)
                {
                    isMoving = false;
                    mrFrontLS.material.color = new Color(1, 0, 0, 0.7f);
                    break;
                }

                rod.localPosition += dir.normalized * speed * Time.deltaTime; // 방향은 그대로 유지한 채 길이를 1로 만든 '단위 벡터'

                yield return null;
            }

        }
    }

    IEnumerator MoveForwardBySignal()
    {
        while (true)
        {
            if(solenoidType == SolenoidType.단동형)
            {
                yield return new WaitUntil(() => frontSignal_SOL); // true 안써도 true임
            }
            else
            {
                yield return new WaitUntil(() => frontSignal_SOL);
            }

            Vector3 dir = new Vector3(maxPos, rod.localPosition.y, rod.localPosition.z);
            yield return MoveCylinder(dir);
        }
    }
    IEnumerator MoveBackwardBySignal()
    {
        while (true)
        {
            if (solenoidType == SolenoidType.단동형)
            {
                yield return new WaitUntil(() => !frontSignal_SOL); // False 안써도 False임
            }
            else
            {
                yield return new WaitUntil(() => backSignal_SOL);
            }

            Vector3 dir = new Vector3(minPos, rod.localPosition.y, rod.localPosition.z);
            yield return MoveCylinder(dir);
        }
    }
}
