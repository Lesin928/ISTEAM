using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��빫�� ���� ���ݹ�� ������ �ڵ�
//������ƮǮ���� ���� ���� ����Ʈ �߻�
//���� ���, ���� ���
//������ ����� ����

public class AncientWood : Weapon
{
    protected static float WoodAttackSpeed = 1.5f; 
    protected static float WoodSpeed = 1.6f;
    private bool isOnCooldown = false; // ���� ��ٿ� ����

    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // ���� ���� �ӵ� -> �÷��̾� ���ݼӵ� ���
        attackSpeed = player.attackSpeed * WoodAttackSpeed;
        // ���� �̵� �ӵ� -> �÷��̾� �̵��ӵ� ���
        bulletSpeed = player.moveSpeed * WoodSpeed; 
    }

    public override void CreatWeapon() //���� Ǯ ����
    {
        ObjectPoolManager.Instance.CreatePool<WoodFlying>("AncientWood", weapon.GetComponent<WoodFlying>(), 10);
    }

    public override void WeaponAttack() //���⺰ �߻��� ����
    {
        AudioManager.Instance.PlaySFX("Player", "Player_Attack_Ancient");

        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // ���� ��ġ (AncientStone�� ������ ��ġ)
        // �Ѿ��� Ǯ���� ������
        WoodFlying newBullet = ObjectPoolManager.Instance.GetFromPool<WoodFlying>("AncientWood");  
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