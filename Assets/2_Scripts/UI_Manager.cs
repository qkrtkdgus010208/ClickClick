using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance; // �̱��� �ν��Ͻ�

    [SerializeField] private Image scoreImg = null; // ������ ������ �������� �������� ���� ǥ���ϱ� ���� �̹���
    [SerializeField] private TextMeshProUGUI scoreTmp = null; // ���� ǥ��

    [SerializeField] private Image timerImg = null; // �ð��� �帣�� �������� �������� ���� ǥ���ϱ� ���� �̹���
    [SerializeField] private TextMeshProUGUI timerTmp = null; // �ð� ǥ��

    // �ʱ�ȭ �Լ�
    void Awake()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����
    }

    // ���� ���� �Լ�
    public void OnScore_Func(int _currentScore, int _maxScore)
    {
        this.scoreTmp.text = $"{_currentScore} / {_maxScore}"; // ���� ǥ��

        this.scoreImg.fillAmount = (float)_currentScore / _maxScore; // ������ �������� ���� ǥ��
    }

    // Ÿ�̸� ���� �Լ�
    public void OnTimer_Func(float _currentTimer, float _maxTimer)
    {
        this.timerTmp.text = $"{_currentTimer.ToString_Func(1)} / {_maxTimer.ToString_Func(0)}"; // �ð� ǥ��

        this.timerImg.fillAmount = (float)_currentTimer / _maxTimer; // ������ �������� ���� ǥ��
    }
}
