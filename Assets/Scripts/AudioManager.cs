using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1~5 번 키를 누를 때, 각 키에 따라 다른 소리를 내고 싶다.
/// 속성 : 오디오 클립 리스트, 오디오 소스
/// </summary>
public class AudioManager : MonoBehaviour
{
    public List<AudioClip> clipList;
    AudioSource audioSource; // 소리가 나게끔하는 역할 ex) 스마트폰 이나 MP3플레이어 역할

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioSource.clip = clipList[0];
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioSource.clip = clipList[1];
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            audioSource.clip = clipList[2];
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            audioSource.clip = clipList[3];
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            audioSource.clip = clipList[4];
            audioSource.Play();
        }
    }
}
