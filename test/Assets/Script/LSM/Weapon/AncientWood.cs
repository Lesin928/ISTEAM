using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//고대무기 나무 공격방식 구현한 코드
//오브젝트풀링을 통해 나무 이펙트 발사
//레벨 기억, 스탯 기억
//레벨에 비례한 공격

public class AncientWood : Weapon
{
    protected static float WoodAttackSpeed = 1.5f; 
    protected static float WoodSpeed = 1.6f;
    private bool isOnCooldown = false; // 공격 쿨다운 상태

    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // 무기 공격 속도 -> 플레이어 공격속도 비례
        attackSpeed = player.attackSpeed * WoodAttackSpeed;
        // 무기 이동 속도 -> 플레이어 이동속도 비례
        bulletSpeed = player.moveSpeed * WoodSpeed; 
    }

    public override void CreatWeapon() //무기 풀 생성
    {
        ObjectPoolManager.Instance.CreatePool<WoodFlying>("AncientWood", weapon.GetComponent<WoodFlying>(), 10);
    }

    public override void WeaponAttack() //무기별 발사방법 구현
    {
        AudioManager.Instance.PlaySFX("Player", "Player_Attack_Ancient");

        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // 무기 위치 (AncientStone에 설정된 위치)
        // 총알을 풀에서 가져옴
        WoodFlying newBullet = ObjectPoolManager.Instance.GetFromPool<WoodFlying>("AncientWood");  
        if (newBullet != null) //총알이 null이 아닐 때 실행
        {
            newBullet.transform.position = attackPosition.position;  // 발사 위치 설정
            newBullet.gameObject.SetActive(true);  // 총알 활성화 

            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f; // 2D 환경에서 Z 좌표 고정
                Vector2 direction = (mousePosition - attackPosition.position).normalized; // 방향 벡터로 정규화 
                rb.linearVelocity = direction * bulletSpeed; // 일정 속도로 발사
                newBullet.LookAtMouse(rb);
            }
            else
            {
                Debug.Log("rb = null");
            }
            // 공격 속도에 따라 발사 간격 조정             
            StartCoroutine(FireCooldown(attackSpeed)); // 공격 간격 유지
        }
        else
        {
            Debug.Log("newBullet = null");
        }
    }

    private IEnumerator FireCooldown(float delay)
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(delay);
        isOnCooldown = false;
    }

}