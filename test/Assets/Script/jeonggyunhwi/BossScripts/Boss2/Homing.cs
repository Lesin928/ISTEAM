using UnityEngine;

public class Homing : MonoBehaviour
{
    private float attack = 4.0f;

    public GameObject target; // �÷��̾ Ÿ������
    public float Speed = 3.0f;
    public Vector2 dir;
    public Vector2 dirNo;
    Vector2 vec2 = Vector2.down;
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
    public void Move(Vector2 vec)
    {
        vec2 = vec;
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
