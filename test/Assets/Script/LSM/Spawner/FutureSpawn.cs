using UnityEngine;

//FutureSpawn는 4스테이지의 몬스터 스폰을 관리함.
public class FutureSpawn : SpawnFactry
{
    [SerializeField]
    private GameObject[] monsterPrefab;
    void Update()
    {
        //시간을 재고 시간별로 점진적으로 몬스터 생성
    }
    public override void CreateMonster()
    {
        // 시간이 지날수록 강한 몬스터 생성되게 코드 작성  


    }
}
