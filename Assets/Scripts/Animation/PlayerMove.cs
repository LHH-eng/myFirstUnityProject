using UnityEngine;

namespace AnimationStudy
{

    // 라이프 사이클(life cycle) 메서드: 스크립트를 가지고 있는 게임 오브젝트가 객체화
    // 되었을 때, 순차적으로 실행되는 메서드들

    /// <summary>
    /// 사용자 키보드 입력을 받아 플레이어를 앞뒤좌우 이동시킨다.
    /// 속성: 스피드, 애니메이션 컨트롤러
    /// </summary>
    public class PlayerMove : MonoBehaviour
    {
        // Awake 라고 하고 엔터 누르면 아래와 같이 나옴
        //private void Awake()
        //{

        //}

        enum State
        {
            Idle,
            Walk
        }

        State state = State.Idle;

        public float speed = 2f; // public으로 하면 인스펙터창에 보임
        Animator animator;
        Vector3 prevPos,currPos;
        float time;

        // 시작시 한번만 실행
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            print("시작!");

            animator = GetComponent<Animator>();
        }

        // 한 프레임에 한번씩 실행(계속 반복)
        // Update is called once per frame
        void Update()
        {
            switch (state)
            {
                case State.Idle:
                    animator.SetTrigger("Idle");
                    break;
                case State.Walk:
                    animator.SetTrigger("Walk");
                    break;
            }


            //// 사용자 키보드 입력을 받아 플레이어를 앞뒤좌우 이동시킨다.
            //if (Input.GetKey(KeyCode.W)) // GetKeyDown - 버튼 누를때 이동(dir), GetKey - 버튼 누른상태일때 이동(dir * speed_속도 조절됨)
            //{
            //    // Vector3 dir = Vector3.forward; // 월드좌표(절대좌표) 기준의 앞방향
            //    Vector3 dir = transform.forward; // 로컬좌표 기준의 앞방향 (0,0,1)

            //    transform.position = transform.position + dir * speed * Time.deltaTime;

            //    state = State.Walk;

            //    currPos = transform.position;
            //}

            //if (Input.GetKey(KeyCode.S)) // else if를 if로 변경하면 대각선으로 이동가능
            //{
            //    //Vector3 dir = -Vector3.forward;
            //    Vector3 dir = -transform.forward; // (0,0,-1)

            //    transform.position = transform.position + dir * speed * Time.deltaTime;

            //    state = State.Walk;

            //    currPos = transform.position;
            //}

            //if (Input.GetKey(KeyCode.A))
            //{
            //    //Vector3 dir = -Vector3.right;
            //    Vector3 dir = -transform.right;

            //    transform.position = transform.position + dir * speed * Time.deltaTime;

            //    state = State.Walk;

            //    currPos = transform.position;
            //}

            //if (Input.GetKey(KeyCode.D))
            //{
            //    //Vector3 dir = Vector3.right;
            //    Vector3 dir = transform.right;

            //    transform.position = transform.position + dir * speed * Time.deltaTime;

            //    state = State.Walk;

            //    currPos = transform.position;
            //}

            float x = Input.GetAxis("Horizontal"); // -1~1
            float z = Input.GetAxis("Vertical"); // -1~1
            Vector3 dir = new Vector3(x, 0, z).normalized;

            print(dir.magnitude);

            if (dir.magnitude > 0) // 방향 벡터가 키입력으로 만들어지면
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Idle", false);
            }
            else
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", true);
            }

            transform.position = transform.position + dir * speed * Time.deltaTime;


        }
    }
}
