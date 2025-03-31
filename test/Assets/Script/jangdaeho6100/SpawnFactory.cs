using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 시대 스폰 팩토리의 공통 부모 클래스
public abstract class SpawnFactory : MonoBehaviour
{
    [Header("생성 가능한 몬스터 인덱스 (MobObjectPool 기준)")]
    [SerializeField] protected int[] allowedMonsterIndices;

    [Header("스폰 위치 범위")]
    [SerializeField] protected float spawnMinX = -5f;
    [SerializeField] protected float spawnMaxX = 5f;
    [SerializeField] protected float spawnMinY = -3f;
    [SerializeField] protected float spawnMaxY = 3f;

    [Header("스폰 속도 조절")]
    [SerializeField] protected float initialSpawnInterval = 2f;
    [SerializeField] protected float minSpawnInterval = 0.5f;

    [Header("한 번에 생성할 몬스터 수")]
    [SerializeField] protected int minSpawnCount = 1;           // 현재 최소 생성 수
    [SerializeField] protected int maxSpawnCount = 3;           // 현재 최대 생성 수

    [Header("몬스터 수 증가 설정")] // 
    [SerializeField] protected int minSpawnLimit = 1;           // 증가 시작값 (최소)
    [SerializeField] protected int maxSpawnLimit = 10;          // 증가 끝값 (최대)

    [Header("스테이지 제한 시간 (초)")] // 
    [SerializeField] protected float stageDuration = 120f;      // 1스테이지: 120, 2스테이지: 180, ...

    protected float spawnInterval;
    protected float elapsedTime = 0f;
    private Coroutine spawnCoroutine;

    protected MobObjectPool mobPool;

    // 팩토리 시작 시 실행
    protected virtual void Start()
    {
        spawnInterval = initialSpawnInterval;
        mobPool = FindAnyObjectByType<MobObjectPool>();
    }

    // 매 프레임마다 실행
    protected virtual void Update()
    {
        elapsedTime += Time.deltaTime;

        // 스폰 간격 점점 감소 (기존)
        spawnInterval = Mathf.Max(minSpawnInterval, initialSpawnInterval - elapsedTime / 30f);

        // 시간에 따라 스폰 개수 증가 (stageDuration까지 점진적 증가)
        float t = Mathf.Clamp01(elapsedTime / stageDuration);  // 0 ~ 1 비율로 제한
        minSpawnCount = Mathf.RoundToInt(Mathf.Lerp(minSpawnLimit, maxSpawnLimit / 2f, t));
        maxSpawnCount = Mathf.RoundToInt(Mathf.Lerp(minSpawnLimit + 1, maxSpawnLimit, t));
    }

    public void BeginSpawning()
    {
        if (spawnCoroutine == null)
            spawnCoroutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            CreateMonster();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public virtual void CreateMonster()
    {
        if (mobPool == null || allowedMonsterIndices == null || allowedMonsterIndices.Length == 0)
            return;

        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            int index = allowedMonsterIndices[Random.Range(0, allowedMonsterIndices.Length)];

            Vector3 spawnPos = new Vector3(
                Random.Range(spawnMinX, spawnMaxX),
                Random.Range(spawnMinY, spawnMaxY),
                0f
            );

            GameObject monster = mobPool.GetFromPool(index, spawnPos);
        }
    }
}

