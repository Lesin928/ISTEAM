using System.Collections;
using UnityEngine;

// ���(Ancient) ���� ���� ���� ���� �Ŵ��� Ŭ����
public class AncientSpawnManager : MonoBehaviour
{
    public AncientSpawn factory;          // ���� ���� ���丮 �� ���� ���͸� �����ϴ� ����
    public float spawnDuration = 30f;     // �� �� ���� ���͸� �������� �����ϴ� �ð�
    public float destroyDelay = 2f;       // ���� ���� �� �߰��� ����� �ð�
    public bool autoStart = true;         // ���� �� �ڵ����� ������ �������� ���� (üũ�ڽ�)

    void Start()
    {
        if (autoStart)                    // autoStart�� true�̸�
            StartCoroutine(SpawnRoutine()); // �ڷ�ƾ ���� �� ���� ���� ��ƾ ����
    }

    // ���͸� �����ϰ�, ������ �ð� �ڿ� �����ϴ� ��ü �帧 �ڷ�ƾ
    IEnumerator SpawnRoutine()
    {
        factory.BeginSpawning();           // ���丮���� ���� ���� ���� ��û
        yield return new WaitForSeconds(spawnDuration); // spawnDuration ��ŭ ���
        factory.StopSpawning();            // ���丮���� ���� �ߴ� ��û

        yield return new WaitForSeconds(destroyDelay); // ���͸� �����ϱ� �� ��� �ð�
        ClearAllMonsters();                // ���� ���� �����ϴ� ���͸� ��� ����
    }

    // ���� �� �� ��� ���� ������Ʈ�� ã�� �����ϴ� �Լ�
    void ClearAllMonsters()
    {
        // BaseMonster Ÿ���� ���� ��� ������Ʈ�� ã�Ƽ� �ݺ�
        foreach (BaseMonster m in FindObjectsByType<BaseMonster>(FindObjectsSortMode.None))
        {
            Destroy(m.gameObject);         // �ش� ������Ʈ�� ���� (������ ���ŵ�)
        }
    }
}
