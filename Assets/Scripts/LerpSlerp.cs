using UnityEngine;

/// <summary>
/// Lerp와 Slerp를 회전을 통해 비교
/// 속성: Lerp바늘, Slerp바늘
/// </summary>
public class LerpSlerp : MonoBehaviour
{
    public Transform lerp바늘;
    Quaternion startQ, endQ;
    public float startAngle = 0, endAngle = 90;

    public Transform slerp바늘;
    Quaternion startQSlerp, endQSlerp;
    public float startAngleSlerp = 0, endAngleSlerp = 90;

    public float duration = 3;
    float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startQ = Quaternion.AngleAxis(startAngle, lerp바늘.up); // 초기값 지정
        endQ = Quaternion.AngleAxis(endAngle, lerp바늘.up);

        startQSlerp = Quaternion.AngleAxis(startAngleSlerp, slerp바늘.up); // 초기값 지정
        endQSlerp = Quaternion.AngleAxis(endAngleSlerp, slerp바늘.up);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > duration)
            time = 0;

        Quaternion lerpQ = Quaternion.Lerp(startQ, endQ, time / duration);

        lerp바늘.rotation = lerpQ;

        Quaternion sLerpQ = Quaternion.Slerp(startQSlerp, endQSlerp, time / duration);

        slerp바늘.rotation = sLerpQ;

    }
}
