using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NoteSystem_Manager : MonoBehaviour
{
    public static NoteSystem_Manager Instance; // �̱��� �ν��Ͻ�

    [SerializeField] private NoteGroup_Script baseNoteGroupClass = null; // ��Ʈ �׷� ������
    [SerializeField] private float noteGroupWithinterval = 1f; // ��Ʈ �׷� ���� ����
    [SerializeField] // ��ü Ű�ڵ� �迭
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
    [SerializeField] private int initNoteGroupNum = 2; // �ʱ� ��Ʈ �׷� ����
    private List<NoteGroup_Script> noteGroupClassList; // ��Ʈ �׷� ����Ʈ

    // �ʱ�ȭ �Լ�
    void Awake()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����

        this.noteGroupClassList = new List<NoteGroup_Script>(); // ��Ʈ �׷� ����Ʈ �ʱ�ȭ
    }

    // ��Ʈ �ý��� �Ŵ��� Ȱ��ȭ �Լ�
    public void Activate_Func()
    {
        for (int i = 0; i < this.initNoteGroupNum; i++) // �ʱ� ��Ʈ �׷� ������ŭ ��Ʈ �׷� ����
        {
            KeyCode _keyCode = this.wholeKeyCodeArr[i];
            this.OnSpawnNoteGroup_Func(_keyCode);
        }
    }

    // ��Ʈ �׷� ���� �Լ� (Ű�ڵ� ���� �����ϱ� ����)
    public void OnSpawnNoteGroup_Func()
    {
        int _activateNoteGroupNum = this.noteGroupClassList.Count;
        KeyCode _keyCode = this.wholeKeyCodeArr[_activateNoteGroupNum]; // ������ ���� Ű�ڵ� ã�� A -> S -> D -> F ...
        this.OnSpawnNoteGroup_Func(_keyCode); // ��¥�� �����ϱ� ���� Ű�ڵ�� �Բ� ����
    }

    // ��Ʈ �׷� ���� �Լ�
    public void OnSpawnNoteGroup_Func(KeyCode _keyCode)
    {
        GameObject _noteGroupClassObj = GameObject.Instantiate(this.baseNoteGroupClass.gameObject); // ��Ʈ �׷� ����
        _noteGroupClassObj.transform.localPosition = Vector3.right * this.noteGroupWithinterval * noteGroupClassList.Count; // ������ ��Ʈ �׷��� �������� ����

        NoteGroup_Script _noteGroupClass = _noteGroupClassObj.GetComponent<NoteGroup_Script>(); // ��Ʈ �׷� ���ӿ�����Ʈ���� ��Ʈ �׷� ��ũ��Ʈ ����
        _noteGroupClass.Activate_Func(_keyCode); // ��Ʈ �׷� Ȱ��ȭ

        this.noteGroupClassList.Add(_noteGroupClass); // ��Ʈ �׷� ����Ʈ�� �߰�
    }

    // ��ǲ ó�� �Լ�
    public void OnInput_Func(KeyCode _keyCode)
    {
        int _randID = Random.Range(0, this.noteGroupClassList.Count); // 0 ~ (��Ʈ �׷� ���� - 1) ������ ���� ���� ����
        NoteGroup_Script _randNoteGroupClass = this.noteGroupClassList[_randID]; // ���� ���ڿ� �ش��ϴ� 1���� ��Ʈ �׷��� ����

        NoteGroup_Script _correctNoteGroupClass = null; // ���� �ۿ��� ����Ʈ�� �����ϱ� ���� ����
        foreach (NoteGroup_Script _noteGroupClass in noteGroupClassList)
        {
            _noteGroupClass.OnSpawnNote_Func(_noteGroupClass == _randNoteGroupClass); // ���� ���ڿ� �ش�Ǵ� ��Ʈ �׷����� �ƴ����� ��Ʈ �׷쿡�� ����, ���������� �ش�Ǹ� ��� �ƴϸ� ��纣���� ����

            if (_noteGroupClass.GetKeyCode != _keyCode) // ���� Ű�ڵ尡 ��Ʈ �׷��� Ű�ڵ����� �ƴ��� ���θ� Ȯ��
                _noteGroupClass.OnInput_Func(false);  // �������� ���� ��Ʈ �׷���  
            else
                _correctNoteGroupClass = _noteGroupClass;
        }

        if (_correctNoteGroupClass)
            _correctNoteGroupClass.OnInput_Func(true); // ������ ��Ʈ �׷���
    }
}

