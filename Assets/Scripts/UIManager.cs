using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI 버튼을 누를 때 작동되는 이벤트 메서드를 정의
/// 버튼과 이벤트 메서드들을 연결(OnClick)
/// </summary>
public class UIManager : MonoBehaviour
{
    public Button enterBtn;
    public TMP_Text text;
    public TMP_InputField input;

    private void Start()
    {
        enterBtn.onClick.AddListener(OnEnterBtnClkEvent);
        // enterBtn.onClick.AddListener(OnEnterBtnClkEvent);

        // 람다 함수 " () => "
        enterBtn.onClick.AddListener(() => print("한 줄짜리 메서드"));
        // 익명 함수 " => "

        text.text = "Start";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (input.text != "")
            {
                text.text = input.text;
            }

        }
    }

    public void OnEnterBtnClkEvent()
    {
        Debug.Log("Enter 버튼을 클릭했습니다.");

    }

    public void OnCancelBtnClkEvent()
    {
        Debug.Log("Cancel 버튼을 클릭했습니다.");
    }
}
