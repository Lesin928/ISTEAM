using UnityEngine;

public class BossForBG : MonoBehaviour
{
    public int maxHP = 100; //보스의 최대 HP
    private int currentHP;  //보스의 현재 HP

    [HideInInspector] public StageManagerForBG stageManager;  //StageManagerForBG 스크립트 참조

    void Start()
    {
        //보스의 HP를 최대 HP로 초기화
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        //대미지 입으면 HP 감소
        currentHP -= damage;

        //HP가 0 이하가 되면
        if (currentHP <= 0)
        {
            //보스 죽음
            Die();
        }
    }

    //보스 죽음 함수
    void Die()
    {
        if (stageManager != null)
        {
            //보스가 죽으면 StageManager에 알리기
            stageManager.OnBossDeath();
        }
        //보스 오브젝트 삭제
        Destroy(gameObject);
    }
}