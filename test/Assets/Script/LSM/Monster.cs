using Unity.Mathematics.Geometry;
using UnityEngine;

//캐릭터를 상속받는 몬스터 클래스 
//모든 몬스터는 해당 스크립트를 상속받고 상호작용함
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
