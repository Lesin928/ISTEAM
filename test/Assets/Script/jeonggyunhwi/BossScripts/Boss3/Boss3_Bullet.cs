using UnityEngine;

public class Boss3_Bullet : MonoBehaviour
{
    private float attack = 4.0f;

    public GameObject target; // �÷��̾ Ÿ������
    private float Speed = 5.0f;
    public Vector2 dir;
    public Vector2 dirNo;
    public bool isReturned = false;

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        dir = target.transform.position - transform.position;
        dirNo = dir.normalized;
        
    }
    
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(dirNo * Speed * Time.deltaTime);
        
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
