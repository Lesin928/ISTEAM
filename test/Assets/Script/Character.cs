using UnityEngine;

//추상화 클래스 Character
//NPC와 PC 모두 해당 클래스를 상속함
public abstract class Character : MonoBehaviour
{
    public float maxHp; //최대체력
    public float currentHp; //현재 체력
    public float armor; //방어력
    public float attack; //공격력
    public float attackSpeed; //공격속도
    public float moveSpeed; //이동속도  

    //HP 관련
    //최대 체력을 설정하고 현재 체력을 그와 맞게 세팅
    public virtual void SetHp(float hp)
    {
        this.maxHp = hp;
        this.currentHp = hp;
    }
    public virtual float GetHp()
    {
        return currentHp;
    }
    
    //방어력만큼 피해를 경감하여 데미지 받음
    //오버플로우 방지를 위해 double 명시적 변환 후 계산
    public virtual void TakeDamage(float damage)
    {
        currentHp -= (float)((double)damage * (double)damage / ((double)armor + (double)damage));
    }

    //체력을 회복함
    public virtual void HealHp(float Heal)
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
