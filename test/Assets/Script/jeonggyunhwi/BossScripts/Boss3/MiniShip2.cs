using System;
using UnityEngine;

public class MiniShip2 : MonoBehaviour
{ 
    private float speed = 1.0f;
    private float speed1 = 5.0f;
    private SpriteRenderer spriteRenderer;
    //public GameObject pos1;
    int flag = 0;

    Boss3 boss3;
    public Vector3 current_position;
    Player player;
    Monster miniship2_monster;

    private void Awake()
    {
        //몬스터 스탯 초기화
        miniship2_monster = GetComponent<Monster>();
        player = FindFirstObjectByType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(miniship2_monster.attack);
            flag = 0;
            BossPoolManager.Instance.Return(gameObject);
        }
    }
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boss3 = GameObject.Find("Boss3").GetComponent<Boss3>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.flipX = (GameObject.Find("Player").transform.position.x < transform.position.x);
        if (flag == 0)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);

        }
        if (transform.position.y <= current_position.y - 2) //보스와의 거리 계산
        {
            flag = 1;
        }
        if (flag == 1)
        {
            var direction = GameObject.FindWithTag("Player").transform.position - transform.position;
            transform.Translate(direction.normalized * speed1 * Time.deltaTime);
        }
    }
}
