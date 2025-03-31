using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MiniShip1 : MonoBehaviour
{
    private Vector3 direction;
    //private int Hp = 10;
    
    private float speed = 1.0f;
    private float speed1 = 5.0f;
    private SpriteRenderer spriteRenderer;
    //public GameObject pos1;
    int flag = 0;

    Boss3 boss3;
    Monster miniship1_monster;

    public Vector3 current_position;

    Player player;
    private void Awake()
    {
        miniship1_monster = GetComponent<Monster>();
        player = FindFirstObjectByType<Player>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(miniship1_monster.attack);
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
            transform.Translate(Vector3.up * speed * Time.deltaTime);

        }
        if (transform.position.y >= current_position.y + 2) // 보스와의 거리 계산
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
