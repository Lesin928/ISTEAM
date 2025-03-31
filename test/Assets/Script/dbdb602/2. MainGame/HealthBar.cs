using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public UI_GameManager gameManager;

    void Start()
    {
        gameManager = UI_GameManager.instance;
    }

    //임시 테스트: Q키를 누르면 데미지 10 입히기
    void Update()
    {/*
        // Q 키가 눌리면 데미지 10을 입힘
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UI_GameManager.instance.TakeDamage(10f);
            Debug.Log("10데미지를 입었습니다.");
        }

        UI_GameManager.instance.UpdateSunHP();*/
    }
}
