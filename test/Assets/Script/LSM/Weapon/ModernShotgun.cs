using System.Collections;
using UnityEngine;

public class ModernShotgun : Weapon
{

    protected static float ShotgunAttackSpeed = 1.0f;
    protected static float ShotgunSpeed = 3f;
    private bool isOnCooldown = false; // 공격 쿨다운 상태
    System.Random random = new System.Random();
    float[] angles = new float[7];

    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // 무기 공격 속도 -> 플레이어 공격속도 비례
        attackSpeed = player.attackSpeed * ShotgunAttackSpeed;
        // 무기 이동 속도 -> 플레이어 이동속도 비례
        bulletSpeed = player.moveSpeed * ShotgunSpeed;
    }

    public override void CreatWeapon() //무기 풀 생성
    {
        ObjectPoolManager.Instance.CreatePool<ShotgunFlying>("ModernShotgun", weapon.GetComponent<ShotgunFlying>(), 10);

    }
    public override void WeaponAttack() //무기별 발사방법 구현
    {
        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // 무기 위치 (ModernShotgun에 설정된 위치) 
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2D 환경에서 Z 좌표 고정
        Vector2 baseDirection = (mousePosition - attackPosition.position).normalized; // 기본 방향 벡터
        for (int i = 0; i < angles.Length; i++)
        {
            angles[i] = random.Next(-15, 16); // -15부터 15 사이의 정수값 생성
        }

        foreach (float angle in angles)
        {
            ShotgunFlying newBullet = ObjectPoolManager.Instance.GetFromPool<ShotgunFlying>("ModernShotgun");
            if (newBullet != null) // 총알이 null이 아닐 때 실행
            {
                newBullet.transform.position = attackPosition.position; // 발사 위치 설정
                newBullet.gameObject.SetActive(true); // 총알 활성화

                Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // 1. 기본 방향으로 발사
                    rb.linearVelocity = baseDirection * bulletSpeed;

                    // 2. LookAtMouse를 호출해서 기본 시각적 회전 설정 (원래의 45도 오프셋은 이미지에 포함되어 있음)
                    newBullet.LookAtMouse(rb);

                    // 3. 추가적으로 angle만큼 회전한 벡터로 velocity를 재설정하여 이동 방향도 변경
                    Vector2 adjustedVelocity = Quaternion.Euler(0, 0, angle) * rb.linearVelocity;
                    rb.linearVelocity = adjustedVelocity;

                    // 4. 오브젝트 이미지(회전)는 angle만큼 추가 회전 적용 (기존 이미지 45도 회전은 유지)
                    newBullet.transform.Rotate(0, 0, angle);
                }
                else
                {
                    Debug.Log("rb = null");
                }
            }
        }


        // 공격 속도에 따라 발사 간격 조정             
        StartCoroutine(FireCooldown(attackSpeed)); // 공격 간격 유지 
    }
    private IEnumerator FireCooldown(float delay)
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(delay);
        isOnCooldown = false;
    }

}