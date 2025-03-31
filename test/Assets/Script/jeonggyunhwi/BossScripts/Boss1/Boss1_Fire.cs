using UnityEngine;

public class Boss1_Fire : MonoBehaviour
{
    private float speed = 14.0f;
    private float attack = 3.0f;
    private GameObject target;

    public Vector3 direction;
    public bool isReturned = false;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindWithTag("Player");
        direction = target.transform.position - transform.position;
        spriteRenderer.flipX = target.transform.position.x < transform.position.x;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
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
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(attack);
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
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
        
    }
}
