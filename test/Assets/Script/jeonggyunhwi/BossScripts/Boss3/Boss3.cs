using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Boss3 : Character
{

    public GameObject shield;
    public float current_time = 0;
    public float move_time = 0;
    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;


    private SpriteRenderer spriteRenderer;
    public GameObject target;
    private Vector2 dir_V;
    private float random_dir_x;
    private float random_dir_y;
    [SerializeField]
    private GameObject MiniShip1_Prefab;
    [SerializeField]
    private GameObject MiniShip2_Prefab;
    [SerializeField]
    private GameObject MiniShip3_Prefab;  // 몬스터 프리팹
    [SerializeField]
    private GameObject Lazer_Prefab;
    [SerializeField]
    private GameObject Boss3_Bullet_Prefab;
    [SerializeField]
    private GameObject Tripple_Bullet;

    public int monsterCount = 20;     // 몬스터 개수
    public float spawnRadius = 7f;    // 원형 배치 반경

    Player player;
    Monster boss3_monster;

    private void Awake()
    {
        boss3_monster = GetComponent<Monster>();
        player = FindFirstObjectByType<Player>();
        //shield 
        BossPoolManager.Instance.CreatePool(MiniShip1_Prefab, 10);
        BossPoolManager.Instance.CreatePool(MiniShip2_Prefab, 10);
        BossPoolManager.Instance.CreatePool(MiniShip3_Prefab, 20);
        BossPoolManager.Instance.CreatePool(Boss3_Bullet_Prefab, 10);
        BossPoolManager.Instance.CreatePool(Tripple_Bullet, 30);
        

    }
    IEnumerator Bullet_Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject Boss3_Bullet = BossPoolManager.Instance.Get(Boss3_Bullet_Prefab);
            Boss3_Bullet.transform.position = pos3.transform.position;
            Boss3_Bullet.GetComponent<Boss3_Bullet>().dirNo = (player.transform.position - Boss3_Bullet.transform.position).normalized;
            Boss3_Bullet.GetComponent<Boss3_Bullet>().isReturned = false;
        }
    }

    IEnumerator SpawnMiniShip1()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameObject miniship1 = BossPoolManager.Instance.Get(MiniShip1_Prefab);
            miniship1.transform.position = pos1.transform.position;
            miniship1.GetComponent<MiniShip1>().current_position = transform.position;
            Shoot();
        }
    }
    IEnumerator SpawnMiniShip2()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameObject miniship2 = BossPoolManager.Instance.Get(MiniShip2_Prefab);
            miniship2.transform.position = pos2.transform.position;
            miniship2.GetComponent<MiniShip2>().current_position = transform.position;
            Shoot1();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(255, 255, 255, 1);
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnMiniShip1());
        StartCoroutine(SpawnMiniShip2());
        StartCoroutine(Active_Lazer());
        StartCoroutine(Bullet_Fire());
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.flipX = (target.transform.position.x < transform.position.x);
        if (!spriteRenderer.flipX) //레이저 발사 위치(pos3) 변경
        {
            pos3.transform.position = transform.position + new Vector3(2.2f, 0, 0);
        }
        else
        {
            pos3.transform.position = transform.position + new Vector3(-2.2f, 0, 0);
        }
        move_time += Time.deltaTime;
        if (move_time >= 2)
        {
            //2초마다 랜덤한 방향으로 이동
            random_dir_x = UnityEngine.Random.Range(-100, 100);
            random_dir_y = UnityEngine.Random.Range(-100, 100);
            dir_V = new Vector2(random_dir_x, random_dir_y);
            move_time = 0;
        }
        transform.Translate(dir_V.normalized * 2.5f * Time.deltaTime);
        /*if (shield.GetComponent<Monster>().currentHp <= 0)
        {
            current_time += Time.deltaTime;

            if (current_time >= 6)
            {
                //6초 후 실드 재생성
                shield.SetActive(true);
                shield.GetComponent<BossShield>().ReActivate_Shield();

                SpawnMonsters();
                current_time = 0;
            }
        }*/
    }
    void SpawnMonsters() //몬스터 원형으로 생성 및 좁혀오기
    {
        for (int i = 0; i < monsterCount; i++)
        {
            float angle = i * Mathf.PI * 2 / monsterCount; // 360도를 몬스터 개수만큼 나누기
            float x = target.transform.position.x + Mathf.Cos(angle) * spawnRadius;
            float y = target.transform.position.y + Mathf.Sin(angle) * spawnRadius;

            Vector2 spawnPosition = new Vector2(x, y);
            GameObject miniship3 = BossPoolManager.Instance.Get(MiniShip3_Prefab);
            miniship3.transform.position = spawnPosition;
        }
    }
    IEnumerator Active_Lazer() //레이저 발사
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); //밸런스 요소 ,N초 마다 레이저 발사
            spriteRenderer.flipX = (target.transform.position.x < transform.position.x);
            if (!spriteRenderer.flipX)
            {
                GameObject go = Instantiate(Lazer_Prefab, pos3.transform.position, Quaternion.identity);
            }
            else
            {
                GameObject go = Instantiate(Lazer_Prefab, pos3.transform.position, Quaternion.identity);

            }

        }
    }

    void Shoot()
    {
        float[] angles = { -(Tripple_Bullet.GetComponent<Boss3_Tripple_Bullet>().spreadAngle), 0, (Tripple_Bullet.GetComponent<Boss3_Tripple_Bullet>().spreadAngle) }; // 좌, 정면, 우

        foreach (float angle in angles)
        {
            GameObject bullet = BossPoolManager.Instance.Get(Tripple_Bullet);
            bullet.transform.position = pos1.transform.position;
            bullet.GetComponent<Boss3_Tripple_Bullet>().isReturned = false;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction;
            // 방향 계산 (회전 적용)
            if (player.transform.position.x < pos1.transform.position.x)
            {
                direction = Quaternion.Euler(0, 0, angle) * -(pos1.transform.right);
            }
            else
            {
                direction = Quaternion.Euler(0, 0, angle) * (pos1.transform.right);
            }
            //Tripple_Bullet.GetComponent<Boss3_Tripple_Bullet>().dir = direction;
            rb.linearVelocity = direction.normalized * Tripple_Bullet.GetComponent<Boss3_Tripple_Bullet>().bulletSpeed;

        }
    }
    void Shoot1()
    {
        float[] angles = { -(Tripple_Bullet.GetComponent<Boss3_Tripple_Bullet>().spreadAngle), 0, (Tripple_Bullet.GetComponent<Boss3_Tripple_Bullet>().spreadAngle) }; // 좌, 정면, 우

        foreach (float angle in angles)
        {
            GameObject bullet = BossPoolManager.Instance.Get(Tripple_Bullet);
            bullet.transform.position = pos2.transform.position;
            bullet.GetComponent<Boss3_Tripple_Bullet>().isReturned = false;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction;
            // 방향 계산 (회전 적용)
            if (player.transform.position.x < pos2.transform.position.x)
            {
                direction = Quaternion.Euler(0, 0, angle) * -(pos2.transform.right);
            }
            else
            {
                direction = Quaternion.Euler(0, 0, angle) * (pos2.transform.right);
            }
            //Tripple_Bullet.GetComponent<Boss3_Tripple_Bullet>().dir = direction;
            rb.linearVelocity = direction.normalized * Tripple_Bullet.GetComponent<Boss3_Tripple_Bullet>().bulletSpeed;

        }
    }

    //데미지 입을 때
    public override void TakeDamage(float force)
    {
        //베이스 데미지 계산
        double damge = ((double)player.attack) * ((double)force); //플레이어 공격력 * 무기 공격력 계수

        //치명타 계산
        if (UnityEngine.Random.value < player.critical)
        {
            damge = (double)player.criticalDamage * damge;
        }

        //방어력 반감 후 적용
        currentHp -= (float)(Math.Pow(damge, 2) / ((double)armor + damge));

        if (currentHp < 0)
        {
            //오브젝트풀링으로 나중에 변경!
            Destroy(gameObject);
        }

    }




}
