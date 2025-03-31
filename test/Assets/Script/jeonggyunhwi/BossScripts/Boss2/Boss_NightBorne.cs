using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss_NightBorne : MonoBehaviour
{

    //���� �༺ �� ����
    //��ü���� ��� ����
    //����, ����, �ʻ��(������ �������� ü�� �������� �ߵ�, ü�� ������ ��õ��� �׷α�)

    private float currentHp = 0;
    private int count = 0;
    private bool check;
    private bool check2;
    private float weightAngle = 0f; //���� �Ѿ˿� ���ߵǴ� ����(�׻� ���� ��ġ�� �߻����� �ʵ��ϼ���)
    
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
        
        //�߻�ü ���� ����
        int count = 20;
        //�߻�ü ������ ����
        float intervalAngle = 360 / count;
        

        for (int i = 0; i < count; i++)
        {
            //�߻�ü ����
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
            //�߻�ü �̵� ����(����)
            float angle = weightAngle + intervalAngle * i;
            //�߻�ü �̵� ����(����)
            //Cos(����)���� ������ ���� ǥ���� ���� pi/180�� ����
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            //Sin(����)���� ������ ���� ǥ���� ���� pi/180�� ����
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);

            //�߻�ü �̵� ���� ����
            clone.GetComponent<BossBullet>().Move(new Vector2(x, y));
        }
        //�߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
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
        AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0); //�ִϸ��̼� ���� �޾ƿ���
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
            if (currentHp - boss.GetComponent<Monster>().currentHp > 20) //���� ���� �� �ð� �� ü���� ������ ���� ���� ����
            {

                ani.SetBool("IsBreak", true);
                guage.SetActive(false);
                ani.SetBool("IsPattern", false);

                if (!check2)
                {
                    if (stateInfo.normalizedTime >= 0.71f 
                        && stateInfo.IsName("Boss_NightBorne_Groggy")) // �޾ƿ� �ִϸ��̼� ������ ���� ź������ �Ͻ������ϱ�,
                                                                       // ������ �׷α�Ÿ�� ������
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
                //���� �ߵ�
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
