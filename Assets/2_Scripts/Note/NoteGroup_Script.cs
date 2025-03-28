using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteGroup_Script : MonoBehaviour
{
    [SerializeField] private int noteMaxNum = 5; // ��Ʈ �׷� �ȿ� �����ϴ� �ִ� ��Ʈ ��
    [SerializeField] private Note_Script baseNoteClass = null; // ��Ʈ ������
    [SerializeField] private float noteGapInterval = 2f;  // ��Ʈ�� ������ ����
    [SerializeField] private Transform noteSpawnTrf = null; // ��Ʈ ���� ��ġ
    [SerializeField] private SpriteRenderer btnSrdr = null; // ��ư �̹���
    [SerializeField] private Sprite normalBtnSprite = null; // �Ϲ����� ��ư �̹���
    [SerializeField] private Sprite selectBtnSprite = null; // ������ �� ��ư �̹���
    [SerializeField] private Animation anim = null; // ��ư ���� �� �ִϸ��̼�
    [SerializeField] private TextMeshPro keyCodeTmp = null; // ��ư ���ĺ� �ؽ�Ʈ
    private KeyCode keyCode; // ���� ��Ʈ �׷��� Ű �ڵ�
    private List<Note_Script> noteClassList; // ��Ʈ ����Ʈ

    // Ű �ڵ带 ��ȯ�ϴ� ������Ƽ
    public KeyCode GetKeyCode => this.keyCode;

    // �ʱ�ȭ �Լ�
    private void Awake()
    {
        this.noteClassList = new List<Note_Script>(); // ��Ʈ ����Ʈ �ʱ�ȭ
    }

    // ��Ʈ �׷� ��ũ��Ʈ Ȱ��ȭ �Լ�
    public void Activate_Func(KeyCode _keyCode)
    {
        this.keyCode = _keyCode; // Ű �ڵ� ����

        this.keyCodeTmp.text = _keyCode.ToString(); // Ű �ڵ带 �ؽ�Ʈ�� ǥ��

        for (int i = 0; i < this.noteMaxNum; i++)
        {
            this.OnSpawnNote_Func(true); // ��Ʈ ����
        }

        InputSystem_Manager.Instance.AddKeyCode_Func(_keyCode); // ��ǲ �ý��� �Ŵ����� Ű �ڵ� �߰�
    }

    // ��Ʈ ���� �Լ�
    public void OnSpawnNote_Func(bool _isApple)
    {
        GameObject _noteClassObj = GameObject.Instantiate(this.baseNoteClass.gameObject); // ��Ʈ ���� ������Ʈ ����
        _noteClassObj.transform.SetParent(this.noteSpawnTrf); // ���� ��ġ�� ����
        _noteClassObj.transform.localPosition = Vector3.up * this.noteClassList.Count * this.noteGapInterval; // ������ ��Ʈ�� ���� ����

        Note_Script _noteClass = _noteClassObj.GetComponent<Note_Script>(); // ��Ʈ ���� ������Ʈ���� ��Ʈ ��ũ��Ʈ ����
        _noteClass.Activate_Func(_isApple); // ��Ʈ Ȱ��ȭ

        this.noteClassList.Add(_noteClass); // ��Ʈ ����Ʈ�� �߰�
    }

    // ��Ʈ �Է� ó�� �Լ�
    public void OnInput_Func(bool _isSelected)
    {
        Note_Script _noteClass = this.noteClassList[0]; // ù ��° ��Ʈ ����
        _noteClass.OnInput_Func(_isSelected); // �Է� ó��

        this.noteClassList.RemoveAt(0); // ù ��° ��Ʈ ����

        for (int i = 0; i < this.noteClassList.Count; i++)
            this.noteClassList[i].transform.localPosition = Vector3.up * i * this.noteGapInterval; // ��Ʈ ��ġ ���ġ

        if (_isSelected) // ��ư �̹��� ���� �� �ִϸ��̼� ����
        {
            this.btnSrdr.sprite = this.selectBtnSprite;
            this.anim.Play();
        }
    }

    // �ִϸ��̼� �Ϸ� �� ȣ��Ǵ� �Լ�
    public void CallAni_Done_Func()
    {
        this.btnSrdr.sprite = this.normalBtnSprite; // ��ư �̹����� ������� ����
    }
}
