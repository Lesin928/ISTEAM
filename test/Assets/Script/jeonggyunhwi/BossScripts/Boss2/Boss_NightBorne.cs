using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss_NightBorne : MonoBehaviour
{

    //보스 행성 위 몬스터
    //구체적인 기능 구현
    //공격, 패턴, 필살기(게이지 찰때까지 체력 못깎으면 발동, 체력 깎으면 잠시동안 그로기)

    private float currentHp = 0;
    private int count = 0;
    private bool check;
    private bool check2;
    private float weightAngle = 0f; //원형 총알에 가중되는 각도(항상 같은 위치로 발사하지 않도록설정)
    
    Animator ani;
    [SerializeField]
    private GameObject red_circle;
    [SerializeField]
    private Transform ms;


    [SerializeField]
    private Boss2 boss;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject bullet2;
    [SerializeField]
    private GameObject bullet3;

    public GameObject guage;
    public Transform player;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        BossPoolManager.Instance.CreatePool(bullet, 10);
        BossPoolManager.Instance.CreatePool(bullet2, 60);
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetBool("IsPattern", false);
        check = false;
        check2 = false;
        
        player = GameObject.FindWithTag("Player").transform;
        guage.SetActive(false);
    }
    void CreateBullet()
    {
        
        {
            
            GameObject b_bullet = BossPoolManager.Instance.Get(bullet);
            b_bullet.transform.position = ms.position;
            b_bullet.GetComponent<Homing>().dirNo = (player.position - b_bullet.transform.position).normalized;
            if(player.position.x > b_bullet.transform.position.x)
            {
                b_bullet.GetComponent<Homing>().spriteRenderer.flipX = true;
            }
            else
            {
                b_bullet.GetComponent<Homing>().spriteRenderer.flipX = false;
            }
                b_bullet.GetComponent<Homing>().isReturned = false;
            count++;
            
            if (count == 3)
            {
                StartCoroutine(TrippleBullet());
                count = 0;
            }

        }
    }
    void CircleFire()
    {
        
        //발사체 생성 갯수
        int count = 20;
        //발사체 사이의 각도
        float intervalAngle = 360 / count;
        

        for (int i = 0; i < count; i++)
        {
            //발사체 생성
            GameObject clone = BossPoolManager.Instance.Get(bullet2);
            clone.transform.position = ms.position;
            if (player.position.x > clone.transform.position.x)
            {
                clone.GetComponent<BossBullet>().spriteRenderer.flipX = true;
            }
            else
            {
                clone.GetComponent<BossBullet>().spriteRenderer.flipX = false;
            }
            clone.GetComponent<BossBullet>().isReturned = false;
            //발사체 이동 방향(각도)
            float angle = weightAngle + intervalAngle * i;
            //발사체 이동 방향(벡터)
            //Cos(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            //Sin(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);

            //발사체 이동 방향 설정
            clone.GetComponent<BossBullet>().Move(new Vector2(x, y));
        }
        //발사체가 생성되는 시작 각도 설정을 위한 변수
        weightAngle+=3;
        

    }

    IEnumerator TrippleBullet()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject b_bullet = BossPoolManager.Instance.Get(bullet);
        b_bullet.transform.position = ms.position;
        if (player.position.x > b_bullet.transform.position.x)
        {
            b_bullet.GetComponent<Homing>().spriteRenderer.flipX = true;
        }
        else
        {
            b_bullet.GetComponent<Homing>().spriteRenderer.flipX = false;
        }
        b_bullet.GetComponent<Homing>().dirNo = (player.position - b_bullet.transform.position).normalized;
        b_bullet.GetComponent<Homing>().isReturned = false;
        yield return new WaitForSeconds(0.2f);
        b_bullet = BossPoolManager.Instance.Get(bullet);
        b_bullet.transform.position = ms.position;
        if (player.position.x > b_bullet.transform.position.x)
        {
            b_bullet.GetComponent<Homing>().spriteRenderer.flipX = true;
        }
        else
        {
            b_bullet.GetComponent<Homing>().spriteRenderer.flipX = false;
        }
        b_bullet.GetComponent<Homing>().dirNo = (player.position - b_bullet.transform.position).normalized;
        b_bullet.GetComponent<Homing>().isReturned = false;
        yield return new WaitForSeconds(0.2f);


    }
    void Pattern1()
    {
        for (int i = -2; i < 3; i++)
        {
            Instantiate(bullet3, player.position + new Vector3(i, i, 0), Quaternion.identity);

        }
        for (int i = -2; i < 3; i++)
        {
            if (i == 0) continue;
            Instantiate(bullet3, player.position + new Vector3(i, -i, 0), Quaternion.identity);
        }
    }
    public void CircleAlert()
    {
        for (int i = -2; i < 3; i++)
        {
            Instantiate(red_circle, player.position + new Vector3(i, i, 0), Quaternion.identity);

        }
        for (int i = -2; i < 3; i++)
        {
            if (i == 0) continue;
            Instantiate(red_circle, player.position + new Vector3(i, -i, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            
        }
        AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0); //애니메이션 정보 받아오기
        ani.SetBool("IsBreak", false);
        if (boss.GetComponent<Monster>().currentHp <= 90 && boss.GetComponent<Monster>().currentHp > 80
            || boss.GetComponent<Monster>().currentHp <= 40 && boss.GetComponent<Monster>().currentHp > 30)
        {
            if (!check)
            {
                check = true;
                currentHp = boss.GetComponent<Monster>().currentHp;
                guage.GetComponent<Slider>().value = 0;
                //StopCoroutine(enumerator);
                check2 = false;

            }
            guage.SetActive(true);
            ani.SetBool("IsPattern", true);
        }

        guage.GetComponent<Slider>().value += Time.deltaTime;
        if (ani.GetBool("IsPattern"))
        {
            if (currentHp - boss.GetComponent<Monster>().currentHp > 20) //패턴 시작 후 시간 내 체력을 깎으면 패턴 파훼 성공
            {

                ani.SetBool("IsBreak", true);
                guage.SetActive(false);
                ani.SetBool("IsPattern", false);

                if (!check2)
                {
                    if (stateInfo.normalizedTime >= 0.71f 
                        && stateInfo.IsName("Boss_NightBorne_Groggy")) // 받아온 애니메이션 정보를 통해 탄막패턴 일시정지하기,
                                                                       // 온전히 그로기타임 갖도록
                    {
                        //StartCoroutine(enumerator);
                        check2 = true;
                    }
                }
                check = false;

            }
            else if (guage.GetComponent<Slider>().value >= 4.95)
            {

                guage.SetActive(false);
                //패턴 발동
                Pattern1();
                guage.GetComponent<Slider>().value = 0;
                ani.SetBool("IsPattern", false);
                if (!check2)
                {
                    //StartCoroutine(enumerator);
                    check2 = true;
                }
                check = false;
            }
        }
    }

}
