using UnityEngine;

public class BazookaEffect : MonoBehaviour
{
    private float force = 5f; //�Ѿ��� �̴� �� + ������ ���  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<Monster>().TakeDamage(force); // ���Ϳ��� �������� ��             
        }
    }
}
