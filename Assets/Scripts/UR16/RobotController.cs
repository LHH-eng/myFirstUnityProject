using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 로봇의 Step 정보들을 저장, PLC의 출력신호 -> 로봇 
///                            + 디지털 트윈도 동시에 로봇 시퀀스 수행
/// 속성 : 로봇의 IK-toolkit, Step 정보 저장을 위한 컨테이너
/// </summary>
public class RobotController : MonoBehaviour
{
    [Serializable]
    public struct Step
    {
        public int id;
        public Vector3 position;
        public Quaternion rotation;
        public bool isSuctionOn;
        public float duration;
        public float speed;
    }

    public IK_toolkit iK_Toolkit;
    public List<Step> steps = new List<Step>();
    bool isXPlusOn, isYPlusOn, isZPlusOn, isXMinusOn, isYMinusOn, isZMinusOn;
    int xPos, yPos, zPos;
    bool isXRotPlusOn, isYRotPlusOn, isZRotPlusOn;
    bool isXRotMinusOn, isYRotMinusOn, isZRotMinusOn;
    int xRot, yRot, zRot;
    Vector3 origin;
    bool isMoving;
    public float multiplier = 0.01f;
    public float rotMultiplier = 5f;

    [Header("UI")]
    public TMP_InputField xPosInput;
    public TMP_InputField yPosInput, zPosInput;
    public TMP_InputField xRotInput, yRotInput, zRotInput;
    public TMP_InputField durationInput, speedInput;
    public Toggle suctionToggle;
    public EventTrigger xRotPlusET, yRotPlusET, zRotPlusET;
    public EventTrigger xRotMinusET, yRotMinusET, zRotMinusET;

    private void Awake()
    {
        AddEventTriggerListner(EventTriggerType.PointerDown, OnXRotPlusBtnDownEvent, ref xRotPlusET);
        AddEventTriggerListner(EventTriggerType.PointerUp, OnXRotPlusBtnUpEvent, ref xRotPlusET);
        AddEventTriggerListner(EventTriggerType.PointerDown, OnXRotMinusBtnDownEvent, ref xRotMinusET);
        AddEventTriggerListner(EventTriggerType.PointerUp, OnXRotMinusBtnUpEvent, ref xRotMinusET);

        AddEventTriggerListner(EventTriggerType.PointerDown, OnYRotPlusBtnDownEvent, ref yRotPlusET);
        AddEventTriggerListner(EventTriggerType.PointerUp, OnYRotPlusBtnUpEvent, ref yRotPlusET);
        AddEventTriggerListner(EventTriggerType.PointerDown, OnYRotMinusBtnDownEvent, ref yRotMinusET);
        AddEventTriggerListner(EventTriggerType.PointerUp, OnYRotMinusBtnUpEvent, ref yRotMinusET);

        AddEventTriggerListner(EventTriggerType.PointerDown, OnZRotPlusBtnDownEvent, ref zRotPlusET);
        AddEventTriggerListner(EventTriggerType.PointerUp, OnZRotPlusBtnUpEvent, ref zRotPlusET);
        AddEventTriggerListner(EventTriggerType.PointerDown, OnZRotMinusBtnDownEvent, ref zRotMinusET);
        AddEventTriggerListner(EventTriggerType.PointerUp, OnZRotMinusBtnUpEvent, ref zRotMinusET);
    }

    /// <summary>
    /// 버튼의 이벤트 트리거에 커스텀 메서드를 특정 타입에 연결하는 메서드
    /// </summary>
    /// <param name="eventType">이벤트 타입</param>
    /// <param name="call">연결하고자 하는 메서드</param>
    /// <param name="trigger">연결하고자 하는 버튼의 이벤트 트리거</param>
    void AddEventTriggerListner(EventTriggerType eventType, System.Action<PointerEventData> call, ref EventTrigger trigger)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;

        entry.callback.AddListener((data) => call((PointerEventData)data));

