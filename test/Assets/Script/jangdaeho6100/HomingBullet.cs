using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 3f;               // 이동 속도
    public float maxLifetime = 4f;         // 최대 생존 시간
    public float maxChaseDistance = 8f;    // 추적 거리 제한
    public float damage = 15f;             // 플레이어에게 입힐 데미지

    private Transform target;              // 추적 대상 (플레이어)
    private Vector2 spawnPosition;         // 생성 시 위치 저장
    private float lifeTimer = 0f;          // 경과 시간 누적

    // 총알이 활성화될 때 초기화
    void OnEnable()
    {
        spawnPosition = transform.position;  // 시작 위치 저장
        lifeTimer = 0f;                      // 생존 시간 초기화
    }

    // 외부에서 타겟을 설정하는 함수
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;                 // 타겟 지정
    }

    void Update()
    {
        // 생존 시간 증가
        lifeTimer += Time.deltaTime;

        // 수명 초과 시 자동 반환
        if (lifeTimer >= maxLifetime)
        {
            MBulletPool.Instance.ReturnToPool(gameObject);
            return;
        }

        // 타겟이 없거나 거리 초과 시 반환
        if (target == null || Vector2.Distance(spawnPosition, transform.position) > maxChaseDistance)
        {
            MBulletPool.Instance.ReturnToPool(gameObject);
            return;
        }

        // 타겟 방향 계산 후 이동
        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // 회전 보정 (앞 방향이 위쪽일 경우 -90도)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어에 부딪히면 데미지 주고 풀에 반환
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.TakeDamage(damage);
            MBulletPool.Instance.ReturnToPool(gameObject);
        }
    }

    void OnDisable()
    {
        StopAllCoroutines(); // 혹시 모를 코루틴 정리
    }
}
