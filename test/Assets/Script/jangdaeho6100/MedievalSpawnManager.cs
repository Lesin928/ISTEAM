using System.Collections;
using UnityEngine;

// 중세(Medieval) 시대 몬스터 생성 전용 스폰 매니저
public class MedievalSpawnManager : MonoBehaviour
{
    public MedievalSpawn factory;        // 몬스터 생성 담당 팩토리 → MedievalSpawn 프리팹 참조
    public float spawnDuration = 30f;    // 몬스터를 몇 초 동안 생성할지 설정 (스폰 지속 시간)
    public float destroyDelay = 2f;      // 스폰이 끝나고 몬스터 삭제까지 기다릴 시간
    public bool autoStart = true;        // 시작할 때 자동으로 스폰을 시작할지 여부 (체크박스로 설정 가능)

    void Start()
    {
        if (autoStart)                          // autoStart가 true일 경우
            StartCoroutine(SpawnRoutine());     // 코루틴 실행 (스폰 시작 → 대기 → 삭제 흐름)
    }

    // 몬스터를 생성하고 제거하는 흐름을 담당하는 코루틴 함수
    IEnumerator SpawnRoutine()
    {
        factory.BeginSpawning();                // 팩토리에게 몬스터 생성 시작 요청
        yield return new WaitForSeconds(spawnDuration); // 지정한 시간만큼 대기 (스폰 유지)
        factory.StopSpawning();                 // 팩토리에게 스폰 종료 요청

        yield return new WaitForSeconds(destroyDelay); // 몬스터 삭제 전 대기 시간
        ClearAllMonsters();                     // 현재 씬에 있는 모든 몬스터 제거
    }

    // 씬 내 모든 몬스터 오브젝트를 찾아서 삭제하는 함수
    void ClearAllMonsters()
    {
        // BaseMonster를 상속받는 모든 오브젝트를 찾는다
        foreach (BaseMonster m in FindObjectsByType<BaseMonster>(FindObjectsSortMode.None))
        {
            Destroy(m.gameObject);             // 해당 몬스터 오브젝트 제거
        }
    }
}
