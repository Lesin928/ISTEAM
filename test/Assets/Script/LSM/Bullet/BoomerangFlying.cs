using System.Collections;
using UnityEngine;

public class BoomerangFlying : MonoBehaviour
{
    private float force = 2f; //총알의 미는 힘 + 데미지 계수
    public GameObject effect;
    private Vector3 rotationSpeed = new Vector3(0, 0, -180); // (X, Y, Z) 축 회전 속도  
    public int maxHitCount; // 3번 충돌하면 반대로 이동
    public float returnAngleOffset; // 되돌아갈 때의 각도 변경
    public float decelerationFactor; // 충돌 시 속도 감소 비율

    private Rigidbody2D rb;
    private int hitCount = 0;
    private bool isReturned = false;
    private bool isFirstHit = false;
    private Vector2 originalDirection;
    private float initialSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        decelerationFactor = 0.8f;
        returnAngleOffset = 15f;
        maxHitCount = 3;
        hitCount = 0;
        isReturned = false;
        isFirstHit = false;
    }

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * 5);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (!isFirstHit)
            {
                // 최초 충돌 시 속도와 방향 저장
                isFirstHit = true;
                originalDirection = rb.linearVelocity.normalized;
                initialSpeed = rb.linearVelocity.magnitude;
            }

            hitCount++;
            Debug.Log(hitCount);
            
            if (hitCount < maxHitCount)
            {
                // 속도 점진적으로 감소
                rb.linearVelocity *= decelerationFactor;
            }
            else if (!isReturned)
            {
                // 5마리째 충돌 후 방향 전환
                isReturned = true;
                Vector2 returnDirection = Quaternion.Euler(0, 0, returnAngleOffset) * -originalDirection;
                StartCoroutine(RestoreSpeed(returnDirection));
            }

            collision.gameObject.GetComponent<Monster>().TakeDamage(force);
            IPushable pushable = collision.gameObject.GetComponent<IPushable>(); // 적이 IPushable을 구현했는지 확인
            if (pushable != null) // IPushable을 구현한 적일 경우
            {
                Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // 충돌 방향벡터
                                                                                                             // 밀리는 힘을 반대 방향으로 적용
                collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * force);
            }

            GameObject go = Instantiate(effect, gameObject.transform.position, gameObject.transform.rotation); 
        }

    }

    IEnumerator RestoreSpeed(Vector2 targetDirection)
    {
        float duration = 0.5f; // 점진적으로 복구하는 시간
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetDirection * initialSpeed, t);
            yield return null;
        }

        rb.linearVelocity = targetDirection * initialSpeed;
    }


    private void OnBecameInvisible()
    {  
        ObjectPoolManager.Instance.ReturnToPool("AncientStone", gameObject.GetComponent<StoneFlying>());// 총알 풀로 반환
        
    }
}
