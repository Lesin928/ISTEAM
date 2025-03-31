using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private float attack = 4.0f;

    public float Speed = 3f;
    Vector2 vec2 = Vector2.left;

    public bool isReturned = false;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
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

        if (isReturned)
        {
            return;
        }
        else
        {
            isReturned = true;
            BossPoolManager.Instance.Return(gameObject);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //플레이어에게 데미지 입히기
            collision.GetComponent<Player>().TakeDamage(attack);
            //추가해야함

            //삭제
            if (isReturned)
            {
                return;
            }
            else
            {
                isReturned = true;
                BossPoolManager.Instance.Return(gameObject);
                
            }
        }
    }
}
