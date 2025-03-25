using UnityEngine;

public class TestAttack : MonoBehaviour
{
    public StageManagerForBG stageManager;  //StageManagerForBG 스크립트 참조
    public int testDamage = 50; //기본 공격력

    void Start()
    {
        //StageManagerForBG가 할당되지 않았다면, 현재 오브젝트에서 가져오기
        if (stageManager == null)
        {
            stageManager = GetComponent<StageManagerForBG>();
        }
    }

    void Update()
    {
        //스페이스 바가 눌렸을 때, 보스에게 피해를 주는 기능
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StageManager와 현재 보스가 유효한지 확인
            if (stageManager != null && stageManager.currentBoss != null)
            {
                //보스의 BossForBG 컴포넌트 가져오기
                BossForBG boss = stageManager.currentBoss.GetComponent<BossForBG>();

                if (boss != null)
                {
                    //보스에게 피해 주기
                    boss.TakeDamage(testDamage);
                }
            }
        }
    }
}