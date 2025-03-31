using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UI_GameManager : MonoBehaviour
{
    public static UI_GameManager instance = null;
    //�¾�
    public Image Sun_HPBar;             //ü�� ������
    /*
    public float Sun_maxHP = 1000f;     //�ִ� ü��
    public float Sun_nowHP;             //���� ü��

    //Player ü��
    public Image P_HPBar;
    public float P_maxHP = 100f;
    public float P_nowHP;
    */
    //Player ����ġ
    public float maxEXP = 100f;
    public float nowEXP;                
    public Image EXP;                   //����ġ �������� ���ӿ�����Ʈ

    //Player ������
    public int level = 1;               //���� ���� 1
    public Text level_text;
    public float overExp = 0;

    //����ġ ī�帮��Ʈ
    public UI_CardManager cardlist;
    public bool cardlistOn = true;
    public GameObject GameOver_Canvas;  //���� ���� ĵ����
    public GameObject SunCanvas;  //���� ���� ĵ����
    public GameObject BossCanvas;  //���� ĵ����  

    private void Awake()
    {
        if (instance == null) //�������� �ڽ��� üũ�Ѵ�. null����
        {
            instance = this; //�ڱ� �ڽ��� �����Ѵ�.
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
         
    }

    private void Start()
    {/*
        Sun_nowHP = Sun_maxHP;
        P_nowHP = P_maxHP;          //���� �ÿ� ���� ü�°� �ִ� ü�� �����ϰ�
    */
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            level = 1;                  //���� ���� 1�� ����
            cardlistOn = true;
            overExp = 0;
        }
    }
    /*
    //�¾� ü�� ����
    public void TakeDamage(float damage)
    {
        Sun_nowHP -= damage;    //������ ��ŭ ü�� ����
        Sun_nowHP = Mathf.Clamp(Sun_nowHP, 0, Sun_maxHP);       //���� ü���� 0 ������ �������� �ʰ�

        if (Sun_nowHP <= 0)
        {
            GameOver(); //0 ���Ϸ� �������� GameOver
        }
    }

    //�÷��̾� ü�� ���� -> ���� �÷��̾� ������ �� ��� .. �ʿ��ϴٸ� ���ñ�
    public void PlayerTakeDamage(float P_damage)
    {
        P_nowHP -= P_damage;    //������ ��ŭ ü�� ����
        P_nowHP = Mathf.Clamp(P_nowHP, 0, P_maxHP);       //���� ü���� 0 ������ �������� �ʰ�

        if (P_nowHP <= 0)
        {
            GameOver(); //0 ���Ϸ� �������� GameOver
        }
    }
    */
    
    //������ UI ����
    public void OnBossCanvas()
    {
        SunCanvas.SetActive(false);
        BossCanvas.SetActive(true);
    }    

    //���͸� ó���� �� ����ġ �����ϴ� �ڵ�
    public void MonsterKill(float MonsterEXP)
    {
        //���� ����ġ��ŭ ����
        nowEXP += MonsterEXP;

        if (nowEXP >= maxEXP)
        {
            levelUP();
        }
    }

    //�÷��̾� ����ġ ����
    public void levelUP()
    {
        AudioManager.Instance.PlaySFX("System", "System_LevelUp");
        level++;                        //������
        level_text.text = level + "";   //�������ϸ� ���� �ö󰡴� �ؽ�Ʈ ǥ��
        nowEXP -= maxEXP;      //max ����ġ ������ ������ �� ���� ������ ������
        overExp = maxEXP += (level * 20); //�������ϸ� �ִ� ����ġ ����
        maxEXP = 10000f;        //�ִ� ����ġ 10000���� ���� (�߰� ������ ����)
        cardlist.DrawCards();           //�������ϸ� ī�� ����Ʈ ���
        cardlistOn = true;
        GameManager.Instance.PauseGame(1.37f); //ī�� ����Ʈ ����ϸ� ���� �Ͻ�����
    }

    public void SetExp()
    {
        maxEXP = overExp;
        cardlistOn = false;
    }  

    //�¾� ü�� ������Ʈ
    public void UpdateSunHP(float Sun_nowHP, float Sun_maxHP)
    {
        Sun_HPBar.fillAmount = Sun_nowHP / Sun_maxHP;
    }

    //�÷��̾� ����ġ ������Ʈ
    public void UpdatePlayerEXP()
    {
        EXP.fillAmount = nowEXP / maxEXP;
    } 

    //���� ����
    public void GameOver()
    {
        if (GameOver_Canvas == null)
        {
            return;
        }
        Instantiate(GameOver_Canvas); //���ӿ��� ������Ʈ ���
    }
}
