using System.Collections;
using UnityEngine;

public class Player_test : MonoBehaviour
{
    [SerializeField]
    private Transform ms;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float moveSpeed; // 이동 속도      
    private Player player;
    private Vector2 moveInput;

    private bool canMove = true;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        if (player != null)
        {
            moveSpeed = player.moveSpeed; // Player의 이동 속도 값 가져오기
        }
        else
        {
            moveSpeed = 1;
        }
        StartCoroutine(CreateBullet());
    }
    IEnumerator CreateBullet()
    {
        while (true)
        {
            Instantiate(bullet, ms.position, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);


        }
    }
    void Update()
    {
        //이동 처리
        if (canMove) // 이동 가능할 때만 입력 처리
        {
            moveInput.x = Input.GetAxisRaw("Horizontal"); // A, D 입력
            moveInput.y = Input.GetAxisRaw("Vertical");   // W, S 입력
        }
        else
        {
            moveInput = Vector2.zero; // 멈춤
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BlackHole")) // 특정 태그 감지
        {
            canMove = false;
            rb.linearVelocity = Vector2.zero; // 즉시 멈춤
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BlackHole"))
        {
            canMove = true;
        }
    }
    

}
