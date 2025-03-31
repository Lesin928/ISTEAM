using System.Collections;
using UnityEngine;

public class FutureLazer : Weapon
{
    protected static float LazerAttackSpeed = 4f;
    protected static float LazerSpeed = 0.4f;
    private bool isOnCooldown = false; // ���� ��ٿ� ���� 
    public GameObject effect1Prefab; // ���� ����Ʈ1
    public GameObject effect2Prefab; // ���� ����Ʈ2
    public float effect1Duration = 0.67f; // ���� ����Ʈ1 ���ӽð�  


    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // ���� ���� �ӵ� -> �÷��̾� ���ݼӵ� ���
        attackSpeed = player.attackSpeed * LazerAttackSpeed;
        // ���� �̵� �ӵ� -> �÷��̾� �̵��ӵ� ���
        bulletSpeed = player.moveSpeed * LazerSpeed;
    }

    public override void CreatWeapon() //���� Ǯ ����
    {
        LazerFlying LazerFlyingPrefab = weapon.GetComponent<LazerFlying>();
        ObjectPoolManager.Instance.CreatePool<LazerFlying>("FutureLazer", weapon.GetComponent<LazerFlying>(), 3);
        // Ǯ���� ������ ��� ��ü�� ���� ��ü�� �ڽ����� ����
        for (int i = 0; i < 3; i++) // ������ ������ŭ �ݺ�
        {
            LazerFlying newWeapon = ObjectPoolManager.Instance.GetFromPool<LazerFlying>("FutureLazer");
            if (newWeapon != null)
            {
                newWeapon.transform.SetParent(transform); // ���� ��ü�� �ڽ����� ���� 
                ObjectPoolManager.Instance.ReturnToPool("FutureLazer", newWeapon.GetComponent<LazerFlying>());// �Ѿ� Ǯ�� ��ȯ 
            }
        }
    }
    public override void WeaponAttack() //���⺰ �߻��� ����
    {
        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // ���� ��ġ (FutureLazer�� ������ ��ġ) 
        // �Ѿ��� Ǯ���� ������
        LazerFlying newBullet = ObjectPoolManager.Instance.GetFromPool<LazerFlying>("FutureLazer");
        newBullet.gameObject.SetActive(false);  // �Ѿ� ��� ��Ȱ��ȭ 

        if (newBullet != null) //�Ѿ��� null�� �ƴ� �� ����
        {
            newBullet.transform.position = attackPosition.position;  // �߻� ��ġ ����
             
            // �Ѿ� ���� (����Ʈ ��� �� �߻�)
            StartCoroutine(SetupBulletSequence(newBullet, attackPosition, bulletSpeed));

            // ���� �ӵ��� ���� �߻� ���� ����             
            StartCoroutine(FireCooldown(attackSpeed)); // ���� ���� ����
        }
        else
        {
            Debug.Log("newBullet = null");
        }
    }
    private IEnumerator FireCooldown(float delay)
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(delay);
        isOnCooldown = false;
    }

    private IEnumerator SetupBulletSequence(LazerFlying newBullet, Transform attackPosition, float bulletSpeed)
    {  
        // ���� ����Ʈ1 ���
        GameObject effect1 = Instantiate(effect1Prefab, attackPosition.position, Quaternion.identity);
        effect1.transform.SetParent(transform);
        yield return new WaitForSeconds(effect1Duration); // ����Ʈ1 ���ӽð� ��� 

        // ����Ʈ2 ���
        GameObject effect2 = Instantiate(effect2Prefab, attackPosition.position, Quaternion.identity);
        effect2.transform.SetParent(transform);
        Destroy(effect2, 2); // �ʿ��ϸ� ����Ʈ ����
        newBullet.gameObject.SetActive(true);  // �Ѿ� Ȱ��ȭ 

        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // 2D ȯ�濡�� Z ��ǥ ����
            Vector2 direction = (mousePosition - attackPosition.position).normalized; // ���� ���ͷ� ����ȭ  
            rb.linearVelocity = direction * bulletSpeed;
            newBullet.LookAtMouse(rb);
            rb.linearVelocity = direction * 0; 
        }
        else
        {
            Debug.Log("rb = null");
        }

    }
}