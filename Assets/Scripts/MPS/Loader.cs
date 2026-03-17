using System.Collections;
using UnityEngine;

/// <summary>
/// 신호가 들어오는 순간 1개의 금속 또는 비금속을 랜덤하게 생성한다.
/// 속성: 플라스틱, 금속 프리팹, Load신호
/// </summary>
public class Loader : MonoBehaviour
{
    public bool loadSignal;
    public GameObject plasticPrefab;
    public GameObject metalPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadObject());
    }

    IEnumerator LoadObject()
    {
        while (true)
        {
            // PLC 신호가 1 일때 생성
            yield return new WaitUntil(() => loadSignal);

            // 생성
            int option = Random.Range(0, 2);

            if(option == 0)
            {
                GameObject go = Instantiate(plasticPrefab);
                go.transform.position = transform.position;
            }
            else
            {
                GameObject go = Instantiate(metalPrefab);
                go.transform.position = transform.position;
            }

            yield return new WaitUntil(() => !loadSignal);
        }
    }
}
