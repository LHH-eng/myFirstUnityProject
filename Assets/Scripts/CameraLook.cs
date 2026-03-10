using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 마우스의 입력을 받아서 플레이어의 몸통을 좌, 우로 돌린다.
/// 속성: 플레이어의 transform, 회전속도
/// 
/// 마우스의 입력을 받아서 카메라의 상,하로 회전시킨다.
/// 속성: 카메라의 transform -> 이건 카메라 자체에 스크립트를 넣는 거라 이미 속성을 가지고 있음
/// </summary>
public class CameraLook : MonoBehaviour
{
    // 속성: 플레이어의 transform, 회전속도
    public Transform player;
    public float rotationSpeed = 2f;

    // 상,하 회전
    float angle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        RotateBody();


        // 마우스의 입력을 받아서 카메라의 상,하로 회전시킨다.
        float mouseY = Input.GetAxis("Mouse Y"); // -1 ~ 1

        // 각도를 -90 ~ 90으로 고정하고 싶다.
        angle += mouseY * rotationSpeed * Time.deltaTime;
        angle = Mathf.Clamp(angle, -90, 90);

        // transform.rotation = transform.rotation * Quaternion.Euler(-mouseY, 0, 0); // 고개가 360도 돌아감
        transform.localRotation = Quaternion.Euler(-angle , 0, 0); // 고개 일정 각도까지 움직이고 고정됨

    }

    private void RotateBody()
    {
        // 마우스의 입력을 받아서 플레이어의 몸통을 좌, 우로 돌린다.

        float mouseX = Input.GetAxis("Mouse X");

        // 짐벌락(gimbal lock) : 오일러 회전의 순서에 따라 회전값이 변하는 형태
        // Quaternion(쿼터니언) : 4개의 원소로 이루어진 회전값(짐벌락을 해결하기 위해 탄생)
        player.rotation = player.rotation * Quaternion.Euler(0, mouseX * rotationSpeed * Time.deltaTime, 0);
    }
}
