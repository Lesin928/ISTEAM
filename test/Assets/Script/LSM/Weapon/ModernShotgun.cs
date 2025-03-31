using System.Collections;
using UnityEngine;

public class ModernShotgun : Weapon
{

    protected static float ShotgunAttackSpeed = 1.0f;
    protected static float ShotgunSpeed = 3f;
    private bool isOnCooldown = false; // ���� ��ٿ� ����
    System.Random random = new System.Random();
    float[] angles = new float[7];

    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // ���� ���� �ӵ� -> �÷��̾� ���ݼӵ� ���
        attackSpeed = player.attackSpeed * ShotgunAttackSpeed;
        // ���� �̵� �ӵ� -> �÷��̾� �̵��ӵ� ���
        bulletSpeed = player.moveSpeed * ShotgunSpeed;
    }

    public override void CreatWeapon() //���� Ǯ ����
    {
        ObjectPoolManager.Instance.CreatePool<ShotgunFlying>("ModernShotgun", weapon.GetComponent<ShotgunFlying>(), 10);

    }
    public override void WeaponAttack() //���⺰ �߻��� ����
    {
        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // ���� ��ġ (ModernShotgun�� ������ ��ġ) 
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2D ȯ�濡�� Z ��ǥ ����
        Vector2 baseDirection = (mousePosition - attackPosition.position).normalized; // �⺻ ���� ����
        for (int i = 0; i < angles.Length; i++)
        {
            angles[i] = random.Next(-15, 16); // -15���� 15 ������ ������ ����
        }

        foreach (float angle in angles)
        {
            ShotgunFlying newBullet = ObjectPoolManager.Instance.GetFromPool<ShotgunFlying>("ModernShotgun");
            if (newBullet != null) // �Ѿ��� null�� �ƴ� �� ����
            {
                newBullet.transform.position = attackPosition.position; // �߻� ��ġ ����
                newBullet.gameObject.SetActive(true); // �Ѿ� Ȱ��ȭ

                Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // 1. �⺻ �������� �߻�
                    rb.linearVelocity = baseDirection * bulletSpeed;

                    // 2. LookAtMouse�� ȣ���ؼ� �⺻ �ð��� ȸ�� ���� (������ 45�� �������� �̹����� ���ԵǾ� ����)
                    newBullet.LookAtMouse(rb);

                    // 3. �߰������� angle��ŭ ȸ���� ���ͷ� velocity�� �缳���Ͽ� �̵� ���⵵ ����
                    Vector2 adjustedVelocity = Quaternion.Euler(0, 0, angle) * rb.linearVelocity;
                    rb.linearVelocity = adjustedVelocity;

                    // 4. ������Ʈ �̹���(ȸ��)�� angle��ŭ �߰� ȸ�� ���� (���� �̹��� 45�� ȸ���� ����)
                    newBullet.transform.Rotate(0, 0, angle);
                }
                else
                {
                    Debug.Log("rb = null");
                }
            }
        }


        // ���� �ӵ��� ���� �߻� ���� ����             
        StartCoroutine(FireCooldown(attackSpeed)); // ���� ���� ���� 
    }
    private IEnumerator FireCooldown(float delay)
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(delay);
        isOnCooldown = false;
    }

}