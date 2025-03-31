using UnityEngine;

public class DroneFlying : MonoBehaviour
{
    private float force = 2f; //총알의 미는 힘 + 데미지 계수   
    public GameObject effect;
    private Vector3 rotationSpeed = new Vector3(0, 0, -180); // (X, Y, Z) 축 회전 속도  
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
            IPushable pushable = collision.gameObject.GetComponent<IPushable>(); // 적이 IPushable을 구현했는지 확인
            if (pushable != null) // IPushable을 구현한 적일 경우
            {
                Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // 충돌 방향벡터
                                                                                                             // 밀리는 힘을 반대 방향으로 적용
                collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * force);
            }

            GameObject go = Instantiate(effect, collision.gameObject.transform.position, gameObject.transform.rotation);
            ObjectPoolManager.Instance.ReturnToPool("FutureDrone", gameObject.GetComponent<DroneFlying>());// 총알 풀로 반환 
        }

    }
    private void OnBecameInvisible()
    {
        if (!isReturned)
        {
            ObjectPoolManager.Instance.ReturnToPool("FutureDrone", gameObject.GetComponent<DroneFlying>());// 총알 풀로 반환 
        }
    }
}
