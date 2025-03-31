using UnityEngine;

// SpawnFactory�� ��ӹ޴� �߼� �ô� ���� ���� Ŭ����
public class MedievalSpawn : SpawnFactory
{
    // ��ũ��Ʈ�� ���۵� �� �ڵ����� ȣ��Ǵ� �Լ�
    protected override void Start()
    {
        base.Start(); // �θ� Ŭ����(SpawnFactory)�� Start() ���� �� ������Ʈ Ǯ ã�� �� �ʱ�ȭ ����

        // �� ���丮�� ������ �� �ִ� ������ �ε��� ��� ����
        // MobObjectPool�� monsterPrefabs �迭���� 3, 4, 5, 6�� �����ո� ���� ����
        allowedMonsterIndices = new int[] { 3, 4, 5, 6 };
    }
}
