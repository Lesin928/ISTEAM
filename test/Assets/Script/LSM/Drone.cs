using UnityEngine;

public class Drone : MonoBehaviour
{
    public Transform parent; // 공전할 부모 오브젝트
    public float orbitSpeed = 50f; // 공전 속도
    public float radius = 1.5f; // 공전 반경

    private Vector3 offset; // 부모로부터의 거리 유지
    private SpriteRenderer spriteRenderer;
    private Animator animator; 
    private Vector3 previousPosition;

    private void Start()
    {
        previousPosition = transform.position;
        if (parent == null)
        {
            Debug.LogError("부모 오브젝트가 설정되지 않았습니다.");
            return;
        } 
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        offset = new Vector3(radius, 0, 0); // 초기 반경 설정
    }

    private void Update()
    {
        if (parent == null) return; 
        // 1. 부모를 중심으로 공전
        float angle = orbitSpeed * Time.deltaTime;
        offset = Quaternion.Euler(0, 0, angle) * offset;
        transform.position = parent.position + offset;

        // 2. 마우스 방향에 따라 Flip 변경
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2D 환경에서 Z 좌표 제거

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = mousePosition.x < transform.position.x; 
        }

        // 3. 이동 방향에 따라 애니메이션 변경 
        float deltaX = transform.position.x - previousPosition.x;
        if (deltaX > 0)
        {
            // 오른쪽으로 이동 중이면 애니메이터의 파라미터를 true로 설정
            animator.SetBool("isMovingRight", true);
        }
        else if (deltaX < 0)
        {
            // 왼쪽으로 이동 중이면 false로 설정
            animator.SetBool("isMovingRight", false);
        }

        // 4. 현재 위치를 이전 위치에 저장 (다음 프레임 비교용)
        previousPosition = transform.position;
    }
}