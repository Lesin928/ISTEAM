using System.Collections;
using UnityEngine;

public class FutureLightSaber : Weapon
{

    protected static float LightSaberAttackSpeed = 1.0f;
    protected static float LightSaberSpeed = 2.5f;
    private bool isOnCooldown = false; // ���� ��ٿ� ����

    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // ���� ���� �ӵ� -> �÷��̾� ���ݼӵ� ���
        attackSpeed = player.attackSpeed * LightSaberAttackSpeed;
        // ���� �̵� �ӵ� -> �÷��̾� �̵��ӵ� ���
        bulletSpeed = player.moveSpeed * LightSaberSpeed;
    }

    public override void CreatWeapon() //���� Ǯ ����
    {
        ObjectPoolManager.Instance.CreatePool<LightSaberFlying>("FutureLightSaber", weapon.GetComponent<LightSaberFlying>(), 10);

    }
    public override void WeaponAttack() //���⺰ �߻��� ����
    {
        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // ���� ��ġ (FutureLightSaber�� ������ ��ġ) 
        // �Ѿ��� Ǯ���� ������
        LightSaberFlying newBullet = ObjectPoolManager.Instance.GetFromPool<LightSaberFlying>("FutureLightSaber");
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