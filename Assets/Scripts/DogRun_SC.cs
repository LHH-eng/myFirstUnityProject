using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DogRun_SC : MonoBehaviour
{
    public List<Transform> targets;
    public float[] durations;
    Vector3 originPos;
    float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartSequence());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartSequence()
    {
        for (int i = 0; i < targets.Count - 1; i++) // 0: originPos, 1: A, 2: B, 3: C
        {
            yield return MoveAtoB(i, i + 1);
        }
        yield return MoveAtoB(targets.Count - 1, 0);
    }

    IEnumerator MoveAtoB(int from, int to)
    {
        while (true)
        {
            time += Time.deltaTime;

            if (time > durations[from])
            {
                time = 0;
                break;
            }

            transform.position = Vector3.Lerp(targets[from].position, targets[to].position, time / durations[from]);

            yield return null;
        }
    }


}
