using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� �ô� ���� ���丮�� ���� �θ� Ŭ����
public abstract class SpawnFactory : MonoBehaviour
{
    [Header("���� ������ ���� �ε��� (MobObjectPool ����)")]
    [SerializeField] protected int[] allowedMonsterIndices;

    [Header("���� ��ġ ����")]
    [SerializeField] protected float spawnMinX = -5f;
    [SerializeField] protected float spawnMaxX = 5f;
    [SerializeField] protected float spawnMinY = -3f;
    [SerializeField] protected float spawnMaxY = 3f;

    [Header("���� �ӵ� ����")]
    [SerializeField] protected float initialSpawnInterval = 2f;
    [SerializeField] protected float minSpawnInterval = 0.5f;

    [Header("�� ���� ������ ���� ��")]
    [SerializeField] protected int minSpawnCount = 1;           // ���� �ּ� ���� ��
    [SerializeField] protected int maxSpawnCount = 3;           // ���� �ִ� ���� ��

    [Header("���� �� ���� ����")] // 
    [SerializeField] protected int minSpawnLimit = 1;           // ���� ���۰� (�ּ�)
    [SerializeField] protected int maxSpawnLimit = 10;          // ���� ���� (�ִ�)

    [Header("�������� ���� �ð� (��)")] // 
    [SerializeField] protected float stageDuration = 120f;      // 1��������: 120, 2��������: 180, ...

    protected float spawnInterval;
    protected float elapsedTime = 0f;
    private Coroutine spawnCoroutine;

    protected MobObjectPool mobPool;

    // ���丮 ���� �� ����
    protected virtual void Start()
    {
        spawnInterval = initialSpawnInterval;
        mobPool = FindAnyObjectByType<MobObjectPool>();
    }

    // �� �����Ӹ��� ����
    protected virtual void Update()
    {
        elapsedTime += Time.deltaTime;

        // ���� ���� ���� ���� (����)
        spawnInterval = Mathf.Max(minSpawnInterval, initialSpawnInterval - elapsedTime / 30f);

        // �ð��� ���� ���� ���� ���� (stageDuration���� ������ ����)
        float t = Mathf.Clamp01(elapsedTime / stageDuration);  // 0 ~ 1 ������ ����
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

