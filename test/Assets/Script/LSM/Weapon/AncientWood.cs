using UnityEngine;

//��빫�� ���� ���ݹ�� ������ �ڵ�
//������ƮǮ���� ���� ���� ����Ʈ �߻�
public class AncientWood : Weapon
{
    public WeaponObjectPool effectPool; // ����Ʈ Ǯ�� �ý���

    public override void WeaponAttack() //���⺰ �߻��� ����
    {
        GameObject effect = effectPool.GetFromPool();
    }
}