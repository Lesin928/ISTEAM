using UnityEngine;
using UnityEngine.UI;

public class Player_EXP : MonoBehaviour
{
    private UI_GameManager gameManager;

    void Start()
    {
        gameManager = UI_GameManager.instance;
    }


    //�ӽ� �׽�Ʈ: WŰ�� ������ ����ġ ����
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UI_GameManager.instance.MonsterKill(10f); //�ӽ÷� 10���� ����
        }

    }
}
