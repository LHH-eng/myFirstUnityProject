using UnityEngine;

/// <summary>
/// 스페이스 버튼을 누르면, 누른 시간에 따라 공에게 힘을 위쪽 방향으로 준다.
/// 속성: 공
/// </summary>
public class Pinball : MonoBehaviour
{
    public int totalScore;
    public Rigidbody ball;
    public float ballPower;
    float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 스페이스 버튼을 누르면, 누른 시간에 따라 공에게 힘을 위쪽 방향으로 준다.
        if (Input.GetKey(KeyCode.Space))
        {
            time += Time.deltaTime;

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            // 공발사
            ball.AddForce(transform.up * ballPower * time, ForceMode.Impulse);

            time = 0;

        }
    }
}
