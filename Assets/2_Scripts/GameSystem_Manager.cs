using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem_Manager : MonoBehaviour
{
    public static GameSystem_Manager Instance; // 싱글톤 인스턴스

    [SerializeField] private int maxScore = 100; // 최대 점수 
    [SerializeField] private int noteGroupSpawnConditionScore = 10; // 새로운 노트 그룹 추가 기준 점수(10점마다 노트 그룹 추가)
    [SerializeField] private GameObject gameClearObj = null; // 게임 클리어 오브젝트
    [SerializeField] private GameObject gameOverObj = null; // 게임 오버 오브젝트
    private int nextNoteGroupUnlockCnt = 0; // 반복적인 노트 그룹 추가를 위한 카운트
    private int score = 0; // 현재 점수

    [SerializeField] private float maxTime = 30f; // 타이머 최대 시간

    // 게임이 끝났는지 여부를 반환하는 프로퍼티
    public bool IsGameDone => this.gameClearObj.activeSelf || this.gameOverObj.activeSelf; // 둘 중 하나라도 활성화되면 게임이 끝난 상태

    // 초기화 함수
    void Awake()
    {
        Instance = this; // 싱글톤 인스턴스 설정
    }

    // 시작 함수
    private void Start()
    {
        UI_Manager.Instance.OnScore_Func(this.score, this.maxScore); // 초기 점수 세팅(ex: 0 / 100)

        NoteSystem_Manager.Instance.Activate_Func(); // 노트 시스템 매니저 활성화

        this.gameClearObj.SetActive(false); // 게임 클리어 오브젝트 비활성화
        this.gameOverObj.SetActive(false); // 게임 오버 오브젝트 비활성화

        StartCoroutine(this.OnTimer_Cor()); // 타이머 코루틴 시작
    }

    // 타이머 코루틴
    IEnumerator OnTimer_Cor()
    {
        float _currentTime = 0;

        while (_currentTime < maxTime)
        {
            _currentTime += Time.deltaTime;

            UI_Manager.Instance.OnTimer_Func(_currentTime, this.maxTime); // 타이머 갱신

            yield return null;

            if (this.IsGameDone) // 게임이 끝났는지 확인
            {
                yield break;
            }
        }

        // Game Over
        this.gameOverObj.SetActive(true); // 게임 오버 오브젝트 활성화
    }

    // 점수 갱신 함수
    public void OnScore_Func(bool _isCorrect)
    {
        if (_isCorrect) // 사과를 눌렀으면
        {
            this.score++;
            this.nextNoteGroupUnlockCnt++;

            if (this.noteGroupSpawnConditionScore <= this.nextNoteGroupUnlockCnt) // 노트 그룹 추가 조건 확인
            {
                this.nextNoteGroupUnlockCnt = 0;

                NoteSystem_Manager.Instance.OnSpawnNoteGroup_Func(); // 10점마다 노트 그룹을 생성하라고 노트 시스템 매니저에게 전달
            }

            if (this.score >= this.maxScore) // 최대 점수에 도달했는지 확인
            {
                // Game Clear
                this.gameClearObj.SetActive(true); // 게임 클리어 오브젝트 활성화
            }
        }
        else // 사과를 누르지 않았으면
        {
            this.score--;
            this.nextNoteGroupUnlockCnt--;
        }

        UI_Manager.Instance.OnScore_Func(this.score, this.maxScore); // 점수 갱신
    }

    // 게임 재시작 함수
    public void CallBtn_Restart_Func()
    {
        SceneManager.LoadScene(0); // Main 씬을 다시 불러옴
    }
}

