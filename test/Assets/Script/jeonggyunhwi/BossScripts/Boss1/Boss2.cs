using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss2 : MonoBehaviour
{
    public GameObject target;
    public float JumpForce = 10f;
    public int Hp = 100;
    private Rigidbody2D rb;
    bool check = true;
    Vector2 dir;
    Vector2 dirNo;
    SpriteRenderer rend;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        dir = target.transform.position - transform.position;
        dirNo = dir.normalized;

        StartCoroutine(JumpAttack());
    }

    IEnumerator JumpAttack()
    {
        while (true)
        {
            dir = target.transform.position - transform.position;
            dirNo = dir.normalized;
            rb.AddForce(dirNo * JumpForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(2.0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //플레이어에게 데미지 입히기
        }
    }
    public void Damage(int att)
    {
        Hp -= att;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target.GetComponent<Transform>().position.x < gameObject.transform.position.x)
        {
            rend.flipX = false;
            if (check)
            {
                rb.linearVelocity = Vector2.zero;
                
                check = false;
            }

        }
        else
        {
            rend.flipX = true;
            if (!check)
            {
                rb.linearVelocity = Vector2.zero;
                
                check = true;
            }
            
        }
        
    }
}
