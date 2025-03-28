using System.Collections.Generic;
using UnityEngine;

public class InputSystem_Manager : MonoBehaviour
{
    public static InputSystem_Manager Instance; // �̱��� �ν��Ͻ�

    private List<KeyCode> keyCodeList; // Ű�ڵ� ����Ʈ

    // �ʱ�ȭ �Լ�
    void Awake()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����

        this.keyCodeList = new List<KeyCode>(); // Ű�ڵ� ����Ʈ �ʱ�ȭ
    }

    // Ű�ڵ带 ����Ʈ�� �߰��ϴ� �Լ�
    public void AddKeyCode_Func(KeyCode _keyCode)
    {
        this.keyCodeList.Add(_keyCode); // ���� ���� Ű�ڵ带 ����Ʈ�� ����
    }

    // �� �����Ӹ��� ȣ��Ǵ� ������Ʈ �Լ�
    void Update()
    {
        if (GameSystem_Manager.Instance.IsGameDone) // ������ �������� Ȯ��
            return;

        foreach (KeyCode _keyCode in keyCodeList) // Ű�ڵ� ����Ʈ�� ��ȸ
        {
            if (Input.GetKeyDown(_keyCode)) // ���� Ű�ڵ尡 ����Ʈ�� �����ϴ� Ű�ڵ����� �˻�
            {
                NoteSystem_Manager.Instance.OnInput_Func(_keyCode); // ��Ʈ �ý��� �Ŵ������� Ű�ڵ尡 ���ȴٰ� ����
                break;
            }
        }
    }
}

