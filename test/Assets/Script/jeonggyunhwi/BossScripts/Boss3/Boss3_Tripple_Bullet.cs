using UnityEngine;

public class Boss3_Tripple_Bullet : MonoBehaviour
{
    private float attack = 4.0f;
    public Player target;
    public float bulletSpeed = 5f;  // �Ѿ� �ӵ�
    public float spreadAngle = 15f;  // ������ ����
    public Vector2 dir;
    public Vector2 dirNo;
    public bool isReturned = false;
    public Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindFirstObjectByType<Player>();
        rb = GetComponent<Rigidbody2D>();
        //rb.linearVelocity = dir.normalized * bulletSpeed;
    }

    
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(dir.normalized * bulletSpeed * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        if (isReturned)
        {
            return;
        }
        else
        {
            isReturned = true;
            BossPoolManager.Instance.Return(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //�÷��̾�� ������ ������
            //�߰��ؾ���
            collision.GetComponent<Player>().TakeDamage(attack);
            if (isReturned)
            {
                return;
            }
            else
            {
                //����
                isReturned = true;
                BossPoolManager.Instance.Return(gameObject);

            }
        }
    }

}
