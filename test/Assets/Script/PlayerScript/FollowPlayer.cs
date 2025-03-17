using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // ���� ĳ����
    public float smoothSpeed = 0.2f; // �ε巯�� �̵� �ӵ�

    private float fixedZ; // ������ Z��

    void Start()
    {
        fixedZ = transform.position.z; // �ʱ� Z�� ����
    }
       
    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, fixedZ); // Z�� ����
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed); // �ε巴�� ���󰡱�
    }
}