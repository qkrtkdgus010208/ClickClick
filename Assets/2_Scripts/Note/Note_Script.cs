using UnityEngine;

public class Note_Script : MonoBehaviour
{
    [SerializeField] private SpriteRenderer srdr; // 노트의 스프라이트 렌더러
    [SerializeField] private Sprite appleSprite; // 사과 스프라이트
    [SerializeField] private Sprite blueberrySprite; // 블루베리 스프라이트
    private bool isApple; // 현재 노트가 사과인지 여부

    // 노트 스크립트 활성화 함수
    public void Activate_Func(bool _isApple)
    {
        this.isApple = _isApple; // 사과 여부 설정

        this.srdr.sprite = _isApple ? this.appleSprite : this.blueberrySprite; // 사과면 사과 스프라이트, 아니면 블루베리 스프라이트 설정
    }

    // 노트 입력 처리 함수
    public void OnInput_Func(bool _isSelected)
    {
        if (_isSelected) // 선택된 노트이면
        {
            bool _isCorrect = (this.isApple == true); // 사과를 눌렀는지 여부 확인
            GameSystem_Manager.Instance.OnScore_Func(_isCorrect); // 게임 시스템 매니저에게 전달
        }

        this.Deactive_Func(); // 노트 삭제
    }

    // 노트 삭제 함수
    public void Deactive_Func()
    {
        GameObject.Destroy(this.gameObject); // 노트 게임 오브젝트 삭제
    }
}

