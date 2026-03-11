using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 3초 후에 적을 소환한다.
/// 속성: 스폰딜레이, 적의 프리펩
/// </summary>
public class Timer : MonoBehaviour
{
    public float spawnDelay = 3f;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab_SQ;
    public float time; // 시간 저장용 필드
    public Transform targetA;
    public Transform targetB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(SpawnEnemy()); // 코루틴 메서드 시작

        //StartCoroutine(CoroutineStudy());

        //StartCoroutine(FollowTarget(Vector3.right));

        StartCoroutine(MoveSequence());

    }

    // Update is called once per frame
    void Update()
    {
        // 3초에 한 번씩 실행됨
        //time += Time.deltaTime;

        //if(time > spawnDelay)
        //{
        //    GameObject go = Instantiate(enemyPrefab); // Instantiate: 물체 생성
        //    go.transform.SetParent(transform);      // 부모 설정
        //    // go.transform.position = Vector3.zero; // 부모의 위치로 초기화(부모 위치의 절대좌표 제로)
        //    go.transform.localPosition = Vector3.zero; // 부모의 위치로 초기화(부모좌표 제로)

        //    time = 0;
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartSequence());
        }
    }

    // 코루틴 메서드: 유니티에서 비동기 기능을 실행하는 메서드
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // while문 안에 넣어서 작동시키면 void Update()에 넣은 것과 동일하게 작동
            yield return new WaitForSeconds(spawnDelay);
            // yield return null; // 1프레임 기다리기

            print(spawnDelay);

            GameObject go = Instantiate(enemyPrefab); // Instantiate: 물체 생성
            go.transform.SetParent(transform);      // 부모 설정
            go.transform.localPosition = Vector3.zero; // 부모의 위치로 초기화(부모좌표 제로)

        }

    }

    IEnumerator CoroutineStudy()
    {
        yield return new WaitForSeconds(1);

        print("1초 지남");

        yield return new WaitForSeconds(3);

        print("4초 지남");
    }

    IEnumerator FollowTarget(Vector3 targetPos)
    {
        print("모터 1작동");

        yield return new WaitForSeconds(1);

        print("모터 2작동");

        yield return new WaitForSeconds(1);

        print("모터 3작동");

        yield return new WaitForSeconds(1);

        print($"그리퍼 작동 {targetPos}");

        // yield return new WaitUntil -> 나중에 써보는 거로

    }

    // 실습1. 스페이스 버튼을 누르면 아래의 시퀀스가 순서대로 실행되는 코루틴 메서드
    // SE1. 1초 후 0,0,0에서 물체를 생성
    // SE2. 1초 후 2,0,0에서 물체를 생성
    // SE3. 1초 후 0,2,0에서 물체를 생성
    // SE3. 1초 후 0,0,2에서 물체를 생성
    /*
     *     IEnumerator StartSequence() -> 내가 만든 코드
    {
        yield return new WaitForSeconds(1);

        GameObject go_SQ = Instantiate(enemyPrefab_SQ);
        go_SQ.transform.SetParent(transform);
        go_SQ.transform.localPosition = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(1);

        GameObject go_SQ1 = Instantiate(enemyPrefab_SQ);
        go_SQ1.transform.SetParent(transform);
        go_SQ1.transform.localPosition = new Vector3(2, 0, 0);

        yield return new WaitForSeconds(1);

        GameObject go_SQ2 = Instantiate(enemyPrefab_SQ);
        go_SQ2.transform.SetParent(transform);
        go_SQ2.transform.localPosition = new Vector3(0, 2, 0);

        yield return new WaitForSeconds(1);

        GameObject go_SQ3 = Instantiate(enemyPrefab_SQ);
        go_SQ3.transform.SetParent(transform);
        go_SQ3.transform.localPosition = new Vector3(0, 0, 2);

    }
     */


    IEnumerator StartSequence()
    {
        // SE1. 1초 후 0,0,0에서 물체를 생성
        yield return new WaitForSeconds(1);
        GameObject go_SQ = Instantiate(enemyPrefab_SQ, transform);
        go_SQ.transform.position = new Vector3(0, 0, 0);

        // SE2. 1초 후 2,0,0에서 물체를 생성
        yield return new WaitForSeconds(1);
        go_SQ = Instantiate(enemyPrefab_SQ, transform);
        go_SQ.transform.position = new Vector3(2, 0, 0);

        // SE3. 1초 후 0,2,0에서 물체를 생성
        yield return new WaitForSeconds(1);
        go_SQ = Instantiate(enemyPrefab_SQ, transform);
        go_SQ.transform.position = new Vector3(0, 2, 0);

        // SE3. 1초 후 0,0,2에서 물체를 생성
        yield return new WaitForSeconds(1);
        go_SQ = Instantiate(enemyPrefab_SQ, transform);
        go_SQ.transform.position = new Vector3(0, 0, 2);

    }

    // 실습2. Coroutine을 사용하여 이동 시퀀스 구성하기
    // 1초 후
    // TargetA로 이동 -> yield return null; // 1프레임 기다리기
    // 1초 후
    // TargetB로 이동
    // 2초 후
    // TargetA로 이동
    IEnumerator MoveSequence()
    {
        GameObject go = Instantiate(enemyPrefab);

        yield return new WaitForSeconds(1); // 1초 후

        yield return MoveToTargetA(go, targetA);
        // StartCoroutine(MoveToTargetA(go, targetA));

        yield return new WaitForSeconds(1); // 1초 후

        yield return MoveToTargetA(go, targetB);

        yield return new WaitForSeconds(2); // 1초 후

        yield return MoveToTargetA(go, targetA);

        yield return new WaitForSeconds(1);
    }

    IEnumerator MoveToTargetA(GameObject go, Transform target)
    {
        while (true)
        {
            Vector3 dir = target.position - go.transform.position;
            float distance = dir.magnitude;

            if (distance < 0.1f)
            {
                break;
            }

            go.transform.position += dir.normalized * 2 * Time.deltaTime;

            yield return null; // 1프레임 기다리기
        }
    }
}
