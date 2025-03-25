using UnityEngine;
using UnityEngine.UI;

public class UI_GameManager : MonoBehaviour
{
    public static UI_GameManager instance = null;

    //�¾�
    public Image Sun_HPBar;             //ü�� ������
    public float Sun_maxHP = 1000f;     //�ִ� ü��
    public float Sun_nowHP;             //���� ü��

    //Player ü��
    public Image P_HPBar;
    public float P_maxHP = 100f;
    public float P_nowHP;

    //Player ����ġ
    public float maxEXP = 100f;
    public float nowEXP;
    public Image EXP;                   //����ġ ������

    //Player ������
    public int level = 1;               //���� ���� 1
    public Text level_text;


    public GameObject GameOver_Canvas;  //���� ���� ĵ����


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

        //�ٸ� Scene���� �Ѿ�� ���� ���� �ʱ�
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Sun_nowHP = Sun_maxHP;
        P_nowHP = P_maxHP;          //���� �ÿ� ���� ü�°� �ִ� ü�� �����ϰ�

        level = 1;

    }

    //�¾� ü�� ����
    public void TakeDamage(float damage)
    {
        Sun_nowHP -= damage;            //������ ��ŭ ü�� ����
        Sun_nowHP = Mathf.Clamp(Sun_nowHP, 0, Sun_maxHP);       //���� ü���� 0 ������ �������� �ʰ�

        if (Sun_nowHP <= 0)
        {
            GameOver(); //0 ���Ϸ� �������� GameOver
        }
    }

    public void PlayerTakeDamage(float P_damage)
    {
        P_nowHP -= P_damage;            //������ ��ŭ ü�� ����
        P_nowHP = Mathf.Clamp(P_nowHP, 0, P_maxHP);       //���� ü���� 0 ������ �������� �ʰ�

        if (P_nowHP <= 0)
        {
            GameOver(); //0 ���Ϸ� �������� GameOver
        }
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
        level++;                        //������
        nowEXP -= maxEXP;               //max ����ġ ������ ������ �� ���� ������ ������

        level_text.text = level + "";   //�������ϸ� ���� �ö󰡴� �ؽ�Ʈ ǥ��

        maxEXP += (level * 30);         //�������ϸ� �ִ� ����ġ ����
    }


    //�¾� ü�� ������Ʈ
    public void UpdateSunHP()
    {
        Sun_HPBar.fillAmount = Sun_nowHP / Sun_maxHP;
    }

    //�÷��̾� ����ġ ������Ʈ
    public void UpdatePlayerEXP()
    {
        EXP.fillAmount = nowEXP / maxEXP;
    }




    //���� ����
    private void GameOver()
    {
        if (GameOver_Canvas == null)
        {
            return;
        }

        Instantiate(GameOver_Canvas);
    }
}
