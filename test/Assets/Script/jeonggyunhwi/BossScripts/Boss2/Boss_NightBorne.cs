using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss_NightBorne : MonoBehaviour
{

    //보스 행성 위 몬스터
    //구체적인 기능 구현
    //공격, 패턴, 필살기(게이지 찰때까지 체력 못깎으면 발동, 체력 깎으면 잠시동안 그로기)

    private int currentHp = 0;
    private int count = 0;
    private bool check;
    private bool check2;
    //public int Hp = 100;
    IEnumerator enumerator;
    Animator ani;
    [SerializeField]
    private GameObject red_circle;
    [SerializeField]
    private Transform ms;
    
    
    [SerializeField]
    private Boss boss;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject bullet2;
    [SerializeField]
    private GameObject bullet3;
    
    public GameObject guage;
    public Transform player;


    private void Awake()
    {
        BossPoolManager.Instance.CreatePool(bullet, 10);
        BossPoolManager.Instance.CreatePool(bullet2, 40);
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetBool("IsPattern", false);
        check = false;
        check2 = false;
        enumerator = CreateBullet();
        StartCoroutine(enumerator);
        StartCoroutine(CircleFire());
        
        guage.SetActive(false);
    }
    IEnumerator CreateBullet()
    {
        while (true)
        {
            //Instantiate(bullet, ms.position, Quaternion.identity);
            GameObject b_bullet = BossPoolManager.Instance.Get(bullet);
            b_bullet.transform.position = ms.position;
            count++;
            yield return new WaitForSeconds(1f);
            if (count == 3)
            {
                StartCoroutine(TrippleBullet());
                count = 0;
            }

        }
    }
    IEnumerator CircleFire()
    {
        //공격주기
        float AttackRate = 3f;
        //발사체 생성 갯수
        int count = 20;
        //발사체 사이의 각도
        float intervalAngle = 360 / count;
        //가중되는 각도(항상 같은 위치로 발사하지 않도록설정)
        float weightAngle = 0f;

        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                //발사체 생성
                GameObject clone = BossPoolManager.Instance.Get(bullet2);
                clone.transform.position = ms.position;
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
            weightAngle++;
            //3초마다 발사
            yield return new WaitForSeconds(AttackRate);


        }

    }

    IEnumerator TrippleBullet()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject b_bullet = BossPoolManager.Instance.Get(bullet);
        b_bullet.transform.position = ms.position;
        yield return new WaitForSeconds(0.2f);
        b_bullet = BossPoolManager.Instance.Get(bullet);
        b_bullet.transform.position = ms.position;
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
        AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);
        ani.SetBool("IsBreak", false);
        if (boss.Hp <= 90 && boss.Hp > 80 || boss.Hp <= 40 && boss.Hp > 30)
        {
            if (!check)
            {
                check = true;
                currentHp = boss.Hp;
                guage.GetComponent<Slider>().value = 0;
                StopCoroutine(enumerator);
                check2 = false;

            }
            guage.SetActive(true);
            
            ani.SetBool("IsPattern", true);


        }

        guage.GetComponent<Slider>().value += Time.deltaTime;

        if (currentHp - boss.Hp > 20)
        {
            
            ani.SetBool("IsBreak", true);
            guage.SetActive(false);
            ani.SetBool("IsPattern", false);
            //Debug.Log(stateInfo.IsName("Boss_NightBorne_Groggy"));
            if (!check2)
            {

                Debug.Log(stateInfo.normalizedTime);
                if (stateInfo.normalizedTime >= 0.71f && stateInfo.IsName("Boss_NightBorne_Groggy"))
                {
                    StartCoroutine(enumerator);
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
                StartCoroutine(enumerator);
                check2 = true;
            }
            check = false;
        }
    }
}
