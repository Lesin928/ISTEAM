using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedievalAxe : Weapon
{
    protected static float AxeAttackSpeed = 1.5f;
    protected static float AxeSpeed = 0.4f;
    private bool isOnCooldown = false; // 공격 쿨다운 상태

    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // 무기 공격 속도 -> 플레이어 공격속도 비례
        attackSpeed = player.attackSpeed * AxeAttackSpeed;
        // 무기 이동 속도 -> 플레이어 이동속도 비례
        bulletSpeed = player.moveSpeed * AxeSpeed;
    }

    public override void CreatWeapon() //무기 풀 생성
    {
        AxeFlying axeFlyingPrefab = weapon.GetComponent<AxeFlying>();
        ObjectPoolManager.Instance.CreatePool<AxeFlying>("MedievalAxe", weapon.GetComponent<AxeFlying>(), 3);
        // 풀에서 가져온 모든 객체를 현재 객체의 자식으로 설정
        for (int i = 0; i < 3; i++) // 생성된 개수만큼 반복
        {
            AxeFlying newWeapon = ObjectPoolManager.Instance.GetFromPool<AxeFlying>("MedievalAxe");
            if (newWeapon != null)
            {
                newWeapon.transform.SetParent(transform); // 현재 객체의 자식으로 설정 
                ObjectPoolManager.Instance.ReturnToPool("MedievalAxe", newWeapon.GetComponent<AxeFlying>());// 총알 풀로 반환 
            }
        }
    }
    public override void WeaponAttack() //무기별 발사방법 구현
    {
        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // 무기 위치 (MedievalAxe에 설정된 위치) 
        // 총알을 풀에서 가져옴
        AxeFlying newBullet = ObjectPoolManager.Instance.GetFromPool<AxeFlying>("MedievalAxe");
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
                newBullet.transform.position += (Vector3)direction * bulletSpeed;
                rb.linearVelocity = direction * bulletSpeed;
                newBullet.LookAtMouse(rb);
                rb.linearVelocity = direction * 0;
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