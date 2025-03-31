using System.IO.Pipes;
using System.Threading;
using UnityEngine;

public class ShotgunFlying : MonoBehaviour
{
    private float force = 2f; //�Ѿ��� �̴� �� + ������ ���  
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
                // �ӵ��� 0�� ��� �⺻ ���� ���� (��: ������)
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
            IPushable pushable = collision.gameObject.GetComponent<IPushable>(); // ���� IPushable�� �����ߴ��� Ȯ��
            if (pushable != null) // IPushable�� ������ ���� ���
            {
                Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // �浹 ���⺤��
                                                                                                             // �и��� ���� �ݴ� �������� ����
                collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * force);
            }

            GameObject go = Instantiate(effect, collision.gameObject.transform.position, gameObject.transform.rotation);
            ObjectPoolManager.Instance.ReturnToPool("ModernShotgun", gameObject.GetComponent<ShotgunFlying>());// �Ѿ� Ǯ�� ��ȯ 
        }

    }
    private void OnBecameInvisible()
    {
        ObjectPoolManager.Instance.ReturnToPool("ModernShotgun", gameObject.GetComponent<ShotgunFlying>());// �Ѿ� Ǯ�� ��ȯ 
    }
}
