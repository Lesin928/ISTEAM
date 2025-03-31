using UnityEngine;

public class Drone : MonoBehaviour
{
    public Transform parent; // ������ �θ� ������Ʈ
    public float orbitSpeed = 50f; // ���� �ӵ�
    public float radius = 1.5f; // ���� �ݰ�

    private Vector3 offset; // �θ�κ����� �Ÿ� ����
    private SpriteRenderer spriteRenderer;
    private Animator animator; 
    private Vector3 previousPosition;

    private void Start()
    {
        previousPosition = transform.position;
        if (parent == null)
        {
            Debug.LogError("�θ� ������Ʈ�� �������� �ʾҽ��ϴ�.");
            return;
        } 
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        offset = new Vector3(radius, 0, 0); // �ʱ� �ݰ� ����
    }

    private void Update()
    {
        if (parent == null) return; 
        // 1. �θ� �߽����� ����
        float angle = orbitSpeed * Time.deltaTime;
        offset = Quaternion.Euler(0, 0, angle) * offset;
        transform.position = parent.position + offset;

        // 2. ���콺 ���⿡ ���� Flip ����
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2D ȯ�濡�� Z ��ǥ ����

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = mousePosition.x < transform.position.x; 
        }

        // 3. �̵� ���⿡ ���� �ִϸ��̼� ���� 
        float deltaX = transform.position.x - previousPosition.x;
        if (deltaX > 0)
        {
            // ���������� �̵� ���̸� �ִϸ������� �Ķ���͸� true�� ����
            animator.SetBool("isMovingRight", true);
        }
        else if (deltaX < 0)
        {
            // �������� �̵� ���̸� false�� ����
            animator.SetBool("isMovingRight", false);
        }

        // 4. ���� ��ġ�� ���� ��ġ�� ���� (���� ������ �񱳿�)
        previousPosition = transform.position;
    }
}