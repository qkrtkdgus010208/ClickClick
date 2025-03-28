using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteGroup_Script : MonoBehaviour
{
    [SerializeField] private int noteMaxNum = 5; // 노트 그룹 안에 존재하는 최대 노트 수
    [SerializeField] private Note_Script baseNoteClass = null; // 노트 프리팹
    [SerializeField] private float noteGapInterval = 2f;  // 노트들 사이의 간격
    [SerializeField] private Transform noteSpawnTrf = null; // 노트 생성 위치
    [SerializeField] private SpriteRenderer btnSrdr = null; // 버튼 이미지
    [SerializeField] private Sprite normalBtnSprite = null; // 일반적인 버튼 이미지
    [SerializeField] private Sprite selectBtnSprite = null; // 눌렸을 때 버튼 이미지
    [SerializeField] private Animation anim = null; // 버튼 눌릴 때 애니메이션
    [SerializeField] private TextMeshPro keyCodeTmp = null; // 버튼 알파벳 텍스트
    private KeyCode keyCode; // 현재 노트 그룹의 키 코드
    private List<Note_Script> noteClassList; // 노트 리스트

    // 키 코드를 반환하는 프로퍼티
    public KeyCode GetKeyCode => this.keyCode;

    // 초기화 함수
    private void Awake()
    {
        this.noteClassList = new List<Note_Script>(); // 노트 리스트 초기화
    }

    // 노트 그룹 스크립트 활성화 함수
    public void Activate_Func(KeyCode _keyCode)
    {
        this.keyCode = _keyCode; // 키 코드 설정

        this.keyCodeTmp.text = _keyCode.ToString(); // 키 코드를 텍스트로 표시

        for (int i = 0; i < this.noteMaxNum; i++)
        {
            this.OnSpawnNote_Func(true); // 노트 생성
        }

        InputSystem_Manager.Instance.AddKeyCode_Func(_keyCode); // 인풋 시스템 매니저에 키 코드 추가
    }

    // 노트 생성 함수
    public void OnSpawnNote_Func(bool _isApple)
    {
        GameObject _noteClassObj = GameObject.Instantiate(this.baseNoteClass.gameObject); // 노트 게임 오브젝트 생성
        _noteClassObj.transform.SetParent(this.noteSpawnTrf); // 스폰 위치를 지정
        _noteClassObj.transform.localPosition = Vector3.up * this.noteClassList.Count * this.noteGapInterval; // 생성된 노트를 위로 나열

        Note_Script _noteClass = _noteClassObj.GetComponent<Note_Script>(); // 노트 게임 오브젝트에서 노트 스크립트 추출
        _noteClass.Activate_Func(_isApple); // 노트 활성화

        this.noteClassList.Add(_noteClass); // 노트 리스트에 추가
    }

    // 노트 입력 처리 함수
    public void OnInput_Func(bool _isSelected)
    {
        Note_Script _noteClass = this.noteClassList[0]; // 첫 번째 노트 선택
        _noteClass.OnInput_Func(_isSelected); // 입력 처리

        this.noteClassList.RemoveAt(0); // 첫 번째 노트 삭제

        for (int i = 0; i < this.noteClassList.Count; i++)
            this.noteClassList[i].transform.localPosition = Vector3.up * i * this.noteGapInterval; // 노트 위치 재배치

        if (_isSelected) // 버튼 이미지 변경 및 애니메이션 실행
        {
            this.btnSrdr.sprite = this.selectBtnSprite;
            this.anim.Play();
        }
    }

    // 애니메이션 완료 후 호출되는 함수
    public void CallAni_Done_Func()
    {
        this.btnSrdr.sprite = this.normalBtnSprite; // 버튼 이미지를 원래대로 변경
    }
}
