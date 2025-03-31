using System;
using UnityEngine;

public class Boss2 : MonoBehaviour
{


    //보스 몸체, 행성 움직임 구현
    //public int Hp = 120;
    Player player;
    Monster boss2_monster;
    private void Awake()
    {
        //몬스터 스탯 초기화
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

    //데미지 입을 때 수정해야 할 코드
    //방어력만큼 피해를 경감하여 데미지 받음    
    //오버플로우 방지를 위해 double 명시적 변환 후 계산
    
}
