using System;
using UnityEngine;

public class Boss2 : MonoBehaviour
{


    //���� ��ü, �༺ ������ ����
    //public int Hp = 120;
    Player player;
    Monster boss2_monster;
    private void Awake()
    {
        //���� ���� �ʱ�ȭ
        boss2_monster = GetComponent<Monster>();
        player = FindFirstObjectByType<Player>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //������ ���� �� �����ؾ� �� �ڵ�
    //���¸�ŭ ���ظ� �氨�Ͽ� ������ ����    
    //�����÷ο� ������ ���� double ����� ��ȯ �� ���
    
}
