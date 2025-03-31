using UnityEngine;
using UnityEngine.Pool;

//�� ����Ʈ�� �浹 ó��
public class StoneEffect : MonoBehaviour
{
    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>(); 
    }

    private void EnableCollider()
    {
        if (col != null)
        { 
            col.enabled = true; 
        }
    }
    private void DisableCollider()
    {
        if (col != null)
        {
            col.enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Debug.Log("���� ������");
            collision.gameObject.GetComponent<Monster>().TakeDamage(0.4f); 
        }
    }
}
