using System.Collections;
using UnityEngine;

public class ModernSniper : Weapon
{

    protected static float SniperAttackSpeed = 2.0f;
    protected static float SniperSpeed = 4.0f;
    private bool isOnCooldown = false; // ���� ��ٿ� ����

    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // ���� ���� �ӵ� -> �÷��̾� ���ݼӵ� ���
        attackSpeed = player.attackSpeed * SniperAttackSpeed;
        // ���� �̵� �ӵ� -> �÷��̾� �̵��ӵ� ���
        bulletSpeed = player.moveSpeed * SniperSpeed;
    }

    public override void CreatWeapon() //���� Ǯ ����
    {
        ObjectPoolManager.Instance.CreatePool<SniperFlying>("ModernSniper", weapon.GetComponent<SniperFlying>(), 10);

    }
    public override void WeaponAttack() //���⺰ �߻��� ����
    {
        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // ���� ��ġ (ModernSniper�� ������ ��ġ) 
        // �Ѿ��� Ǯ���� ������
        SniperFlying newBullet = ObjectPoolManager.Instance.GetFromPool<SniperFlying>("ModernSniper");
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