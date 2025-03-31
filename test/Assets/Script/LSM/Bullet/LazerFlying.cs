using UnityEngine;

public class LazerFlying : MonoBehaviour
{
    public GameObject effect;
    private float force = 3f; //�Ѿ��� �̴� �� + ������ ��� 
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    } 

    private void OnEnable()
    { 
        Invoke("OnReturn", 2f); //2�� �Ŀ� ��ȯ;
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
            Destroy(go, 1);
        }
    }
    public void OnReturn()
    { 
        ObjectPoolManager.Instance.ReturnToPool("FutureLazer", gameObject.GetComponent<LazerFlying>());// �Ѿ� Ǯ�� ��ȯ 
    }

}

