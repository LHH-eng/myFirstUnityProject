using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// - 실습4. 3D 시계
///1.시작시 컴퓨터의 시간을 받아온다.
///  - ex) 오후 4:41 → 시침: 4, 분침: 41, 초침: 0
///1.Time.deltaTime으로 시간이 가게 한다.
///2. 시침, 분침, 초침을 시간에 따라 회전하게 한다.
/// </summary>
public class ClockScript : MonoBehaviour
{
    public Transform hourPivot;
    public Transform minPivot;
    public Transform secPivot;
    Quaternion startQH;
    Quaternion startQM;
    Quaternion startQS;
    float startAngleH;
    float startAngleM;
    float startAngleS;
    float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DateTime now = DateTime.Now;

        // 각도로 변환 (360도 기준)
        // 시침: 12시간 = 360도
        startAngleH = (now.Hour % 12) / 12f * 360f;
        // 분침: 60분 = 360도
        startAngleM = now.Minute / 60f * 360f;
        // 초침: 60초 = 360도
        startAngleS = now.Second / 60f * 360f;

        hourPivot = transform.parent;
        minPivot = transform.parent;
        secPivot = transform.parent;

        // 초기 회전 적용
        hourPivot.localRotation = Quaternion.AngleAxis(startAngleH, Vector3.forward);
        minPivot.localRotation = Quaternion.AngleAxis(startAngleM, Vector3.forward);
        secPivot.localRotation = Quaternion.AngleAxis(startAngleS, Vector3.forward);

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        // 초침: 60초에 360도
        float angleS = startAngleS + (time / 60f * 360f);
        // 분침: 3600초에 360도
        float angleM = startAngleM + (time / 3600f * 360f);
        // 시침: 43200초(12시간)에 360도
        float angleH = startAngleH + (time / 43200f * 360f);

        secPivot.localRotation = Quaternion.AngleAxis(angleS, Vector3.back);
        minPivot.localRotation = Quaternion.AngleAxis(angleM, Vector3.back);
        hourPivot.localRotation = Quaternion.AngleAxis(angleH, Vector3.back);

    }
}
