using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem_Manager : MonoBehaviour
{
    public static GameSystem_Manager Instance; // �̱��� �ν��Ͻ�

    [SerializeField] private int maxScore = 100; // �ִ� ���� 
    [SerializeField] private int noteGroupSpawnConditionScore = 10; // ���ο� ��Ʈ �׷� �߰� ���� ����(10������ ��Ʈ �׷� �߰�)
    [SerializeField] private GameObject gameClearObj = null; // ���� Ŭ���� ������Ʈ
    [SerializeField] private GameObject gameOverObj = null; // ���� ���� ������Ʈ
    private int nextNoteGroupUnlockCnt = 0; // �ݺ����� ��Ʈ �׷� �߰��� ���� ī��Ʈ
    private int score = 0; // ���� ����

    [SerializeField] private float maxTime = 30f; // Ÿ�̸� �ִ� �ð�

    // ������ �������� ���θ� ��ȯ�ϴ� ������Ƽ
    public bool IsGameDone => this.gameClearObj.activeSelf || this.gameOverObj.activeSelf; // �� �� �ϳ��� Ȱ��ȭ�Ǹ� ������ ���� ����

    // �ʱ�ȭ �Լ�
    void Awake()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����
    }

    // ���� �Լ�
    private void Start()
    {
        UI_Manager.Instance.OnScore_Func(this.score, this.maxScore); // �ʱ� ���� ����(ex: 0 / 100)

        NoteSystem_Manager.Instance.Activate_Func(); // ��Ʈ �ý��� �Ŵ��� Ȱ��ȭ

        this.gameClearObj.SetActive(false); // ���� Ŭ���� ������Ʈ ��Ȱ��ȭ
        this.gameOverObj.SetActive(false); // ���� ���� ������Ʈ ��Ȱ��ȭ

        StartCoroutine(this.OnTimer_Cor()); // Ÿ�̸� �ڷ�ƾ ����
    }

    // Ÿ�̸� �ڷ�ƾ
    IEnumerator OnTimer_Cor()
    {
        float _currentTime = 0;

        while (_currentTime < maxTime)
        {
            _currentTime += Time.deltaTime;

            UI_Manager.Instance.OnTimer_Func(_currentTime, this.maxTime); // Ÿ�̸� ����

            yield return null;

            if (this.IsGameDone) // ������ �������� Ȯ��
            {
                yield break;
            }
        }

        // Game Over
        this.gameOverObj.SetActive(true); // ���� ���� ������Ʈ Ȱ��ȭ
    }

    // ���� ���� �Լ�
    public void OnScore_Func(bool _isCorrect)
    {
        if (_isCorrect) // ����� ��������
        {
            this.score++;
            this.nextNoteGroupUnlockCnt++;

            if (this.noteGroupSpawnConditionScore <= this.nextNoteGroupUnlockCnt) // ��Ʈ �׷� �߰� ���� Ȯ��
            {
                this.nextNoteGroupUnlockCnt = 0;

                NoteSystem_Manager.Instance.OnSpawnNoteGroup_Func(); // 10������ ��Ʈ �׷��� �����϶�� ��Ʈ �ý��� �Ŵ������� ����
            }

            if (this.score >= this.maxScore) // �ִ� ������ �����ߴ��� Ȯ��
            {
                // Game Clear
                this.gameClearObj.SetActive(true); // ���� Ŭ���� ������Ʈ Ȱ��ȭ
            }
        }
        else // ����� ������ �ʾ�����
        {
            this.score--;
            this.nextNoteGroupUnlockCnt--;
        }

        UI_Manager.Instance.OnScore_Func(this.score, this.maxScore); // ���� ����
    }

    // ���� ����� �Լ�
    public void CallBtn_Restart_Func()
    {
        SceneManager.LoadScene(0); // Main ���� �ٽ� �ҷ���
    }
}

