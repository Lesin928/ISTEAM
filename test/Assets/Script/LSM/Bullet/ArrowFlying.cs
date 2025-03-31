using UnityEngine;

public class ArrowFlying : MonoBehaviour
{
    public GameObject effect;
    private float force = 2f; //총알의 미는 힘 + 데미지 계수
    private int mobcount;
    private bool isTrigger = false;
    private bool isReturned = false;
    private Rigidbody2D rb;

    private void Start()
    {
        OnEnable();
    }

    public void LookAtMouse(Rigidbody2D rb)
    {

        if (rb != null)
        {
            Vector2 velocity = rb.linearVelocity;
            if (velocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 45f);
            }
            else
            {
                // 속도가 0일 경우 기본 방향 설정 (예: 오른쪽)
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        mobcount = 5;
        isTrigger = false;
        isReturned = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") && isTrigger == false)
        {
            collision.gameObject.GetComponent<Monster>().TakeDamage(force);
            IPushable pushable = collision.gameObject.GetComponent<IPushable>(); // 적이 IPushable을 구현했는지 확인
            if (pushable != null) // IPushable을 구현한 적일 경우
            {
                Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // 충돌 방향벡터
                                                                                                             // 밀리는 힘을 반대 방향으로 적용
                collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * force);
            }

            GameObject go = Instantiate(effect, gameObject.transform.position, gameObject.transform.rotation);

            if (mobcount > 1)
                --mobcount;
            else
            {
                isTrigger = true;
                isReturned = true;
                ObjectPoolManager.Instance.ReturnToPool("MedievalArrow", gameObject.GetComponent<WoodFlying>());// 총알 풀로 반환
            }
        }

    }
    private void OnBecameInvisible()
    {
        if (!isReturned)
        {
            ObjectPoolManager.Instance.ReturnToPool("MedievalArrow", gameObject.GetComponent<WoodFlying>());// 총알 풀로 반환
        }
    }
}
