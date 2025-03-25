using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float Speed = 3f;
    Vector2 vec2 = Vector2.left;


    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(vec2 * Speed * Time.deltaTime);
    }
    public void Move(Vector2 vec)
    {
        vec2 = vec;
    }

    private void OnBecameInvisible()
    {
        BossPoolManager.Instance.Return(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //플레이어에게 데미지 입히기

            //삭제
            BossPoolManager.Instance.Return(gameObject);
        }
    }
}
