using System.Threading;
using UnityEngine;

public class PBullet : MonoBehaviour
{   //�浹�� ���Ϳ��� �������� ���ϴ� ����
    //�浹�� ������� ����Ʈ�� �����ϴ� ����


    //�ش� ���� ���� ���ݷ�
    //�߻� ������ �÷��̾��� ���ݷ�
    private float Attack;
    [SerializeField]
    private GameObject effect;

    void Update()
    { 

    }

    //ȭ������� �������
    private void OnBecameInvisible()
    {
        //�ڱ� �ڽ� �����
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<Monster>().TakeDamage(Attack);
            //����Ʈ����
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //1�ʵڿ� �����
            Destroy(go, 1);
            //�̻��� ����
            Destroy(gameObject);
        }
    }
}