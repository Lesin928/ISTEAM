using Unity.Mathematics.Geometry;
using UnityEngine;

//ĳ���͸� ��ӹ޴� ���� Ŭ���� 
//��� ���ʹ� �ش� ��ũ��Ʈ�� ��ӹް� ��ȣ�ۿ���
public class Monster : Character
{
    Player player; 
    public override void SetHp(float hp)
    {
        this.maxHp = hp;
        this.currentHp = hp;
    }

    public override float GetHp()
    {
        return currentHp;
    }

    public override void TakeDamage(float damage)
    {
        player = FindFirstObjectByType<Player>();
        if (UnityEngine.Random.Range(0f, 1f) < player.critical)
        {
            damage *= player.criticalDamage;
        }

        currentHp -= (float)((double)((Mathf.Pow(damage, 2f)) / ((double)armor) + (double)damage));
        
    }
}
