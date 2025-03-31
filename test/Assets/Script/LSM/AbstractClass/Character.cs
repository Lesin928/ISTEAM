using UnityEngine;

//추상화 클래스 Character
//NPC와 PC 모두 해당 클래스를 상속
public abstract class Character : MonoBehaviour
{ 
    public float maxHp; //최대체력
    public float currentHp; //현재 체력
    public float armor; //방어력
    public float attack; //공격력
    public float attackSpeed; //공격속도 (Weapon에서 활용)
    public float moveSpeed; //이동속도 (Weapon에서 활용)
    public float critical; //치명타 확률 
    public float criticalDamage; //치명타 피해

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

    //치명타 판정 이후 방어력만큼 피해를 경감하여 데미지 받음 
    //몬스터는 치명타 발생못시킴
    public abstract void TakeDamage(float damage); 

}
