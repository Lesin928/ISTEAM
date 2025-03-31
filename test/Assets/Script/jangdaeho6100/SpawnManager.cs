using System.Collections;         // �ڷ�ƾ ����� ���� ���ӽ����̽�
using UnityEngine;               // Unity ���� ��� ���

// ���������� ���� ��ȯ�� �����ϴ� �Ŵ��� Ŭ����
public class SpawnManager : MonoBehaviour
{
    // �������� ���� ��� Ŭ����
    [System.Serializable]
    public class Stage
    {
        public string name;              // �������� �̸� (������ or �ν����� ǥ��)
        public SpawnFactory factory;     // �ش� ������������ ����� ���� ���� ���丮
        public float duration = 30f;     // �������� ���� �ð� (��)
    }

    public Stage[] stages;               // ��ü �������� ����Ʈ (�ν����Ϳ� �迭�� ����)
    public float delayBeforeStart = 2f;  // ���� ���� ���� ����� �ð�
    public float betweenStagesDelay = 3f;// �������� ���� ���� �ð�

    private int currentStage = 0;        // ���� �� ��° ������������ ���

    // ���� ���� �� �ڵ� ȣ��
    void Start()
    {
        StartCoroutine(StageRoutine()); // �������� ���� ��ƾ ����
    }

    // ���������� ���������� �����ϴ� �ڷ�ƾ
    IEnumerator StageRoutine()
    {
        yield return new WaitForSeconds(delayBeforeStart); // ���� �� ���

        // ��� ���������� ������� ����
        while (currentStage < stages.Length)
        {
            var stage = stages[currentStage];               // ���� �������� ��������
            Debug.Log($"[Stage] {stage.name} ����");        // ����� �α�

            stage.factory.BeginSpawning();                  // �ش� ���丮���� ���� ���� ����
            yield return new WaitForSeconds(stage.duration);// ������ �ð� ���� ���� ����
            stage.factory.StopSpawning();                   // ���� �ߴ�

            ClearAllMonsters();                             // �����ִ� ���� ����
            currentStage++;                                 // ���� ���������� �Ѿ
            yield return new WaitForSeconds(betweenStagesDelay); // ���� ������������ ���
        }

        Debug.Log("��� �������� �Ϸ�!"); // ������ �������� ������ �� ���
    }

    // ���� �����ϴ� ��� ���� ����
    void ClearAllMonsters()
    {
        foreach (BaseMonster bm in FindObjectsByType<BaseMonster>(FindObjectsSortMode.None))
        {
            Destroy(bm.gameObject); // ���� ������Ʈ ����
        }
    }
}
