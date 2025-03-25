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
    private void Awake()
    {
        //무기 활성화시 무기 풀 생성
        ObjectPoolManager.Instance.CreatePool<AncientStone>("AncientStone", weapon, 10);  
    }

    public override void WeaponAttack() //무기별 발사방법 구현
    {
        // 현재 AncientStone 스크립트가 붙어있는 오브젝트에서 위치와 공격 속도 값 받기
        Transform attackPosition = weaponPos; // 무기 위치 (AncientStone에 설정된 위치)
        float attackSpeed = this.attackSpeed; // 무기 속도 (각 무기마다 다를 수 있음)
        float bulletSpeed = this.bulletSpeed; // 총알 속도 (각 무기마다 다를 수 있음)

        // 총알을 풀에서 가져옴
        StoneFying newBullet = ObjectPoolManager.Instance.GetFromPool<StoneFying>("AncientStone");
        Debug.Log("총알 생성");
        //Debug.Log(newBullet.gameObject.activeInHierarchy);
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
    /*
    private IEnumerator ReturnBulletToPool(GameObject bullet, float delay)
    {
        // 일정 시간 후 총알을 풀로 반환
        yield return new WaitForSeconds(delay);
        bullet.SetActive(false); // 총알 비활성화
        bulletPool.ReturnToPool(bullet); // 총알 풀로 반환

        ObjectPoolManager.Instance.ReturnToPool("AncientStone", gameObject);
    }*/
  
    private IEnumerator FireCooldown(float delay)
    { 
        yield return new WaitForSeconds(delay);
    }

}

