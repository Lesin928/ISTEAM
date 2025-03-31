using UnityEngine;

public class BazookaEffect : MonoBehaviour
{
    private float force = 5f; //총알의 미는 힘 + 데미지 계수  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<Monster>().TakeDamage(force); // 몬스터에게 데미지를 줌             
        }
    }
}
