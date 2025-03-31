using UnityEngine;

// Future (�̷�) �ô� ���� �Ŵ��� �� ���� ���� ����
public class FutureSpawnManager : MonoBehaviour
{
    public FutureSpawn factory;     // ���� ���� ������ ����� FutureSpawn ���丮
    public bool autoStart = true;   // �������ڸ��� �ڵ����� �������� ���� (�ν����Ϳ��� ���� ����)

    void Start()
    {
        if (autoStart)                         // autoStart�� true�� ���
        {
            factory.BeginSpawning();           // ���丮���� ���� ���� ��û (���� ���� ���ο��� ���ư�)
        }
    }

    // �ܺο��� �������� ������ �ߴ��ϰ� ���� �� ȣ���ϴ� �Լ�
    public void StopSpawning()
    {
        factory.StopSpawning();                // ���丮���� ���� ���� ��û
    }
}
