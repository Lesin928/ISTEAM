using UnityEngine;
using UnityEngine.UI;

public class Boss_UI : MonoBehaviour
{
    private UI_GameManager gameManager;


    public Image Player_Bar;            //������ ���� �÷��̾� ü�¹�

    void Start()
    {
        //UI_GameManager �ν��Ͻ� ��������
        gameManager = UI_GameManager.instance;

        if (gameManager == null)
        {
            Debug.LogError("�ȵ�;");
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
