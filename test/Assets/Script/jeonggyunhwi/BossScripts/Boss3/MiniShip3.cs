using System;
using UnityEngine;

public class MiniShip3 : MonoBehaviour
{   
    private float speed = 1.0f;
    Player player;
    Monster miniship3_monster;
    private void Awake()
    {
        //몬스터 스탯 초기화
             
        player = FindFirstObjectByType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(miniship3_monster.attack);
            BossPoolManager.Instance.Return(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var direction = GameObject.FindWithTag("Player").transform.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);
        gameObject.GetComponent<SpriteRenderer>().flipX = transform.position.x > GameObject.FindWithTag("Player").transform.position.x;
    }
    
}
