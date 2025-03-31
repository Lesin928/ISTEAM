using System.Collections;
using UnityEngine;

// ����(Modern) �ô� ���� ���� ���� �Ŵ���
public class ModernSpawnManager : MonoBehaviour
{
    public ModernSpawn factory;         // ���͸� ������ ���丮 (ModernSpawn ������ ����)
    public float spawnDuration = 30f;   // ���͸� �� �� ���� �������� ���� (���� ���� �ð�)
    public float destroyDelay = 2f;     // ������ ���� �� ���� ���ű��� ��ٸ� �ð�
    public bool autoStart = true;       // ���� ���� �� �ڵ����� ������ �������� ����

    void Start()
    {
        if (autoStart)                          // autoStart�� true�� ���
            StartCoroutine(SpawnRoutine());     // ���� ��ƾ �ڷ�ƾ ����
    }

    // ���� �� ��� �� ���ű��� ��ü �帧�� ����ϴ� �ڷ�ƾ
    IEnumerator SpawnRoutine()
    {
        factory.BeginSpawning();                // ���丮���� ���� ���� ���
        yield return new WaitForSeconds(spawnDuration); // ���� ���� �ð���ŭ ���
        factory.StopSpawning();                 // ���丮���� ���� �ߴ� ���

        yield return new WaitForSeconds(destroyDelay); // ���� �� ������
        ClearAllMonsters();                     // ��� ���� ������Ʈ ����
    }

    // ���� �����ϴ� ��� ���� ���� �Լ�
    void ClearAllMonsters()
    {
        // BaseMonster�� ��ӹ��� ��� ������Ʈ ã��
        foreach (BaseMonster m in FindObjectsByType<BaseMonster>(FindObjectsSortMode.None))
        {
            Destroy(m.gameObject);              // �ش� ���� ������Ʈ ����
        }
    }
}
