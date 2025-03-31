using System.IO.Pipes;
using System.Threading;
using UnityEngine;

public class ShotgunFlying : MonoBehaviour
{
    private float force = 2f; //총알의 미는 힘 + 데미지 계수  
    private Rigidbody2D rb;
    public GameObject effect;

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
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
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
            ObjectPoolManager.Instance.ReturnToPool("ModernShotgun", gameObject.GetComponent<ShotgunFlying>());// 총알 풀로 반환 
        }

    }
    private void OnBecameInvisible()
    {
        ObjectPoolManager.Instance.ReturnToPool("ModernShotgun", gameObject.GetComponent<ShotgunFlying>());// 총알 풀로 반환 
    }
}
