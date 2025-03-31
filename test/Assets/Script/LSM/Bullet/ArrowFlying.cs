using UnityEngine;

public class ArrowFlying : MonoBehaviour
{
    public GameObject effect;
    private float force = 2f; //�Ѿ��� �̴� �� + ������ ���
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
                // �ӵ��� 0�� ��� �⺻ ���� ���� (��: ������)
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
            IPushable pushable = collision.gameObject.GetComponent<IPushable>(); // ���� IPushable�� �����ߴ��� Ȯ��
            if (pushable != null) // IPushable�� ������ ���� ���
            {
                Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // �浹 ���⺤��
                                                                                                             // �и��� ���� �ݴ� �������� ����
                collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * force);
            }

            GameObject go = Instantiate(effect, gameObject.transform.position, gameObject.transform.rotation);

            if (mobcount > 1)
                --mobcount;
            else
            {
                isTrigger = true;
                isReturned = true;
                ObjectPoolManager.Instance.ReturnToPool("MedievalArrow", gameObject.GetComponent<WoodFlying>());// �Ѿ� Ǯ�� ��ȯ
            }
        }

    }
    private void OnBecameInvisible()
    {
        if (!isReturned)
        {
            ObjectPoolManager.Instance.ReturnToPool("MedievalArrow", gameObject.GetComponent<WoodFlying>());// �Ѿ� Ǯ�� ��ȯ
        }
    }
}
