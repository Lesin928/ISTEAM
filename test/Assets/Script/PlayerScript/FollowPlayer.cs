using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // 따라갈 캐릭터
    public float smoothSpeed = 0.2f; // 부드러운 이동 속도

    private float fixedZ; // 고정된 Z값

    void Start()
    {
        fixedZ = transform.position.z; // 초기 Z값 저장
    }
       
    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, fixedZ); // Z값 유지
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed); // 부드럽게 따라가기
    }
}