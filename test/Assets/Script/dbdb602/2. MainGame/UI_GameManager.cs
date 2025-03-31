using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UI_GameManager : MonoBehaviour
{
    public static UI_GameManager instance = null;
    //태양
    public Image Sun_HPBar;             //체력 게이지
    /*
    public float Sun_maxHP = 1000f;     //최대 체력
    public float Sun_nowHP;             //현재 체력

    //Player 체력
    public Image P_HPBar;
    public float P_maxHP = 100f;
    public float P_nowHP;
    */
    //Player 경험치
    public float maxEXP = 100f;
    public float nowEXP;                
    public Image EXP;                   //경험치 게이지바 게임오브젝트

    //Player 레벨업
    public int level = 1;               //기존 레벨 1
    public Text level_text;
    public float overExp = 0;

    //경험치 카드리스트
    public UI_CardManager cardlist;
    public bool cardlistOn = true;
    public GameObject GameOver_Canvas;  //게임 오버 캔버스
    public GameObject SunCanvas;  //게임 진행 캔버스
    public GameObject BossCanvas;  //보스 캔버스  

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
         
    }

    private void Start()
    {/*
        Sun_nowHP = Sun_maxHP;
        P_nowHP = P_maxHP;          //시작 시에 현재 체력과 최대 체력 동일하게
    */
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            level = 1;                  //기존 레벨 1로 시작
            cardlistOn = true;
            overExp = 0;
        }
    }
    /*
    //태양 체력 감소
    public void TakeDamage(float damage)
    {
        Sun_nowHP -= damage;    //데미지 만큼 체력 감소
        Sun_nowHP = Mathf.Clamp(Sun_nowHP, 0, Sun_maxHP);       //현재 체력을 0 밑으로 떨어지지 않게

        if (Sun_nowHP <= 0)
        {
            GameOver(); //0 이하로 떨어지면 GameOver
        }
    }

    //플레이어 체력 감소 -> 현재 플레이어 감소할 게 없어서 .. 필요하다면 쓰시길
    public void PlayerTakeDamage(float P_damage)
    {
        P_nowHP -= P_damage;    //데미지 만큼 체력 감소
        P_nowHP = Mathf.Clamp(P_nowHP, 0, P_maxHP);       //현재 체력을 0 밑으로 떨어지지 않게

        if (P_nowHP <= 0)
        {
            GameOver(); //0 이하로 떨어지면 GameOver
        }
    }
    */
    
    //보스전 UI 세팅
    public void OnBossCanvas()
    {
        SunCanvas.SetActive(false);
        BossCanvas.SetActive(true);
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
        AudioManager.Instance.PlaySFX("System", "System_LevelUp");
        level++;                        //레벨업
        level_text.text = level + "";   //레벨업하면 숫자 올라가는 텍스트 표시
        nowEXP -= maxEXP;      //max 경험치 넘으면 나머지 값 다음 레벨로 보내기
        overExp = maxEXP += (level * 20); //레벨업하면 최대 경험치 증가
        maxEXP = 10000f;        //최대 경험치 10000으로 설정 (추가 레벨업 방지)
        cardlist.DrawCards();           //레벨업하면 카드 리스트 출력
        cardlistOn = true;
        GameManager.Instance.PauseGame(1.37f); //카드 리스트 출력하면 게임 일시정지
    }

    public void SetExp()
    {
        maxEXP = overExp;
        cardlistOn = false;
    }  

    //태양 체력 업데이트
    public void UpdateSunHP(float Sun_nowHP, float Sun_maxHP)
    {
        Sun_HPBar.fillAmount = Sun_nowHP / Sun_maxHP;
    }

    //플레이어 경험치 업데이트
    public void UpdatePlayerEXP()
    {
        EXP.fillAmount = nowEXP / maxEXP;
    } 

    //게임 오버
    public void GameOver()
    {
        if (GameOver_Canvas == null)
        {
            return;
        }
        Instantiate(GameOver_Canvas); //게임오버 오브젝트 출력
    }
}
