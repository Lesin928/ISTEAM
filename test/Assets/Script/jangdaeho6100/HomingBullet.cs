using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 3f;               // �̵� �ӵ�
    public float maxLifetime = 4f;         // �ִ� ���� �ð�
    public float maxChaseDistance = 8f;    // ���� �Ÿ� ����
    public float damage = 15f;             // �÷��̾�� ���� ������

    private Transform target;              // ���� ��� (�÷��̾�)
    private Vector2 spawnPosition;         // ���� �� ��ġ ����
    private float lifeTimer = 0f;          // ��� �ð� ����

    // �Ѿ��� Ȱ��ȭ�� �� �ʱ�ȭ
    void OnEnable()
    {
        spawnPosition = transform.position;  // ���� ��ġ ����
        lifeTimer = 0f;                      // ���� �ð� �ʱ�ȭ
    }

    // �ܺο��� Ÿ���� �����ϴ� �Լ�
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;                 // Ÿ�� ����
    }

    void Update()
    {
        // ���� �ð� ����
        lifeTimer += Time.deltaTime;

        // ���� �ʰ� �� �ڵ� ��ȯ
        if (lifeTimer >= maxLifetime)
        {
            MBulletPool.Instance.ReturnToPool(gameObject);
            return;
        }

        // Ÿ���� ���ų� �Ÿ� �ʰ� �� ��ȯ
        if (target == null || Vector2.Distance(spawnPosition, transform.position) > maxChaseDistance)
        {
            MBulletPool.Instance.ReturnToPool(gameObject);
            return;
        }

        // Ÿ�� ���� ��� �� �̵�
        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // ȸ�� ���� (�� ������ ������ ��� -90��)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ �ε����� ������ �ְ� Ǯ�� ��ȯ
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.TakeDamage(damage);
            MBulletPool.Instance.ReturnToPool(gameObject);
        }
    }

    void OnDisable()
    {
        StopAllCoroutines(); // Ȥ�� �� �ڷ�ƾ ����
    }
}
