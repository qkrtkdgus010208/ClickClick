using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance; // 싱글톤 인스턴스

    [SerializeField] private Image scoreImg = null; // 점수가 오르면 게이지가 차오르는 것을 표현하기 위한 이미지
    [SerializeField] private TextMeshProUGUI scoreTmp = null; // 점수 표기

    [SerializeField] private Image timerImg = null; // 시간이 흐르면 게이지가 차오르는 것을 표현하기 위한 이미지
    [SerializeField] private TextMeshProUGUI timerTmp = null; // 시간 표기

    // 초기화 함수
    void Awake()
    {
        Instance = this; // 싱글톤 인스턴스 설정
    }

    // 점수 갱신 함수
    public void OnScore_Func(int _currentScore, int _maxScore)
    {
        this.scoreTmp.text = $"{_currentScore} / {_maxScore}"; // 점수 표기

        this.scoreImg.fillAmount = (float)_currentScore / _maxScore; // 게이지 차오르는 것을 표현
    }

    // 타이머 갱신 함수
    public void OnTimer_Func(float _currentTimer, float _maxTimer)
    {
        this.timerTmp.text = $"{_currentTimer.ToString_Func(1)} / {_maxTimer.ToString_Func(0)}"; // 시간 표기

        this.timerImg.fillAmount = (float)_currentTimer / _maxTimer; // 게이지 차오르는 것을 표현
    }
}
