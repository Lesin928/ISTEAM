using System.Collections;
using UnityEngine;

// 고대(Ancient) 몬스터 생성 전용 스폰 매니저 클래스
public class AncientSpawnManager : MonoBehaviour
{
    public AncientSpawn factory;          // 몬스터 생성 팩토리 → 실제 몬스터를 생성하는 역할
    public float spawnDuration = 30f;     // 몇 초 동안 몬스터를 생성할지 결정하는 시간
    public float destroyDelay = 2f;       // 몬스터 제거 전 추가로 대기할 시간
    public bool autoStart = true;         // 시작 시 자동으로 스폰을 시작할지 여부 (체크박스)

    void Start()
    {
        if (autoStart)                    // autoStart가 true이면
            StartCoroutine(SpawnRoutine()); // 코루틴 시작 → 몬스터 생성 루틴 실행
    }

    // 몬스터를 생성하고, 정해진 시간 뒤에 삭제하는 전체 흐름 코루틴
    IEnumerator SpawnRoutine()
    {
        factory.BeginSpawning();           // 팩토리에게 몬스터 스폰 시작 요청
        yield return new WaitForSeconds(spawnDuration); // spawnDuration 만큼 대기
        factory.StopSpawning();            // 팩토리에게 스폰 중단 요청

        yield return new WaitForSeconds(destroyDelay); // 몬스터를 제거하기 전 대기 시간
        ClearAllMonsters();                // 현재 씬에 존재하는 몬스터를 모두 제거
    }

    // 현재 씬 내 모든 몬스터 오브젝트를 찾아 제거하는 함수
    void ClearAllMonsters()
    {
        // BaseMonster 타입을 가진 모든 오브젝트를 찾아서 반복
        foreach (BaseMonster m in FindObjectsByType<BaseMonster>(FindObjectsSortMode.None))
        {
            Destroy(m.gameObject);         // 해당 오브젝트를 삭제 (씬에서 제거됨)
        }
    }
}
