using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedievalAxe : Weapon
{
    protected static float AxeAttackSpeed = 1.5f;
    protected static float AxeSpeed = 0.4f;
    private bool isOnCooldown = false; // ���� ��ٿ� ����

    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // ���� ���� �ӵ� -> �÷��̾� ���ݼӵ� ���
        attackSpeed = player.attackSpeed * AxeAttackSpeed;
        // ���� �̵� �ӵ� -> �÷��̾� �̵��ӵ� ���
        bulletSpeed = player.moveSpeed * AxeSpeed;
    }

    public override void CreatWeapon() //���� Ǯ ����
    {
        AxeFlying axeFlyingPrefab = weapon.GetComponent<AxeFlying>();
        ObjectPoolManager.Instance.CreatePool<AxeFlying>("MedievalAxe", weapon.GetComponent<AxeFlying>(), 3);
        // Ǯ���� ������ ��� ��ü�� ���� ��ü�� �ڽ����� ����
        for (int i = 0; i < 3; i++) // ������ ������ŭ �ݺ�
        {
            AxeFlying newWeapon = ObjectPoolManager.Instance.GetFromPool<AxeFlying>("MedievalAxe");
            if (newWeapon != null)
            {
                newWeapon.transform.SetParent(transform); // ���� ��ü�� �ڽ����� ���� 
                ObjectPoolManager.Instance.ReturnToPool("MedievalAxe", newWeapon.GetComponent<AxeFlying>());// �Ѿ� Ǯ�� ��ȯ 
            }
        }
    }
    public override void WeaponAttack() //���⺰ �߻��� ����
    {
        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // ���� ��ġ (MedievalAxe�� ������ ��ġ) 
        // �Ѿ��� Ǯ���� ������
        AxeFlying newBullet = ObjectPoolManager.Instance.GetFromPool<AxeFlying>("MedievalAxe");
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
                newBullet.transform.position += (Vector3)direction * bulletSpeed;
                rb.linearVelocity = direction * bulletSpeed;
                newBullet.LookAtMouse(rb);
                rb.linearVelocity = direction * 0;
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