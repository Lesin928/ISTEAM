using UnityEngine;

public class LightSaberFlying : MonoBehaviour
{
    public GameObject effect;
    private float force = 8f; //�Ѿ��� �̴� �� + ������ ��� 
    private Rigidbody2D rb;
    public float scaleSpeed; // ������ ���� �ӵ�
    private Vector3 originalScale; // �ʱ� ũ�� ����

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
        originalScale = transform.localScale;
        OnEnable();
    }
    private void OnEnable()
    {
        scaleSpeed=10;
        // ���� �ʱ� ũ�Ⱑ (0,0,0)�̶�� (1,1,1)�� ����
        originalScale = transform.localScale;
        if (originalScale == Vector3.zero)
        {
            originalScale = Vector3.one;
        }
    }
    private void OnDisable()
    {
        transform.localScale = originalScale; // ũ�� �ʱ�ȭ
    }

    private void Update()
    {
        transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime; // ũ�� ����
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
                // �ӵ��� 0�� ��� �⺻ ���� ���� (��: ������)
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<Monster>().TakeDamage(force);
            IPushable pushable = collision.gameObject.GetComponent<IPushable>(); // ���� IPushable�� �����ߴ��� Ȯ��
            if (pushable != null) // IPushable�� ������ ���� ���
            {
                Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // �浹 ���⺤��
                                                                                                             // �и��� ���� �ݴ� �������� ����
                collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * force);
            }
            GameObject go = Instantiate(effect, collision.gameObject.transform.position, gameObject.transform.rotation);
        }
    } 

    private void OnBecameInvisible()
    {
        ObjectPoolManager.Instance.ReturnToPool("FutureLightSaber", gameObject.GetComponent<LightSaberFlying>());// �Ѿ� Ǯ�� ��ȯ 
    }
}

