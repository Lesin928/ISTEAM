using UnityEngine;

// Monster.cs�� ���� �����տ� ���̰�, BaseMonster�� �̵�/���� ����
public class BaseMonster : MonoBehaviour
{
    protected Transform solar; // �¾� ��ġ (���� ���Ͱ� �̰� ����)
    protected Transform player; // �÷��̾� ��ġ (���Ÿ� ���Ͱ� �̰� ����)

    public int poolIndex; // ������Ʈ Ǯ���� �ǵ��� �� �ĺ���

    protected virtual void Awake()
    {
        // �¾�� �÷��̾� ������Ʈ�� �±׷� ã��
        solar = GameObject.FindWithTag("Solar")?.transform;
        player = GameObject.FindWithTag("Player")?.transform;
    }

    // �ڽ� Ŭ�������� Start �ʿ� �� �������̵�
    protected virtual void Start() { }
     
    // ���� ó�� Monster ������Ʈ ȣ��
    public virtual void TakeDamage(float damage)
    {
        // Monster ��ũ��Ʈ ���� �پ� �����Ƿ� GetComponent�� ȣ��
        Monster stats = GetComponent<Monster>();
        if (stats != null)
        {
            stats.currentHp -= damage;
            if (stats.currentHp <= 0)
            {
                Die(); // ������ Ǯ�� ��ȯ
            }
        }
    }

    // ��� ó�� (Ǯ�� ��ȯ)
    protected virtual void Die()
    {
        gameObject.SetActive(false); // ��Ȱ��ȭ
        TimerManager.Instance.killCount++; // ų ī��Ʈ ����
        MobObjectPool pool = FindAnyObjectByType<MobObjectPool>();
        if (pool != null)
        {
            pool.ReturnToPool(gameObject);
        }
    }
}
