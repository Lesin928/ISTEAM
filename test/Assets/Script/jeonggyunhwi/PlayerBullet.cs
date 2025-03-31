using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public GameObject target; // 플레이어를 타겟으로
    public float Speed = 3.0f;
    Vector2 dir;
    Vector2 dirNo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Boss");
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
        Destroy(gameObject);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Boss"))
    //    {
    //        collision.GetComponent<Boss2>().Damage(1);
    //        Destroy(gameObject);
    //    }
    //    if (collision.gameObject.CompareTag("Shield"))
    //    {
    //        collision.GetComponent<BossShield>().Damage_Shield(3);
    //        Destroy(gameObject);
    //    }
    //    if (collision.gameObject.CompareTag("Monster"))
    //    {
    //        collision.GetComponent<MiniShip1>().Damage(10);
    //    }
    //    if (collision.gameObject.CompareTag("Monster1"))
    //    {
    //        collision.GetComponent<MiniShip2>().Damage(10);
    //    }
    //    if (collision.gameObject.CompareTag("Monster2"))
    //    {
    //        collision.GetComponent<MiniShip3>().Damage(10);
    //    }
    //}
}
