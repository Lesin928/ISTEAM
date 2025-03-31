using UnityEngine;
using UnityEngine.UI;

public class Boss_UI : MonoBehaviour
{ 

    public Image Player_Bar;            //보스전 때의 플레이어 체력바 
    public Image Boss_Bar;              //보스전 때의 보스 체력바

    public Player player;
    public Monster boss;

    void Start()
    {  
        // 자동으로 할당하기
        if (Player_Bar == null)
        {
            Player_Bar = transform.Find("Player HP Bar").GetComponent<Image>(); // 하위 객체 중 첫 번째 Image를 찾아 할당
        }

        if (Boss_Bar == null)
        {
            // 하위 객체 중에서 "Boss_Bar"라는 이름의 이미지 찾기
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
        //플레이어와 보스 찾기
        player = FindFirstObjectByType<Player>();
        boss = GameObject.Find("Boss").GetComponent<Monster>();
        UpdatePlayerBar();
        UpdateBossBar();
    }

    //플레이어 체력바 업데이트
    void UpdatePlayerBar()
    {
        Player_Bar.fillAmount = player.currentHp / player.maxHp;
        Debug.Log("Player HP : " + player.currentHp);
        Debug.Log("Player Max HP : " + player.maxHp);
        Debug.Log("Player HP Bar : " + Player_Bar.fillAmount);  
    }

    // 보스 체력바 업데이트
    void UpdateBossBar()
    {
        Boss_Bar.fillAmount = boss.currentHp / boss.maxHp;
        Debug.Log("Boss HP : " + boss.currentHp);
        Debug.Log("Boss Max HP : " + boss.maxHp);
        Debug.Log("Boss HP Bar : " + Boss_Bar.fillAmount);
    }
}

 

