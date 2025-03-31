using UnityEngine;

// SpawnFactory�� ����� �̷� ���� ���� Ŭ����
public class FutureSpawn : SpawnFactory
{
    // Unity���� ��ũ��Ʈ�� Ȱ��ȭ�� �� �ڵ����� ȣ��Ǵ� �Լ�
    protected override void Start()
    {
        base.Start(); // �θ� Ŭ���� SpawnFactory�� Start() ȣ�� �� ���� Ǯ ã��, ���� ���� �ʱ�ȭ ��

        // �� ���� ���丮�� ������ �� �ִ� ���� �ε��� ��� ����
        // MobObjectPool�� monsterPrefabs �迭 �������� 12~16�� �����ո� ������
        allowedMonsterIndices = new int[] { 12, 13, 14, 15, 16 };
    }
}
