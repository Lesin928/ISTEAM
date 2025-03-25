using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

//돌 이펙트의 충돌 처리
public class StoneFying : MonoBehaviour
{
   // public WeaponObjectPool bulletPool;  // 총알 풀링 시스템 
    public GameObject effect;
    public float Force = 1f; 
    private Vector3 rotationSpeed = new Vector3(0, 0, -180); // (X, Y, Z) 축 회전 속도 
     

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerStay2D(Collider2D collision)    
    {
        if (collision.CompareTag("Monster"))
        {
            //collision.gameObject.GetComponent<Monster>().TakeDamage(power);            
            IPushable pushable = collision.gameObject.GetComponent<IPushable>(); // 적이 IPushable을 구현했는지 확인
            if (pushable != null) // IPushable을 구현한 적일 경우
            {
                Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // 충돌 방향벡터
               // 밀리는 힘을 반대 방향으로 적용
                collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * Force); 
            }

            GameObject go = Instantiate(effect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(go, 1);
            Debug.Log("총알 반환");
            ObjectPoolManager.Instance.ReturnToPool("AncientStone", gameObject);// 총알 풀로 반환
        }  

    }
    private void OnBecameInvisible()
    {
        ObjectPoolManager.Instance.ReturnToPool("AncientStone", gameObject);// 총알 풀로 반환
    }
}
