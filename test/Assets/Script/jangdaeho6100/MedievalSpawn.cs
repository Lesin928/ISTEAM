using UnityEngine;

// SpawnFactory를 상속받는 중세 시대 몬스터 스폰 클래스
public class MedievalSpawn : SpawnFactory
{
    // 스크립트가 시작될 때 자동으로 호출되는 함수
    protected override void Start()
    {
        base.Start(); // 부모 클래스(SpawnFactory)의 Start() 실행 → 오브젝트 풀 찾기 등 초기화 수행

        // 이 팩토리가 생성할 수 있는 몬스터의 인덱스 목록 설정
        // MobObjectPool의 monsterPrefabs 배열에서 3, 4, 5, 6번 프리팹만 생성 가능
        allowedMonsterIndices = new int[] { 3, 4, 5, 6 };
    }
}
