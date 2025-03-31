using UnityEngine;

public class FireFlying : MonoBehaviour
{
    private float force = 1f; //총알의 미는 힘 + 데미지 계수
    public GameObject effect;   
     
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("Monster"))
        { 
            collision.gameObject.GetComponent<Monster>().TakeDamage(force);  
            GameObject go = Instantiate(effect, gameObject.transform.position, gameObject.transform.rotation);          
        }

    }
    private void OnBecameInvisible()
    { 
        ObjectPoolManager.Instance.ReturnToPool("AncientFire", gameObject.GetComponent<FireFlying>());// 총알 풀로 반환

    }
}
