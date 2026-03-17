using System.Collections;
using UnityEngine;
using static Cylinder_MPS;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

/// <summary>
/// 실습1. TowerLamp.cs 만들기
/// - PLC X10 켜지면 → RED ON
/// - PLC X10 켜지면 → RED OFF
/// X11: Yellow
/// X12: Green
/// </summary>
public class TowerLamp : MonoBehaviour
{
    public bool x10_PLC;
    public bool x11_PLC;
    public bool x12_PLC;
    public MeshRenderer redLamp;
    public MeshRenderer yellowLamp;
    public MeshRenderer greenLamp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TurnOffAllLamp();
        StartCoroutine(RedLampChange());
        StartCoroutine(YellowLampChange());
        StartCoroutine(GreenLampChange());
    }

    void TurnOffAllLamp()
    {
        redLamp.material.color = new Color(0, 0, 0, 0.7f);
        yellowLamp.material.color = new Color(0, 0, 0, 0.7f);
        greenLamp.material.color = new Color(0, 0, 0, 0.7f);
    }

    IEnumerator RedLampChange()
    {
        while (true)
        {
            yield return new WaitUntil(() => x10_PLC);

            redLamp.material.color = new Color(1, 0, 0, 0.7f);

            yield return new WaitUntil(() => !x10_PLC);

            redLamp.material.color = new Color(0, 0, 0, 0.7f);
        }

    }

    IEnumerator YellowLampChange()
    {
        while (true)
        {
            yield return new WaitUntil(() => x11_PLC);

            yellowLamp.material.color = new Color(1, 1, 0, 0.7f);

            yield return new WaitUntil(() => !x11_PLC);

            yellowLamp.material.color = new Color(0, 0, 0, 0.7f);
        }

    }

    IEnumerator GreenLampChange()
    {
        while (true)
        {
            yield return new WaitUntil(() => x12_PLC);

            greenLamp.material.color = new Color(0, 1, 0, 0.7f);

            yield return new WaitUntil(() => !x12_PLC);

            greenLamp.material.color = new Color(0, 0, 0, 0.7f);
        }
    }
}