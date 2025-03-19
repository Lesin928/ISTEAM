using UnityEngine;

//무기 추상 클래스
//모든 무기는 해당 클래스를 상속함
public abstract class Weapon : MonoBehaviour
{
    public int Level = 1;  // 무기의 데미지 배율
    public float attackCooldown = 1f;  // 공격 주기 (닿아있을 경우 데미지가 들어가는 주기)
    protected float attackTimer = 0f;

    protected virtual void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            WeaponAttack();
            attackTimer = 0f;
        }
    }

    public abstract void WeaponAttack(); // 각 무기별 공격 방식 정의
}