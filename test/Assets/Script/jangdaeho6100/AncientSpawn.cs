using UnityEngine;

// SpawnFactory�� ����� ��� ���� ���� Ŭ����
public class AncientSpawn : SpawnFactory
{
    // Unity�� ��ũ��Ʈ ���� �� �ڵ����� ȣ���ϴ� �Լ�
    protected override void Start()
    {
        base.Start(); // �θ� Ŭ����(SpawnFactory)�� Start() ���� �� ���� Ǯ �ʱ�ȭ �� ����

        // �� ���丮�� ��ȯ ������ ���� �ε����� ����
        // MobObjectPool�� monsterPrefabs �迭 �������� 0, 1, 2�� �����ո� ������ �� �ֵ��� ����
        allowedMonsterIndices = new int[] { 0, 1, 2 };
    }
}
