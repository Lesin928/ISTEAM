using UnityEngine;

public class LightSaberFlying : MonoBehaviour
{
    public GameObject effect;
    private float force = 8f; //총알의 미는 힘 + 데미지 계수 
    private Rigidbody2D rb;
    public float scaleSpeed; // 스케일 증가 속도
    private Vector3 originalScale; // 초기 크기 저장

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        originalScale = transform.localScale;
        OnEnable();
    }
    private void OnEnable()
    {
        scaleSpeed=10;
        // 만약 초기 크기가 (0,0,0)이라면 (1,1,1)로 설정
        originalScale = transform.localScale;
        if (originalScale == Vector3.zero)
        {
            originalScale = Vector3.one;
        }
    }
    private void OnDisable()
    {
        transform.localScale = originalScale; // 크기 초기화
    }

    private void Update()
    {
        transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime; // 크기 증가
    }

    public void LookAtMouse(Rigidbody2D rb)
    {
        if (rb != null)
        {
            Vector2 velocity = rb.linearVelocity;
            if (velocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
            else
            {
                // 속도가 0일 경우 기본 방향 설정 (예: 오른쪽)
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<Monster>().TakeDamage(force);
            IPushable pushable = collision.gameObject.GetComponent<IPushable>(); // 적이 IPushable을 구현했는지 확인
            if (pushable != null) // IPushable을 구현한 적일 경우
            {
                Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // 충돌 방향벡터
                                                                                                             // 밀리는 힘을 반대 방향으로 적용
                collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * force);
            }
            GameObject go = Instantiate(effect, collision.gameObject.transform.position, gameObject.transform.rotation);
        }
    } 

    private void OnBecameInvisible()
    {
        ObjectPoolManager.Instance.ReturnToPool("FutureLightSaber", gameObject.GetComponent<LightSaberFlying>());// 총알 풀로 반환 
    }
}

