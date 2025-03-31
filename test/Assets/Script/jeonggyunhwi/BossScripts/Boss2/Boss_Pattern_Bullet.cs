using UnityEngine;

public class Boss_Pattern_Bullet : MonoBehaviour
{
    private float attack = 4.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //플레이어에게 데미지 입히기
            //추가해야 함
            collision.GetComponent<Player>().TakeDamage(attack);
            //삭제
            Destroy(gameObject);
        }
    }
}
