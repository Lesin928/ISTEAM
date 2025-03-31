using System.Collections;
using UnityEngine;

// 현대(Modern) 시대 몬스터 스폰 전용 매니저
public class ModernSpawnManager : MonoBehaviour
{
    public ModernSpawn factory;         // 몬스터를 생성할 팩토리 (ModernSpawn 프리팹 참조)
    public float spawnDuration = 30f;   // 몬스터를 몇 초 동안 생성할지 설정 (스폰 유지 시간)
    public float destroyDelay = 2f;     // 스폰이 끝난 뒤 몬스터 제거까지 기다릴 시간
    public bool autoStart = true;       // 게임 시작 시 자동으로 스폰을 시작할지 여부

    void Start()
    {
        if (autoStart)                          // autoStart가 true일 경우
            StartCoroutine(SpawnRoutine());     // 스폰 루틴 코루틴 시작
    }

    // 스폰 → 대기 → 제거까지 전체 흐름을 담당하는 코루틴
    IEnumerator SpawnRoutine()
    {
        factory.BeginSpawning();                // 팩토리에게 스폰 시작 명령
        yield return new WaitForSeconds(spawnDuration); // 스폰 유지 시간만큼 대기
        factory.StopSpawning();                 // 팩토리에게 스폰 중단 명령

        yield return new WaitForSeconds(destroyDelay); // 제거 전 딜레이
        ClearAllMonsters();                     // 모든 몬스터 오브젝트 제거
    }

    // 씬에 존재하는 모든 몬스터 제거 함수
    void ClearAllMonsters()
    {
        // BaseMonster를 상속받은 모든 오브젝트 찾기
        foreach (BaseMonster m in FindObjectsByType<BaseMonster>(FindObjectsSortMode.None))
        {
            Destroy(m.gameObject);              // 해당 몬스터 오브젝트 삭제
        }
    }
}
