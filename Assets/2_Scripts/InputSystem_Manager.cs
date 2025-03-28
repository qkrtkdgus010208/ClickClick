using System.Collections.Generic;
using UnityEngine;

public class InputSystem_Manager : MonoBehaviour
{
    public static InputSystem_Manager Instance; // 싱글톤 인스턴스

    private List<KeyCode> keyCodeList; // 키코드 리스트

    // 초기화 함수
    void Awake()
    {
        Instance = this; // 싱글톤 인스턴스 설정

        this.keyCodeList = new List<KeyCode>(); // 키코드 리스트 초기화
    }

    // 키코드를 리스트에 추가하는 함수
    public void AddKeyCode_Func(KeyCode _keyCode)
    {
        this.keyCodeList.Add(_keyCode); // 전달 받은 키코드를 리스트에 저장
    }

    // 매 프레임마다 호출되는 업데이트 함수
    void Update()
    {
        if (GameSystem_Manager.Instance.IsGameDone) // 게임이 끝났는지 확인
            return;

        foreach (KeyCode _keyCode in keyCodeList) // 키코드 리스트를 순회
        {
            if (Input.GetKeyDown(_keyCode)) // 누른 키코드가 리스트에 존재하는 키코드인지 검사
            {
                NoteSystem_Manager.Instance.OnInput_Func(_keyCode); // 노트 시스템 매니저에게 키코드가 눌렸다고 전달
                break;
            }
        }
    }
}

