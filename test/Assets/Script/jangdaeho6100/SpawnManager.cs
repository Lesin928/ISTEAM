using System.Collections;         // 코루틴 사용을 위한 네임스페이스
using UnityEngine;               // Unity 관련 기능 사용

// 스테이지별 몬스터 소환을 제어하는 매니저 클래스
public class SpawnManager : MonoBehaviour
{
    // 스테이지 정보 담는 클래스
    [System.Serializable]
    public class Stage
    {
        public string name;              // 스테이지 이름 (디버깅용 or 인스펙터 표시)
        public SpawnFactory factory;     // 해당 스테이지에서 사용할 몬스터 생성 팩토리
        public float duration = 30f;     // 스테이지 진행 시간 (초)
    }

    public Stage[] stages;               // 전체 스테이지 리스트 (인스펙터에 배열로 노출)
    public float delayBeforeStart = 2f;  // 게임 시작 전에 대기할 시간
    public float betweenStagesDelay = 3f;// 스테이지 간의 쉬는 시간

    private int currentStage = 0;        // 현재 몇 번째 스테이지인지 기록

    // 게임 시작 시 자동 호출
    void Start()
    {
        StartCoroutine(StageRoutine()); // 스테이지 진행 루틴 실행
    }

    // 스테이지를 순차적으로 진행하는 코루틴
    IEnumerator StageRoutine()
    {
        yield return new WaitForSeconds(delayBeforeStart); // 시작 전 대기

        // 모든 스테이지를 순서대로 실행
        while (currentStage < stages.Length)
        {
            var stage = stages[currentStage];               // 현재 스테이지 가져오기
            Debug.Log($"[Stage] {stage.name} 시작");        // 디버깅 로그

            stage.factory.BeginSpawning();                  // 해당 팩토리에서 몬스터 스폰 시작
            yield return new WaitForSeconds(stage.duration);// 정해진 시간 동안 스폰 지속
            stage.factory.StopSpawning();                   // 스폰 중단

            ClearAllMonsters();                             // 남아있는 몬스터 제거
            currentStage++;                                 // 다음 스테이지로 넘어감
            yield return new WaitForSeconds(betweenStagesDelay); // 다음 스테이지까지 대기
        }

        Debug.Log("모든 스테이지 완료!"); // 마지막 스테이지 끝났을 때 출력
    }

    // 씬에 존재하는 모든 몬스터 삭제
    void ClearAllMonsters()
    {
        foreach (BaseMonster bm in FindObjectsByType<BaseMonster>(FindObjectsSortMode.None))
        {
            Destroy(bm.gameObject); // 몬스터 오브젝트 삭제
        }
    }
}
