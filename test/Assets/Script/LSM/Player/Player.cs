using System.Collections;
using UnityEngine;

public class Player : Character
{

    //HP ����
    //�ִ� ü���� �����ϰ� ���� ü���� �׿� �°� ����
    public override void SetHp(float hp)
    {
        this.maxHp = hp;
        this.currentHp = hp;
    }
    public override float GetHp()
    {
        return currentHp;
    }

    //���¸�ŭ ���ظ� �氨�Ͽ� ������ ����
    //�����÷ο� ������ ���� double ����� ��ȯ �� ���
    public override void TakeDamage(float damage)
    {
        currentHp -= (float)((double)damage * (double)damage / ((double)armor + (double)damage));
    }

    //ü���� ȸ����
    public override void HealHp(float Heal)
    {
        if (maxHp < currentHp + Heal)
        {
            currentHp = maxHp;
        }
        else
        {
            currentHp += Heal;
        }
    }
}

