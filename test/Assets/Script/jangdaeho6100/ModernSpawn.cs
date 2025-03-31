using UnityEngine;

// SpawnFactory를 상속받는 현대(Modern) 몬스터 스폰 클래스
public class ModernSpawn : SpawnFactory
{
    // Start는 Unity에서 스크립트가 시작될 때 자동으로 호출됨
    protected override void Start()
    {
        base.Start(); // 부모 클래스 SpawnFactory의 Start() 호출 → 오브젝트 풀 찾기, 초기 스폰 설정 등

        // 이 팩토리는 Modern 시대 전용 몬스터만 생성하도록 설정
        // MobObjectPool의 monsterPrefabs 배열에서 인덱스 7~11번 몬스터만 소환됨
        allowedMonsterIndices = new int[] { 7, 8, 9, 10, 11 };
    }
}
