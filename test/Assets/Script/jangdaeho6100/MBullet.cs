
using UnityEngine;

public class MBullet : MonoBehaviour
{
    public float speed = 3f;               // 총알 속도
    public float damage = 10f;             // 플레이어에게 줄 데미지
    public float maxDistance = 10f;        // 최대 사거리
    public float rotationOffset = 0f;      // 회전 보정 값

    private Vector2 direction;             // 총알 방향
    private Vector2 startPosition;         // 시작 위치 (사거리 측정용)

    // 총알 활성화 시 초기화
    void OnEnable()
    {
        startPosition = transform.position; // 현재 위치 저장
    }

    // 방향 설정 함수 (외부에서 호출)
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; // 방향 정규화

        // 총알 회전 방향도 맞춰줌
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }

    // 매 프레임마다 호출됨
    void Update()
    {
        // 지정된 방향으로 이동
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // 사거리 초과 시 → 풀에 반환
        if (Vector2.Distance(startPosition, transform.position) >= maxDistance)
        {
            MBulletPool.Instance.ReturnToPool(gameObject);
            return;
        }

        // 화면 밖으로 나가면 → 풀에 반환
        if (!IsVisibleOnScreen())
        {
            MBulletPool.Instance.ReturnToPool(gameObject);
        }
    }

    // 화면 안에 있는지 확인 (Viewport 기준)
    bool IsVisibleOnScreen()
    {
        Vector3 view = Camera.main.WorldToViewportPoint(transform.position);
        return view.x >= -0.1f && view.x <= 1.1f &&
               view.y >= -0.1f && view.y <= 1.1f;
    }

    // 충돌 처리
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.TakeDamage(damage);          // 플레이어에게 데미지 전달
            MBulletPool.Instance.ReturnToPool(gameObject);           // 충돌 시 반환
        }
    }

    void OnDisable()
    {
        StopAllCoroutines(); // 혹시 남아있는 코루틴 정리
    }
}