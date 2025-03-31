using System.Collections;
using UnityEngine;

public class FutureDrone : Weapon
{

    protected static float DroneAttackSpeed = 0.4f;
    protected static float DroneSpeed = 1.5f;
    private bool isOnCooldown = false; // ���� ��ٿ� ����

    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // ���� ���� �ӵ� -> �÷��̾� ���ݼӵ� ���
        attackSpeed = player.attackSpeed * DroneAttackSpeed;
        // ���� �̵� �ӵ� -> �÷��̾� �̵��ӵ� ���
        bulletSpeed = player.moveSpeed * DroneSpeed;
    }

    public override void CreatWeapon() //���� Ǯ ����
    {
        ObjectPoolManager.Instance.CreatePool<DroneFlying>("FutureDrone", weapon.GetComponent<DroneFlying>(), 10);

    }
    public override void WeaponAttack() //���⺰ �߻��� ����
    {
        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // ���� ��ġ (FutureDrone�� ������ ��ġ) 
        // �Ѿ��� Ǯ���� ������
        DroneFlying newBullet = ObjectPoolManager.Instance.GetFromPool<DroneFlying>("FutureDrone");
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