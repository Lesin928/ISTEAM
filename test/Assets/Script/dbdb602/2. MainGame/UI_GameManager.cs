using UnityEngine;
using UnityEngine.UI;

public class UI_GameManager : MonoBehaviour
{
    public static UI_GameManager instance = null;

    //태양
    public Image Sun_HPBar;             //체력 게이지
    public float Sun_maxHP = 1000f;     //최대 체력
    public float Sun_nowHP;             //현재 체력

    //Player 체력
    public Image P_HPBar;
    public float P_maxHP = 100f;
    public float P_nowHP;

    //Player 경험치
    public float maxEXP = 100f;
    public float nowEXP;
    public Image EXP;                   //경험치 게이지

    //Player 레벨업
    public int level = 1;               //기존 레벨 1
    public Text level_text;


    public GameObject GameOver_Canvas;  //게임 오버 캔버스


    private void Awake()
    {
        if (instance == null) //정적으로 자신을 체크한다. null인지
        {
            instance = this; //자기 자신을 저장한다.
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        //다른 Scene으로 넘어가도 삭제 되지 않기
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Sun_nowHP = Sun_maxHP;
        P_nowHP = P_maxHP;          //시작 시에 현재 체력과 최대 체력 동일하게

        level = 1;

    }

    //태양 체력 감소
    public void TakeDamage(float damage)
    {
        Sun_nowHP -= damage;            //데미지 만큼 체력 감소
        Sun_nowHP = Mathf.Clamp(Sun_nowHP, 0, Sun_maxHP);       //현재 체력을 0 밑으로 떨어지지 않게

        if (Sun_nowHP <= 0)
        {
            GameOver(); //0 이하로 떨어지면 GameOver
        }
    }

    public void PlayerTakeDamage(float P_damage)
    {
        P_nowHP -= P_damage;            //데미지 만큼 체력 감소
        P_nowHP = Mathf.Clamp(P_nowHP, 0, P_maxHP);       //현재 체력을 0 밑으로 떨어지지 않게

        if (P_nowHP <= 0)
        {
            GameOver(); //0 이하로 떨어지면 GameOver
        }
    }

    //몬스터를 처지할 때 경험치 증가하는 코드
    public void MonsterKill(float MonsterEXP)
    {
        //몬스터 경험치만큼 증가
        nowEXP += MonsterEXP;

        if (nowEXP >= maxEXP)
        {
            levelUP();
        }
    }

    //플레이어 경험치 증가
    public void levelUP()
    {
        level++;                        //레벨업
        nowEXP -= maxEXP;               //max 경험치 넘으면 나머지 값 다음 레벨로 보내기

        level_text.text = level + "";   //레벨업하면 숫자 올라가는 텍스트 표시

        maxEXP += (level * 30);         //레벨업하면 최대 경험치 증가
    }


    //태양 체력 업데이트
    public void UpdateSunHP()
    {
        Sun_HPBar.fillAmount = Sun_nowHP / Sun_maxHP;
    }

    //플레이어 경험치 업데이트
    public void UpdatePlayerEXP()
    {
        EXP.fillAmount = nowEXP / maxEXP;
    }




    //게임 오버
    private void GameOver()
    {
        if (GameOver_Canvas == null)
        {
            return;
        }

        Instantiate(GameOver_Canvas);
    }
}
