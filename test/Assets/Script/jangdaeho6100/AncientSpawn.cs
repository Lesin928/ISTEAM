using UnityEngine;

// SpawnFactory를 상속한 고대 몬스터 스폰 클래스
public class AncientSpawn : SpawnFactory
{
    // Unity가 스크립트 시작 시 자동으로 호출하는 함수
    protected override void Start()
    {
        base.Start(); // 부모 클래스(SpawnFactory)의 Start() 실행 → 몬스터 풀 초기화 등 수행

        // 이 팩토리가 소환 가능한 몬스터 인덱스를 설정
        // MobObjectPool의 monsterPrefabs 배열 기준으로 0, 1, 2번 프리팹만 생성할 수 있도록 제한
        allowedMonsterIndices = new int[] { 0, 1, 2 };
    }
}
