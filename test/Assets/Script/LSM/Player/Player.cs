using System.Collections;
using UnityEngine;

public class Player : Character
{

    //HP 관련
    //최대 체력을 설정하고 현재 체력을 그와 맞게 세팅
    public override void SetHp(float hp)
    {
        this.maxHp = hp;
        this.currentHp = hp;
    }
    public override float GetHp()
    {
        return currentHp;
    }

    //방어력만큼 피해를 경감하여 데미지 받음
    //오버플로우 방지를 위해 double 명시적 변환 후 계산
    public override void TakeDamage(float damage)
    {
        currentHp -= (float)((double)damage * (double)damage / ((double)armor + (double)damage));
    }

    //체력을 회복함
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

