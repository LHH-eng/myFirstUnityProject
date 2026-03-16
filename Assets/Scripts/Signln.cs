using JetBrains.Annotations;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 1. 아이디와 패스워드가 일치하면 로그인을 한다.
/// 2. 회원가입 버튼을 누르면 회원가입 패널로 넘어간다(Login패널OFF, PW패널 ON)
/// 3. 아이디가 존재하지 않고, 비밀번호가 비밀번호 체크와 일치하는 지 확인한다.
/// 4. 정규표현식을 통해 비밀번호가 유효하면 회원가입을 한다.
/// </summary>
public class Signln : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject logInPanel; // 인스펙터에서 연결할 회원가입 패널
    public GameObject signUpPanel;  // 인스펙터에서 연결할 로그인 패널 (선택 사항)
    public GameObject testPanel;

    [Header("로그인 패널")] // 어트리뷰트 
    public TMP_InputField signINIDInput;
    public TMP_InputField signINPWInput;
    public Button okBtn_signIn;
    public Button exitBtn_signIn;
    public Button signUpBtn_signIn;

    [Header("회원가입 패널")]
    public TMP_InputField signUpIDInput;
    public TMP_InputField signUpPWInput;
    public TMP_InputField signUpPWCheckInput;
    public Button okBtn_signUp;
    public Button cancelBtn_signUp;

    [Header("ID & PW 저장")]
    // Dictionary: 빠른 로그인 확인용(Key: 아이디, Value: 비밀번호)
    private Dictionary<string, string> userDict = new Dictionary<string, string>();

    // List: 가입된 유저의 아이디 목록 (순서 보장, 유저 수 파악 등에 사용)
    private List<string> userIdList = new List<string>();

    // 비밀번호 정규식 패턴 (영문 대소문자 1개 이상, 특수문자 1개 이상, 공백 없이 8자 이상)
    private const string passwordPattern = @"^(?=.*[a-zA-Z])(?=.*[^a-zA-Z0-9\s])\S{8,}$";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        testPanel.SetActive(false);
        // 시작할 때 로그인 패널은 켜고, 회원가입 패널은 꺼둡니다.
        logInPanel.SetActive(true);
        signUpPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


    }

    // [("로그인 패널")]
    // exitBtn_signIn를 누르면 로그인 패널이 닫힌다.
    public void OnEnterexitBtn_signIn()
    {
        logInPanel.SetActive(false);
        Debug.Log("로그인 패널이 닫혔습니다.");
    }

    // signUpBtn_signIn를 누르면 로그인 패널이 닫히고 회원가입 패널이 열린다.
    public void OnEntersignUpBtn_signIn()
    {
        logInPanel.SetActive(false);
        signUpPanel.SetActive(true);

        // 회원가입 창을 열 때 기존에 적혀있던 텍스트 초기화
        signUpIDInput.text = "";
        signUpPWInput.text = "";
        signUpPWCheckInput.text = "";
    }

    // okBtn_signIn를 누르면 ID와 PW가 저장된 값과 동일한 지 확인 후 "로그인이 되었습니다. " 문구가 나온다
    public void OnEnterokBtn_signIn()
    {
        string id = signINIDInput.text;
        string pw = signINPWInput.text;

        // Dictionary를 활용한 로그인 검증
        // 1. ContainsKey(id) 로 아이디가 존재하는지 확인
        // 2. userDict[id] == pw 로 저장된 비밀번호와 입력한 비밀번호가 같은지 확인
        if (userDict.ContainsKey(id) && userDict[id] == pw)
        {
            Debug.Log("로그인이 되었습니다.");
            // TODO: 다음 씬으로 넘어가거나 게임 시작 처리
        }
        else
        {
            Debug.Log("아이디가 존재하지 않거나 비밀번호가 일치하지 않습니다.");
        }
    }

    // [("회원가입 패널")]
    // okBtn_signUp를 누르면 회원가입이 된다.(정규표현식 함수로 비밀번호를 확인 하고)
    public void OnEnterokBtn_signUp()
    {
        string id = signUpIDInput.text;
        string pw = signUpPWInput.text;
        string pwCheck = signUpPWCheckInput.text;

        // 1. 아이디 입력 확인
        if (string.IsNullOrEmpty(id))
        {
            Debug.Log("아이디를 입력해주세요.");
            return;
        }

        // 2. 아이디 중복 검사 (Dictionary 활용)
        if (userDict.ContainsKey(id))
        {
            Debug.Log("이미 존재하는 아이디입니다.");
            return;
        }

        // 3. 비밀번호와 비밀번호 확인이 일치하는지 검사
        if (pw != pwCheck)
        {
            Debug.Log("비밀번호와 비밀번호 확인이 일치하지 않습니다.");
            return;
        }

        // 4. 정규표현식으로 비밀번호 유효성 검사
        if (!Regex.IsMatch(pw, passwordPattern))
        {
            Debug.Log("비밀번호는 영문, 특수문자를 포함해 총 8자 이상이어야 합니다.");
            return;
        }

        // --- 회원가입 성공 처리: Dictionary와 List에 데이터 추가 ---
        userDict.Add(id, pw);   // Dictionary에 ID와 PW 쌍으로 저장
        userIdList.Add(id);     // List에 ID 저장 (가입 순서 기억용)

        Debug.Log($"회원가입이 완료되었습니다! 현재 가입된 총 유저 수: {userIdList.Count}명");
        logInPanel.SetActive(true);
        signUpPanel.SetActive(false);

    }

    // cancelBtn_signUp를 누르면 회원가입 패널이 닫힌다.
    public void OnEntercancelBtn_signUp()
    {
        logInPanel.SetActive(true);
        signUpPanel.SetActive(false);
    }


}
