using UnityEngine;

public class Lazer : MonoBehaviour
{
    public Transform pos3;

    Boss3 boss3;
    private float attack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 0.9f);
        boss3 = GameObject.Find("Boss3").GetComponent<Boss3>();
        pos3 = boss3.pos3.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (boss3.GetComponent<SpriteRenderer>().flipX)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            transform.position = pos3.position+new Vector3(-6.3f,0,0);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            transform.position = pos3.position + new Vector3(6.3f, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌 시 데미지주기
        //추가해야 함
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(attack);
        }
    }
}
