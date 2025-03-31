using UnityEngine;

public class DroneFlying : MonoBehaviour
{
    private float force = 2f; //�Ѿ��� �̴� �� + ������ ���   
    public GameObject effect;
    private Vector3 rotationSpeed = new Vector3(0, 0, -180); // (X, Y, Z) �� ȸ�� �ӵ�  
    private bool isTrigger = false;
    private bool isReturned = false;

    private void OnEnable()
    {
        isTrigger = false;
        isReturned = false;
    }

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") && isTrigger == false)
        {
            isTrigger = true;
            isReturned = true;
            collision.gameObject.GetComponent<Monster>().TakeDamage(force);
            IPushable pushable = collision.gameObject.GetComponent<IPushable>(); // ���� IPushable�� �����ߴ��� Ȯ��
            if (pushable != null) // IPushable�� ������ ���� ���
            {
                Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // �浹 ���⺤��
                                                                                                             // �и��� ���� �ݴ� �������� ����
                collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * force);
            }

            GameObject go = Instantiate(effect, collision.gameObject.transform.position, gameObject.transform.rotation);
            ObjectPoolManager.Instance.ReturnToPool("FutureDrone", gameObject.GetComponent<DroneFlying>());// �Ѿ� Ǯ�� ��ȯ 
        }

    }
    private void OnBecameInvisible()
    {
        if (!isReturned)
        {
            ObjectPoolManager.Instance.ReturnToPool("FutureDrone", gameObject.GetComponent<DroneFlying>());// �Ѿ� Ǯ�� ��ȯ 
        }
    }
}
