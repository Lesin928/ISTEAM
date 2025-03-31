using UnityEngine;
using UnityEngine.Pool;

//돌 이펙트의 충돌 처리
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
            Debug.Log("파편 데미지");
            collision.gameObject.GetComponent<Monster>().TakeDamage(0.4f); 
        }
    }
}
