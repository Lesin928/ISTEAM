using UnityEngine;

// Future (미래) 시대 스폰 매니저 → 무한 스폰 전용
public class FutureSpawnManager : MonoBehaviour
{
    public FutureSpawn factory;     // 몬스터 생성 역할을 담당할 FutureSpawn 팩토리
    public bool autoStart = true;   // 시작하자마자 자동으로 스폰할지 여부 (인스펙터에서 조정 가능)

    void Start()
    {
        if (autoStart)                         // autoStart가 true일 경우
        {
            factory.BeginSpawning();           // 팩토리에게 스폰 시작 요청 (무한 루프 내부에서 돌아감)
        }
    }

    // 외부에서 수동으로 스폰을 중단하고 싶을 때 호출하는 함수
    public void StopSpawning()
    {
        factory.StopSpawning();                // 팩토리에게 스폰 중지 요청
    }
}
