using UnityEngine;

/// <summary>
/// 물체A를 현 위치에서 Target까지 2초 동안 이동시킨다.
/// 속성: 물체A, Target의 transform, duration
/// </summary>
public class LerpManager : MonoBehaviour
{
    public Transform objA;
    public Transform target;
    public float duration;
    [Range(0, 1)] // Attribute
    public float ratio;
    float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= duration)
            time = 0;

        // 물체A를 현 위치에서 Target까지 duration초 동안 이동시킨다.
        Vector3 result = Vector3.Lerp(Vector3.zero, target.position, time/duration);
        objA.position = result;
    }


}
