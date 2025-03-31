using UnityEngine;
using UnityEngine.UI;

public class Boss_UI : MonoBehaviour
{ 

    public Image Player_Bar;            //������ ���� �÷��̾� ü�¹� 
    public Image Boss_Bar;              //������ ���� ���� ü�¹�

    public Player player;
    public Monster boss;

    void Start()
    {  
        // �ڵ����� �Ҵ��ϱ�
        if (Player_Bar == null)
        {
            Player_Bar = transform.Find("Player HP Bar").GetComponent<Image>(); // ���� ��ü �� ù ��° Image�� ã�� �Ҵ�
        }

        if (Boss_Bar == null)
        {
            // ���� ��ü �߿��� "Boss_Bar"��� �̸��� �̹��� ã��
            Boss_Bar = transform.Find("Boss Bar").GetComponent<Image>();
        } 
    }

    void Update()
    {
        UpdatePlayerBar();
        UpdateBossBar();
    }
    private void OnEnable()
    {
        //�÷��̾�� ���� ã��
        player = FindFirstObjectByType<Player>();
        boss = GameObject.Find("Boss").GetComponent<Monster>();
        UpdatePlayerBar();
        UpdateBossBar();
    }

    //�÷��̾� ü�¹� ������Ʈ
    void UpdatePlayerBar()
    {
        Player_Bar.fillAmount = player.currentHp / player.maxHp;
        Debug.Log("Player HP : " + player.currentHp);
        Debug.Log("Player Max HP : " + player.maxHp);
        Debug.Log("Player HP Bar : " + Player_Bar.fillAmount);  
    }

    // ���� ü�¹� ������Ʈ
    void UpdateBossBar()
    {
        Boss_Bar.fillAmount = boss.currentHp / boss.maxHp;
        Debug.Log("Boss HP : " + boss.currentHp);
        Debug.Log("Boss Max HP : " + boss.maxHp);
        Debug.Log("Boss HP Bar : " + Boss_Bar.fillAmount);
    }
}

 

