using UnityEngine;
using UnityEngine.UI;

public class Boss_UI : MonoBehaviour
{
    private UI_GameManager gameManager;


    public Image Player_Bar;            //보스전 때의 플레이어 체력바

    void Start()
    {
        //UI_GameManager 인스턴스 가져오기
        gameManager = UI_GameManager.instance;

        if (gameManager == null)
        {
            Debug.LogError("안됨;");
            return;
        }

        UpdatePlayerBar();
    }


    void Update()
    {
        
    }

    void UpdatePlayerBar()
    {
        Player_Bar.fillAmount = gameManager.P_nowHP / gameManager.P_maxHP;
    }
}
