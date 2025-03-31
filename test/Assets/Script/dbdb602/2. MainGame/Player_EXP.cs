using UnityEngine;
using UnityEngine.UI;

public class Player_EXP : MonoBehaviour
{
    private UI_GameManager gameManager;

    void Start()
    {
        gameManager = UI_GameManager.instance;
    }


    //임시 테스트: W키를 누르면 경험치 증가
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UI_GameManager.instance.MonsterKill(10f); //임시로 10으로 설정
        }

    }
}
