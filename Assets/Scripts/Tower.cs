using UnityEngine;

/// <summary>
/// 스페이스 버튼을 누르면 공을 만들어서 내가 원하는 방향으로 발사한다.
/// 속성: 공 프리펩, 방향
/// </summary>
public class Tower : MonoBehaviour
{
    public Transform prefab;
    public float force = 10;
    Vector3 dir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 스페이스 버튼을 누르면 공을 만들어서 내가 원하는 방향으로 발사한다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transform go = Instantiate(prefab);
            go.position = transform.position; // 타워의 방향으로 위치 이동
            dir = transform.forward; // 타워의 앞 방향

            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(dir * force, ForceMode.Impulse); // 크기가 10인 앞방향 벡터의 힘을 전달

        }

    }
}
