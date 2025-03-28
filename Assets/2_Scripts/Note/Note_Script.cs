using UnityEngine;

public class Note_Script : MonoBehaviour
{
    [SerializeField] private SpriteRenderer srdr; // ��Ʈ�� ��������Ʈ ������
    [SerializeField] private Sprite appleSprite; // ��� ��������Ʈ
    [SerializeField] private Sprite blueberrySprite; // ��纣�� ��������Ʈ
    private bool isApple; // ���� ��Ʈ�� ������� ����

    // ��Ʈ ��ũ��Ʈ Ȱ��ȭ �Լ�
    public void Activate_Func(bool _isApple)
    {
        this.isApple = _isApple; // ��� ���� ����

        this.srdr.sprite = _isApple ? this.appleSprite : this.blueberrySprite; // ����� ��� ��������Ʈ, �ƴϸ� ��纣�� ��������Ʈ ����
    }

    // ��Ʈ �Է� ó�� �Լ�
    public void OnInput_Func(bool _isSelected)
    {
        if (_isSelected) // ���õ� ��Ʈ�̸�
        {
            bool _isCorrect = (this.isApple == true); // ����� �������� ���� Ȯ��
            GameSystem_Manager.Instance.OnScore_Func(_isCorrect); // ���� �ý��� �Ŵ������� ����
        }

        this.Deactive_Func(); // ��Ʈ ����
    }

    // ��Ʈ ���� �Լ�
    public void Deactive_Func()
    {
        GameObject.Destroy(this.gameObject); // ��Ʈ ���� ������Ʈ ����
    }
}

