using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NoteSystem_Manager : MonoBehaviour
{
    public static NoteSystem_Manager Instance; // 싱글톤 인스턴스

    [SerializeField] private NoteGroup_Script baseNoteGroupClass = null; // 노트 그룹 프리팹
    [SerializeField] private float noteGroupWithinterval = 1f; // 노트 그룹 생성 간격
    [SerializeField] // 전체 키코드 배열
    private KeyCode[] wholeKeyCodeArr = new KeyCode[]
    {
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
    };
    [SerializeField] private int initNoteGroupNum = 2; // 초기 노트 그룹 개수
    private List<NoteGroup_Script> noteGroupClassList; // 노트 그룹 리스트

    // 초기화 함수
    void Awake()
    {
        Instance = this; // 싱글톤 인스턴스 설정

        this.noteGroupClassList = new List<NoteGroup_Script>(); // 노트 그룹 리스트 초기화
    }

    // 노트 시스템 매니저 활성화 함수
    public void Activate_Func()
    {
        for (int i = 0; i < this.initNoteGroupNum; i++) // 초기 노트 그룹 개수만큼 노트 그룹 생성
        {
            KeyCode _keyCode = this.wholeKeyCodeArr[i];
            this.OnSpawnNoteGroup_Func(_keyCode);
        }
    }

    // 노트 그룹 생성 함수 (키코드 없이 접근하기 위함)
    public void OnSpawnNoteGroup_Func()
    {
        int _activateNoteGroupNum = this.noteGroupClassList.Count;
        KeyCode _keyCode = this.wholeKeyCodeArr[_activateNoteGroupNum]; // 다음에 넣을 키코드 찾음 A -> S -> D -> F ...
        this.OnSpawnNoteGroup_Func(_keyCode); // 진짜로 생성하기 위해 키코드와 함께 전달
    }

    // 노트 그룹 생성 함수
    public void OnSpawnNoteGroup_Func(KeyCode _keyCode)
    {
        GameObject _noteGroupClassObj = GameObject.Instantiate(this.baseNoteGroupClass.gameObject); // 노트 그룹 생성
        _noteGroupClassObj.transform.localPosition = Vector3.right * this.noteGroupWithinterval * noteGroupClassList.Count; // 생성된 노트 그룹을 우측으로 나열

        NoteGroup_Script _noteGroupClass = _noteGroupClassObj.GetComponent<NoteGroup_Script>(); // 노트 그룹 게임오브젝트에서 노트 그룹 스크립트 추출
        _noteGroupClass.Activate_Func(_keyCode); // 노트 그룹 활성화

        this.noteGroupClassList.Add(_noteGroupClass); // 노트 그룹 리스트에 추가
    }

    // 인풋 처리 함수
    public void OnInput_Func(KeyCode _keyCode)
    {
        int _randID = Random.Range(0, this.noteGroupClassList.Count); // 0 ~ (노트 그룹 개수 - 1) 사이의 랜덤 숫자 생성
        NoteGroup_Script _randNoteGroupClass = this.noteGroupClassList[_randID]; // 랜덤 숫자에 해당하는 1개의 노트 그룹을 지정

        NoteGroup_Script _correctNoteGroupClass = null; // 루프 밖에서 리스트를 제어하기 위한 변수
        foreach (NoteGroup_Script _noteGroupClass in noteGroupClassList)
        {
            _noteGroupClass.OnSpawnNote_Func(_noteGroupClass == _randNoteGroupClass); // 랜덤 숫자에 해당되는 노트 그룹인지 아닌지를 노트 그룹에게 전달, 최종적으로 해당되면 사과 아니면 블루베리를 생성

            if (_noteGroupClass.GetKeyCode != _keyCode) // 누른 키코드가 노트 그룹의 키코드인지 아닌지 여부를 확인
                _noteGroupClass.OnInput_Func(false);  // 눌려지지 않은 노트 그룹임  
            else
                _correctNoteGroupClass = _noteGroupClass;
        }

        if (_correctNoteGroupClass)
            _correctNoteGroupClass.OnInput_Func(true); // 눌려진 노트 그룹임
    }
}

