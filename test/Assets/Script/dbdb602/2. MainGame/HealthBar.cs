using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public UI_GameManager gameManager;

    void Start()
    {
        gameManager = UI_GameManager.instance;
    }

    //�ӽ� �׽�Ʈ: QŰ�� ������ ������ 10 ������
    void Update()
    {/*
        // Q Ű�� ������ ������ 10�� ����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UI_GameManager.instance.TakeDamage(10f);
            Debug.Log("10�������� �Ծ����ϴ�.");
        }

        UI_GameManager.instance.UpdateSunHP();*/
    }
}
