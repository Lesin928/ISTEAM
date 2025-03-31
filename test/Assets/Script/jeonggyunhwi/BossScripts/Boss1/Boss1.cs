using System;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public GameObject target;
    public float JumpForce = 6f;
    private Rigidbody2D rb;
    private int Jump_count = 0;
    bool check = true;
    bool flag = true;
    Vector2 dir;
    Vector2 dirNo;
    SpriteRenderer rend;
    Animator ani;
    
    

    Monster boss1_monster;
    public GameObject Fire;
    public GameObject effect;

    public Transform pos;

    Player player;
    private void Awake()
    {
        boss1_monster = GetComponent<Monster>();
        player = FindFirstObjectByType<Player>();
        BossPoolManager.Instance.CreatePool(Fire, 20);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(boss1_monster.attack);
            AudioManager.Instance.PlaySFX("Boss", "Boss_Attack_Ancient");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        dir = target.transform.position - transform.position;
        dirNo = dir.normalized; 
    }

    void JumpAttack() //Ÿ��(�÷��̾ ���� ���� �� AddForce�� �����ϵ���
    {
        dir = target.transform.position - transform.position;
        dirNo = dir.normalized;
        rb.AddForce(dirNo * JumpForce, ForceMode2D.Impulse);
    }
    //���ߴ� �Լ� �����ؼ�
    void Boss1_Land()
    {
        rb.linearVelocity = Vector2.zero;
        ani.SetBool("Jump", false);
    }
    void Boss1_Fire()
    {
        Jump_count++;
        GameObject go = BossPoolManager.Instance.Get(Fire);
        go.transform.position = pos.position;
        go.GetComponent<Boss1_Fire>().isReturned = false;
        go.GetComponent<Boss1_Fire>().spriteRenderer.flipX = target.transform.position.x < transform.position.x;
        go.GetComponent<Boss1_Fire>().direction = target.transform.position - transform.position;

    }
    // Update is called once per frame
    void Update()
    {
        if (Jump_count == 4)
        {
            rb.linearVelocity = Vector2.zero;
            ani.SetBool("Jump", true);
            Jump_count = 0; 
        }

        if (target.GetComponent<Transform>().position.x < gameObject.transform.position.x) // Ÿ��(�÷��̾�)�� x �� ��ġ�� Ȯ��
        {
            rend.flipX = true;
            if (check)
            {
                rb.linearVelocity = Vector2.zero; // x���� ���� ���߰� ���� ��ȯ
                ani.SetBool("Jump", false);
                check = false;
            }
        }
        else
        {
            rend.flipX = false;
            if (!check)
            {
                rb.linearVelocity = Vector2.zero;
                ani.SetBool("Jump", false);
                check = true;
            }
        }
        if(boss1_monster.currentHp <= 95 && flag)
        {
            rb.linearVelocity = Vector2.zero;
            transform.localScale = new Vector3(10.0f, 10.0f, 1);
            rend.color = new Color(244.0f/255f, 106.0f/255f, 106.0f/255f);
            JumpForce = 10f;
            flag = false;
        }
        if (boss1_monster.currentHp <= 0) 
        {
            AudioManager.Instance.PlaySFX("Boss", "Boss_Death");
            GameObject effec1t = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
