using System;
using System.Collections.Generic;
using UnityEngine;


public class MiniShip1 : MonoBehaviour
{
    private Vector3 direction;
    private int Hp = 10;
    [SerializeField]
    private float speed = 1.0f;
    private float speed1 = 3.0f;
    //public GameObject pos1;
    int flag = 0;

    Boss3 boss3;
    private Vector3 current_position;

    public void Damage(int att)
    {
        Hp -= att;
        if (Hp <= 0)
        {
            flag = 0;
            BossPoolManager.Instance.Return(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //플레이어와 충돌 시
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gameObject.transform.position = gameObject.GetComponent<Boss3>().pos1.transform.position;

        boss3 = GameObject.Find("Boss3").GetComponent<Boss3>();
        current_position = boss3.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == 0)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);

        }
        if (transform.position.y > current_position.y + 2)
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
