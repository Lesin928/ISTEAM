using System.Collections;
using UnityEngine;

public class FutureLazer : Weapon
{
    protected static float LazerAttackSpeed = 4f;
    protected static float LazerSpeed = 0.4f;
    private bool isOnCooldown = false; // 공격 쿨다운 상태 
    public GameObject effect1Prefab; // 선딜 이펙트1
    public GameObject effect2Prefab; // 선딜 이펙트2
    public float effect1Duration = 0.67f; // 선딜 이펙트1 지속시간  


    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // 무기 공격 속도 -> 플레이어 공격속도 비례
        attackSpeed = player.attackSpeed * LazerAttackSpeed;
        // 무기 이동 속도 -> 플레이어 이동속도 비례
        bulletSpeed = player.moveSpeed * LazerSpeed;
    }

    public override void CreatWeapon() //무기 풀 생성
    {
        LazerFlying LazerFlyingPrefab = weapon.GetComponent<LazerFlying>();
        ObjectPoolManager.Instance.CreatePool<LazerFlying>("FutureLazer", weapon.GetComponent<LazerFlying>(), 3);
        // 풀에서 가져온 모든 객체를 현재 객체의 자식으로 설정
        for (int i = 0; i < 3; i++) // 생성된 개수만큼 반복
        {
            LazerFlying newWeapon = ObjectPoolManager.Instance.GetFromPool<LazerFlying>("FutureLazer");
            if (newWeapon != null)
            {
                newWeapon.transform.SetParent(transform); // 현재 객체의 자식으로 설정 
                ObjectPoolManager.Instance.ReturnToPool("FutureLazer", newWeapon.GetComponent<LazerFlying>());// 총알 풀로 반환 
            }
        }
    }
    public override void WeaponAttack() //무기별 발사방법 구현
    {
        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // 무기 위치 (FutureLazer에 설정된 위치) 
        // 총알을 풀에서 가져옴
        LazerFlying newBullet = ObjectPoolManager.Instance.GetFromPool<LazerFlying>("FutureLazer");
        newBullet.gameObject.SetActive(false);  // 총알 잠시 비활성화 

        if (newBullet != null) //총알이 null이 아닐 때 실행
        {
            newBullet.transform.position = attackPosition.position;  // 발사 위치 설정
             
            // 총알 세팅 (이펙트 출력 후 발사)
            StartCoroutine(SetupBulletSequence(newBullet, attackPosition, bulletSpeed));

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

    private IEnumerator SetupBulletSequence(LazerFlying newBullet, Transform attackPosition, float bulletSpeed)
    {  
        // 선딜 이펙트1 출력
        GameObject effect1 = Instantiate(effect1Prefab, attackPosition.position, Quaternion.identity);
        effect1.transform.SetParent(transform);
        yield return new WaitForSeconds(effect1Duration); // 이펙트1 지속시간 대기 

        // 이펙트2 출력
        GameObject effect2 = Instantiate(effect2Prefab, attackPosition.position, Quaternion.identity);
        effect2.transform.SetParent(transform);
        Destroy(effect2, 2); // 필요하면 이펙트 제거
        newBullet.gameObject.SetActive(true);  // 총알 활성화 

        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // 2D 환경에서 Z 좌표 고정
            Vector2 direction = (mousePosition - attackPosition.position).normalized; // 방향 벡터로 정규화  
            rb.linearVelocity = direction * bulletSpeed;
            newBullet.LookAtMouse(rb);
            rb.linearVelocity = direction * 0; 
        }
        else
        {
            Debug.Log("rb = null");
        }

    }
}