        trigger.triggers.Add(entry);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = iK_Toolkit.ik.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            UpdatePosition();
            UpdateRotation();
        }

        ShowPosition();
        ShowRotation();
    }

    void ShowPosition()
    {
        xPosInput.text = iK_Toolkit.ik.localPosition.x.ToString();
        yPosInput.text = iK_Toolkit.ik.localPosition.y.ToString();
        zPosInput.text = iK_Toolkit.ik.localPosition.z.ToString();
    }
    void ShowRotation()
    {
        xRotInput.text = iK_Toolkit.ik.localRotation.eulerAngles.x.ToString();
        yRotInput.text = iK_Toolkit.ik.localRotation.eulerAngles.y.ToString();
        zRotInput.text = iK_Toolkit.ik.localRotation.eulerAngles.z.ToString();
    }

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
        if (isXRotPlusOn) xRot = 1;
        else if (isXRotMinusOn) xRot = -1;
        else xRot = 0;

        if (isYRotPlusOn) yRot = 1;
        else if (isYRotMinusOn) yRot = -1;
        else yRot = 0;

        if (isZRotPlusOn) zRot = 1;
        else if (isZRotMinusOn) zRot = -1;
        else zRot = 0;

        iK_Toolkit.ik.localRotation *= Quaternion.Euler(xRot * rotMultiplier, yRot * rotMultiplier, zRot * rotMultiplier);
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


    public void OnXRotPlusBtnDownEvent(PointerEventData data)
    {
        isXRotPlusOn = true;
    }

    public void OnXRotPlusBtnUpEvent(PointerEventData data)
    {
        isXRotPlusOn = false;
    }

    public void OnXRotMinusBtnDownEvent(PointerEventData data)
    {
        isXRotMinusOn = true;
    }

    public void OnXRotMinusBtnUpEvent(PointerEventData data)
    {
        isXRotMinusOn = false;
    }
    public void OnYRotPlusBtnDownEvent(PointerEventData data)
    {
        isYRotPlusOn = true;
    }

    public void OnYRotPlusBtnUpEvent(PointerEventData data)
    {
        isYRotPlusOn = false;
    }

    public void OnYRotMinusBtnDownEvent(PointerEventData data)
    {
        isYRotMinusOn = true;
    }

    public void OnYRotMinusBtnUpEvent(PointerEventData data)
    {
        isYRotMinusOn = false;
    }

    public void OnZRotPlusBtnDownEvent(PointerEventData data)
    {
        isZRotPlusOn = true;
    }

    public void OnZRotPlusBtnUpEvent(PointerEventData data)
    {
        isZRotPlusOn = false;
    }

    public void OnZRotMinusBtnDownEvent(PointerEventData data)
    {
        isZRotMinusOn = true;
    }

    public void OnZRotMinusBtnUpEvent(PointerEventData data)
    {
        isZRotMinusOn = false;
    }

    int stepCout;
    /// <summary>
    /// 버튼을 누르면 현재 로봇의 정보가 Step으로 저장된다.
    /// </summary>
    public void OnTeachBtnClkEvent()
    {
        Step step = new Step();

        step.position = iK_Toolkit.ik.localPosition;
        step.rotation = iK_Toolkit.ik.localRotation;

        bool isParsed = float.TryParse(durationInput.text, out step.duration);

        if (!isParsed)
        {
            Debug.LogAssertion("Duration은 양의 정수 또는 실수형으로 입력 후 다시 시도해 주세요.");
            return;
        }

        step.isSuctionOn = suctionToggle.isOn;

        isParsed = float.TryParse(speedInput.text, out step.speed);

        if (!isParsed)
        {
            Debug.LogAssertion("Speed는 양의 정수 또는 실수형으로 입력 후 다시 시도해 주세요.");
            return;
        }

        step.id = stepCout++;

        steps.Add(step);
        Debug.Log($"{stepCout}번째 Step이 성공적으로 저장되었습니다.");
    }

    /// <summary>
    /// 버튼을 누르면 Step 리스트가 초기화된다.
    /// </summary>
    public void OnDeleteBtnClkEvent()
    {
        steps.Clear();
    }

    /// <summary>
    /// 버튼을 누르면 Step 리스트를 순회하며 로봇이 1번 운전인다.
    /// </summary>
    public void OnStartBtnClkEvent()
    {
        // 초기 위치를 받아놔야함 -> step 0
        // step 0: 앞쪽 이동

        // step 0 -> step 1 
        // Coroutine함수 사용 -> Vector3.Lerp(A, B, t)
        // yield return new waitForSeconds(duration - t);
        StartCoroutine(MoveStep());

    }

    Vector3 startPos;
    Vector3 endPos;

    /// <summary>
    /// steps 리스트를 순회하며, 로봇을 움직인다.
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveStep()
    {
        isMoving = true;

        for (int i = 0; i < steps.Count; i++)
        {
            if (i == 0) startPos = origin;
            else startPos = steps[i - 1].position;

            endPos = steps[i].position;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * steps[i].speed;
                iK_Toolkit.ik.localPosition = Vector3.Lerp(startPos, endPos, t);
                yield return null;
            }
            
            yield return new WaitForSeconds(steps[i].duration - (1f / steps[i].speed));
        }

        isMoving = false;
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
