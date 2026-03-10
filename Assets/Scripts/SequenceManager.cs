using UnityEngine;

/// <summary>
/// Object를 Target 방향으로 이동시킨다.
/// 속성: Object의 transform, target의 transform, 속도
/// </summary>
public class SequenceManager : MonoBehaviour
{
    public Transform obj;
    public Transform targetA;
    public Transform[] targets; // 여러 배열을 담는 배열
    public float speed;
    Vector3 originPos;
    public bool istargerA;
    int option = 0; // 타켓들의 옵션
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originPos = obj.position;
        animator = obj.GetComponent<Animator>(); // 내 GameObject의 컴포넌트 가져오기

        animator.SetInteger("AnimationID", 4);
    }

    // Update is called once per frame
    void Update()
    {
        //if (istargerA)
        //    MoveObjectToTarget(targetA.position);

        //else
        //    MoveObjectToTarget(originPos);

            MoveObjectToTarget(targets[option].position);       

    }

    private void MoveObjectToTarget(Vector3 targetPos)
    {
        // Object를 Target 방향으로 이동시킨다.
        // 벡터(vector): 방향과 크기를 가진 값( ex) 속도 )
        // 스칼라(Scalar): 크기만 가진 값( ex) 속력 )
        Vector3 dir = targetPos - obj.position;

        Vector3 xyDir = new Vector3(dir.x, 0, dir.z);
        float distance = xyDir.magnitude; // 거리: 벡터의 크기

        if (distance < 0.1f)
        {
            // istargerA = false;
            istargerA = !istargerA; // Toggle: bool은 toggle 가능 true <-> flase

            option++;

            if (option >= targets.Length)
            {
                option = targets.Length - 1; // 마지막 인덱스로 고정

                //animator.SetInteger("AnimationID", 7); // 바로 앉기
                Invoke("StartAnimation", 2.0f); // 2초 후에 앉기 (몇 초 후에 실행)
                InvokeRepeating("CreateBox", 3, 1); // 3초 후 1초마다 함수를 반복

            }

            return;

        }

        obj.forward = xyDir.normalized; // 진행 방향의 크기가 1인 벡터를 앞으로 바꾸기

        // obj.position = obj.position + dir.normalized * speed * Time.deltaTime; -> 줄여서 아래와 같이 씀
        obj.position += xyDir.normalized * speed * Time.deltaTime;
    }

    void StartAnimation()
    {
        animator.SetInteger("AnimationID", 7);
    }

    void CreateBox() // 오브젝트 생성 함수
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.AddComponent<Rigidbody>();
    }

}
