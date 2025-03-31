using System.Collections;
using UnityEngine;

public class ModernBazooka : Weapon
{

    protected static float BazookaAttackSpeed = 3.0f;
    protected static float BazookaSpeed = 1.0f;
    private bool isOnCooldown = false; // ���� ��ٿ� ����

    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // ���� ���� �ӵ� -> �÷��̾� ���ݼӵ� ���
        attackSpeed = player.attackSpeed * BazookaAttackSpeed;
        // ���� �̵� �ӵ� -> �÷��̾� �̵��ӵ� ���
        bulletSpeed = player.moveSpeed * BazookaSpeed;
    }

    public override void CreatWeapon() //���� Ǯ ����
    {
        ObjectPoolManager.Instance.CreatePool<BazookaFlying>("ModernBazooka", weapon.GetComponent<BazookaFlying>(), 10);

    }
    public override void WeaponAttack() //���⺰ �߻��� ����
    {
        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // ���� ��ġ (ModernBazooka�� ������ ��ġ) 
        // �Ѿ��� Ǯ���� ������
        BazookaFlying newBullet = ObjectPoolManager.Instance.GetFromPool<BazookaFlying>("ModernBazooka");
        if (newBullet != null) //�Ѿ��� null�� �ƴ� �� ����
        {
            newBullet.transform.position = attackPosition.position;  // �߻� ��ġ ����
            newBullet.gameObject.SetActive(true);  // �Ѿ� Ȱ��ȭ 

            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f; // 2D ȯ�濡�� Z ��ǥ ����
                Vector2 direction = (mousePosition - attackPosition.position).normalized; // ���� ���ͷ� ����ȭ 
                rb.linearVelocity = direction * bulletSpeed; // ���� �ӵ��� �߻�
                newBullet.LookAtMouse(rb);
            }
            else
            {
                Debug.Log("rb = null");
            }
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

}