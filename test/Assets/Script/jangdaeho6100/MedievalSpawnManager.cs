using System.Collections;
using UnityEngine;

// �߼�(Medieval) �ô� ���� ���� ���� ���� �Ŵ���
public class MedievalSpawnManager : MonoBehaviour
{
    public MedievalSpawn factory;        // ���� ���� ��� ���丮 �� MedievalSpawn ������ ����
    public float spawnDuration = 30f;    // ���͸� �� �� ���� �������� ���� (���� ���� �ð�)
    public float destroyDelay = 2f;      // ������ ������ ���� �������� ��ٸ� �ð�
    public bool autoStart = true;        // ������ �� �ڵ����� ������ �������� ���� (üũ�ڽ��� ���� ����)

    void Start()
    {
        if (autoStart)                          // autoStart�� true�� ���
            StartCoroutine(SpawnRoutine());     // �ڷ�ƾ ���� (���� ���� �� ��� �� ���� �帧)
    }

    // ���͸� �����ϰ� �����ϴ� �帧�� ����ϴ� �ڷ�ƾ �Լ�
    IEnumerator SpawnRoutine()
    {
        factory.BeginSpawning();                // ���丮���� ���� ���� ���� ��û
        yield return new WaitForSeconds(spawnDuration); // ������ �ð���ŭ ��� (���� ����)
        factory.StopSpawning();                 // ���丮���� ���� ���� ��û

        yield return new WaitForSeconds(destroyDelay); // ���� ���� �� ��� �ð�
        ClearAllMonsters();                     // ���� ���� �ִ� ��� ���� ����
    }

    // �� �� ��� ���� ������Ʈ�� ã�Ƽ� �����ϴ� �Լ�
    void ClearAllMonsters()
    {
        // BaseMonster�� ��ӹ޴� ��� ������Ʈ�� ã�´�
        foreach (BaseMonster m in FindObjectsByType<BaseMonster>(FindObjectsSortMode.None))
        {
            Destroy(m.gameObject);             // �ش� ���� ������Ʈ ����
        }
    }
}
