//using UnityEngine;

///// <summary>
///// 실습2. Dog에셋을 사용하여, 강아지 3마리가 A -> B -> C -> A로 이동
///// 강아지1(A: 1초 동안 이동, B: 2초, c: 3초) -> DogRun.cs
///// public Transform[] targets;
///// public float timeA = 1;
///// public float timeB = 2;
///// public float timeC = 3;
///// </summary>
//public class DogRun : MonoBehaviour
//{

//    public float[] duration;
//    public Transform[] targets;
//    public Transform objDog;
//    float time;
//    int option = 0;

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        time += Time.deltaTime;

//        Vector3 locationValue = targets[option].position - objDog.position;

//        Vector3 xyLocationValue = new Vector3(locationValue.x, 0, locationValue.z);
//        float distance = xyLocationValue.magnitude; // 거리: 벡터의 크기

//        if (distance < 0.1f)
//        {
//            option++;
//            if (option == targets.Length + 1)
//                option = 0;

//            return;
//        }

//        Debug.Log(option);

//        Vector3 result = Vector3.Lerp(objDog.position, targets[option].position, time / duration[option]);
//        objDog.position = result;

//        Debug.Log(option);
//    }
//}
