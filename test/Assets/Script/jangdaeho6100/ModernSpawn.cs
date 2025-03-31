using UnityEngine;

// SpawnFactory�� ��ӹ޴� ����(Modern) ���� ���� Ŭ����
public class ModernSpawn : SpawnFactory
{
    // Start�� Unity���� ��ũ��Ʈ�� ���۵� �� �ڵ����� ȣ���
    protected override void Start()
    {
        base.Start(); // �θ� Ŭ���� SpawnFactory�� Start() ȣ�� �� ������Ʈ Ǯ ã��, �ʱ� ���� ���� ��

        // �� ���丮�� Modern �ô� ���� ���͸� �����ϵ��� ����
        // MobObjectPool�� monsterPrefabs �迭���� �ε��� 7~11�� ���͸� ��ȯ��
        allowedMonsterIndices = new int[] { 7, 8, 9, 10, 11 };
    }
}
