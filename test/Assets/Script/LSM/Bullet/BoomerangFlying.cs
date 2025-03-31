using System.Collections;
using UnityEngine;

public class BoomerangFlying : MonoBehaviour
{
    private float force = 2f; //�Ѿ��� �̴� �� + ������ ���
    public GameObject effect;
    private Vector3 rotationSpeed = new Vector3(0, 0, -180); // (X, Y, Z) �� ȸ�� �ӵ�  
    public int maxHitCount; // 3�� �浹�ϸ� �ݴ�� �̵�
    public float returnAngleOffset; // �ǵ��ư� ���� ���� ����
    public float decelerationFactor; // �浹 �� �ӵ� ���� ����

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
                // ���� �浹 �� �ӵ��� ���� ����
                isFirstHit = true;
                originalDirection = rb.linearVelocity.normalized;
                initialSpeed = rb.linearVelocity.magnitude;
            }

            hitCount++;
            Debug.Log(hitCount);
            
            if (hitCount < maxHitCount)
            {
                // �ӵ� ���������� ����
                rb.linearVelocity *= decelerationFactor;
            }
            else if (!isReturned)
            {
                // 5����° �浹 �� ���� ��ȯ
                isReturned = true;
                Vector2 returnDirection = Quaternion.Euler(0, 0, returnAngleOffset) * -originalDirection;
                StartCoroutine(RestoreSpeed(returnDirection));
            }

            collision.gameObject.GetComponent<Monster>().TakeDamage(force);
            IPushable pushable = collision.gameObject.GetComponent<IPushable>(); // ���� IPushable�� �����ߴ��� Ȯ��
            if (pushable != null) // IPushable�� ������ ���� ���
            {
                Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // �浹 ���⺤��
                                                                                                             // �и��� ���� �ݴ� �������� ����
                collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * force);
            }

            GameObject go = Instantiate(effect, gameObject.transform.position, gameObject.transform.rotation); 
        }

    }

    IEnumerator RestoreSpeed(Vector2 targetDirection)
    {
        float duration = 0.5f; // ���������� �����ϴ� �ð�
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
        ObjectPoolManager.Instance.ReturnToPool("AncientStone", gameObject.GetComponent<StoneFlying>());// �Ѿ� Ǯ�� ��ȯ
        
    }
}
