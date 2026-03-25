using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 로봇의 Step 정보들을 저장, PLC의 출력신호 -> 로봇 
///                            + 디지털 트윈도 동시에 로봇 시퀀스 수행
/// 속성 : 로봇의 IK-toolkit, Step 정보 저장을 위한 컨테이너
/// </summary>
public class RobotController : MonoBehaviour
{
    public struct Step
    {
        public int id;
        public Vector3 position;
        public Quaternion rotation;
        public bool isSuctionOn;
        public float duration;
    }

    public IK_toolkit iK_Toolkit;
    public List<Step> steps = new List<Step>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    bool isXPlusOn, isYPlusOn, isZPlusOn, isXMinusOn, isYMinusOn, isZMinusOn;
    int xPos, yPos, zPos;
    public float multiplier = 0.01f;

    /// <summary>
    /// UI 버튼의 Position 변경 값을 받아 로봇의 End-Effector 가 움직인다.
    /// </summary>
    void UpdatePosition()
    {
        if (isXPlusOn) xPos = 1;
        else if (isXMinusOn) xPos = -1;
        else xPos = 0;

        if (isYPlusOn) yPos = 1;
        else if (isYMinusOn) yPos = -1;
        else yPos = 0;

        if (isZPlusOn) zPos = 1;
        else if (isZMinusOn) zPos = -1;
        else zPos = 0;

        iK_Toolkit.ik.localPosition += new Vector3(xPos, yPos, zPos) * multiplier;
    }

    /// <summary>
    /// UI 버튼의 Rotation 변경 값을 받아 로봇의 End-Effector 가 움직인다.
    /// </summary>
    void UpdateRotation()
    {

    }

    // -----------------------------------------------------------------------------------
    /// <summary>
    /// Event Trigger 기능을 사용하여 버튼이 누른 순간을 확인
    /// </summary>
    public void OnXPlusBtnDownEvent()
    {
        isXPlusOn = true;
    }

    /// <summary>
    /// Event Trigger 기능을 사용하여 버튼이 떼는 순간을 포착
    /// </summary>
    public void OnXPlusBtnUpEvent()
    {
        isXPlusOn = false;
    }

    public void OnYPlusBtnDownEvent()
    {
        isYPlusOn = true;
    }

    public void OnYPlusBtnUpEvent()
    {
        isYPlusOn = false;
    }

    public void OnZPlusBtnDownEvent()
    {
        isZPlusOn = true;
    }

    public void OnZPlusBtnUpEvent()
    {
        isZPlusOn = false;
    }

    public void OnXMinusBtnDownEvent()
    {
        isXMinusOn = true;
    }

    public void OnXMinusBtnUpEvent()
    {
        isXMinusOn = false;
    }

    public void OnYMinusBtnDownEvent()
    {
        isYMinusOn = true;
    }

    public void OnYMinusBtnUpEvent()
    {
        isYMinusOn = false;
    }

    public void OnZMinusBtnDownEvent()
    {
        isZMinusOn = true;
    }

    public void OnZMinusBtnUpEvent()
    {
        isZMinusOn = false;
    }

    // -----------------------------------------------------------------------------------

    /// <summary>
    /// 버튼을 누르면 현재 로봇의 정보가 Step으로 저장된다.
    /// </summary>
    public void OnTeachBtnClkEvent()
    {

    }

    /// <summary>
    /// 버튼을 누르면 Step 리스트가 초기화된다.
    /// </summary>
    public void OnDeleteBtnClkEvent()
    {

    }

    /// <summary>
    /// 버튼을 누르면 Step 리스트를 순회하며 로봇이 1번 운전인다.
    /// </summary>
    public void OnStartBtnClkEvent()
    {

    }

    /// <summary>
    /// 버튼을 누르면 Step 리스트를 순회하며 로봇이 계속 반복 운전인다.
    /// </summary>
    public void OnCycleBtnClkEvent()
    {

    }

    /// <summary>
    /// 버튼을 누르면 로봇이 멈춘다.
    /// </summary>
    public void OnStopBtnClkEvent()
    {

    }
}
