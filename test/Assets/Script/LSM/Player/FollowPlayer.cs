using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player; // 따라갈 캐릭터
    public float smoothSpeed = 0.2f; // 기본 부드러운 이동 속도
    public float distanceFactor = 0.1f; // 플레이어와의 거리 차이에 따른 보간 속도 조정
    private float fixedZ; // 고정된 Z값

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fixedZ = transform.position.z; // 초기 Z값 저장 
    }

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, fixedZ); // Z값 유지

        // 플레이어와의 거리 계산
        float distance = Vector3.Distance(transform.position, targetPosition);

        // 거리 차이에 따라 부드러운 이동 속도 조정
        float dynamicSmoothSpeed = smoothSpeed * Mathf.Clamp(distance * distanceFactor, 0.1f, 1.0f);

        // 부드럽게 따라가기
        transform.position = Vector3.Lerp(transform.position, targetPosition, dynamicSmoothSpeed);
    }
}
