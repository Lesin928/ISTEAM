using UnityEngine;

public class Homing : MonoBehaviour
{
    public GameObject target; // 플레이어를 타겟으로
    public float Speed = 3.0f;
    Vector2 dir;
    Vector2 dirNo;
    Vector2 vec2 = Vector2.down;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        dir = target.transform.position - transform.position;
        dirNo = dir.normalized;
    }
    public void Move(Vector2 vec)
    {
        vec2 = vec;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(dirNo * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        BossPoolManager.Instance.Return(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //플레이어에게 데미지 입히기

            //삭제
            BossPoolManager.Instance.Return(gameObject);
        }
    }
}
