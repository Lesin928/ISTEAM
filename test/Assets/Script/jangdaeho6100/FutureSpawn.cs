using UnityEngine;

// SpawnFactory를 상속한 미래 몬스터 스폰 클래스
public class FutureSpawn : SpawnFactory
{
    // Unity에서 스크립트가 활성화될 때 자동으로 호출되는 함수
    protected override void Start()
    {
        base.Start(); // 부모 클래스 SpawnFactory의 Start() 호출 → 몬스터 풀 찾기, 스폰 간격 초기화 등

        // 이 스폰 팩토리가 생성할 수 있는 몬스터 인덱스 목록 설정
        // MobObjectPool의 monsterPrefabs 배열 기준으로 12~16번 프리팹만 생성됨
        allowedMonsterIndices = new int[] { 12, 13, 14, 15, 16 };
    }
}
