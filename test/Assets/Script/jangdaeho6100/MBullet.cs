
using UnityEngine;

public class MBullet : MonoBehaviour
{
    public float speed = 3f;               // �Ѿ� �ӵ�
    public float damage = 10f;             // �÷��̾�� �� ������
    public float maxDistance = 10f;        // �ִ� ��Ÿ�
    public float rotationOffset = 0f;      // ȸ�� ���� ��

    private Vector2 direction;             // �Ѿ� ����
    private Vector2 startPosition;         // ���� ��ġ (��Ÿ� ������)

    // �Ѿ� Ȱ��ȭ �� �ʱ�ȭ
    void OnEnable()
    {
        startPosition = transform.position; // ���� ��ġ ����
    }

    // ���� ���� �Լ� (�ܺο��� ȣ��)
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; // ���� ����ȭ

        // �Ѿ� ȸ�� ���⵵ ������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }

    // �� �����Ӹ��� ȣ���
    void Update()
    {
        // ������ �������� �̵�
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // ��Ÿ� �ʰ� �� �� Ǯ�� ��ȯ
        if (Vector2.Distance(startPosition, transform.position) >= maxDistance)
        {
            MBulletPool.Instance.ReturnToPool(gameObject);
            return;
        }

        // ȭ�� ������ ������ �� Ǯ�� ��ȯ
        if (!IsVisibleOnScreen())
        {
            MBulletPool.Instance.ReturnToPool(gameObject);
        }
    }

    // ȭ�� �ȿ� �ִ��� Ȯ�� (Viewport ����)
    bool IsVisibleOnScreen()
    {
        Vector3 view = Camera.main.WorldToViewportPoint(transform.position);
        return view.x >= -0.1f && view.x <= 1.1f &&
               view.y >= -0.1f && view.y <= 1.1f;
    }

    // �浹 ó��
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.TakeDamage(damage);          // �÷��̾�� ������ ����
            MBulletPool.Instance.ReturnToPool(gameObject);           // �浹 �� ��ȯ
        }
    }

    void OnDisable()
    {
        StopAllCoroutines(); // Ȥ�� �����ִ� �ڷ�ƾ ����
    }
}