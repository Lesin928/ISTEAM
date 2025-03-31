using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

//고대무기 돌 공격방식 구현한 코드
//오브젝트풀링을 통해 돌 이펙트 발사
//몬스토와 충돌시 단일 높은 데미지
//몬스터와 충돌시 파편으로 부서짐 

public class AncientStone : Weapon
{

    protected static float StoneAttackSpeed = 2.0f;
    protected static float StoneSpeed = 1.2f; 
    private bool isOnCooldown = false; // 공격 쿨다운 상태

    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // 무기 공격 속도 -> 플레이어 공격속도 비례
        attackSpeed = player.attackSpeed * StoneAttackSpeed;        
        // 무기 이동 속도 -> 플레이어 이동속도 비례
        bulletSpeed = player.moveSpeed * StoneSpeed; 
    }

    public override void CreatWeapon() //무기 풀 생성
    {
        ObjectPoolManager.Instance.CreatePool<StoneFlying>("AncientStone", weapon.GetComponent<StoneFlying>(), 10); 

    }
    public override void WeaponAttack() //무기별 발사방법 구현
    {
        AudioManager.Instance.PlaySFX("Player", "Player_Attack_Ancient");

        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // 무기 위치 (AncientStone에 설정된 위치) 
        // 총알을 풀에서 가져옴
        StoneFlying newBullet = ObjectPoolManager.Instance.GetFromPool<StoneFlying>("AncientStone");  
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

