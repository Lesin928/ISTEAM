using System.Collections;
using UnityEngine;

public class Player_test : MonoBehaviour
{
    [SerializeField]
    private Transform ms;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float moveSpeed; // �̵� �ӵ�      
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
            moveSpeed = player.moveSpeed; // Player�� �̵� �ӵ� �� ��������
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
        //�̵� ó��
        if (canMove) // �̵� ������ ���� �Է� ó��
        {
            moveInput.x = Input.GetAxisRaw("Horizontal"); // A, D �Է�
            moveInput.y = Input.GetAxisRaw("Vertical");   // W, S �Է�
        }
        else
        {
            moveInput = Vector2.zero; // ����
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BlackHole")) // Ư�� �±� ����
        {
            canMove = false;
            rb.linearVelocity = Vector2.zero; // ��� ����
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
