using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player; // ���� ĳ����
    public float smoothSpeed = 0.2f; // �⺻ �ε巯�� �̵� �ӵ�
    public float distanceFactor = 0.1f; // �÷��̾���� �Ÿ� ���̿� ���� ���� �ӵ� ����
    private float fixedZ; // ������ Z��

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fixedZ = transform.position.z; // �ʱ� Z�� ���� 
    }

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, fixedZ); // Z�� ����

        // �÷��̾���� �Ÿ� ���
        float distance = Vector3.Distance(transform.position, targetPosition);

        // �Ÿ� ���̿� ���� �ε巯�� �̵� �ӵ� ����
        float dynamicSmoothSpeed = smoothSpeed * Mathf.Clamp(distance * distanceFactor, 0.1f, 1.0f);

        // �ε巴�� ���󰡱�
        transform.position = Vector3.Lerp(transform.position, targetPosition, dynamicSmoothSpeed);
    }
}
