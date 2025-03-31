using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

//��빫�� �� ���ݹ�� ������ �ڵ�
//������ƮǮ���� ���� �� ����Ʈ �߻�
public class AncientFire : Weapon
{
    protected static float FireAttackSpeed = 1.0f;
    protected static float FireSpeed = 1.0f;
    private bool isOnCooldown = false; // ���� ��ٿ� ����

    protected override void Awake()
    {
        Player player = gameObject.GetComponentInParent<Player>();
        // ���� ���� �ӵ� -> �÷��̾� ���ݼӵ� ���
        attackSpeed = player.attackSpeed * FireAttackSpeed;
        // ���� �̵� �ӵ� -> �÷��̾� �̵��ӵ� ���
        bulletSpeed = player.moveSpeed * FireSpeed; 
    }

    public override void CreatWeapon() //���� Ǯ ����
    {
        ObjectPoolManager.Instance.CreatePool<FireFlying>("AncientFire", weapon.GetComponent<FireFlying>(), 10);
    }
    public override void WeaponAttack() //���⺰ �߻��� ����
    {
        AudioManager.Instance.PlaySFX("Player", "Player_Attack_Ancient"); 
        if (isOnCooldown)
            return;
        Transform attackPosition = weaponPos; // ���� ��ġ (AncientFire�� ������ ��ġ) 
        // �Ѿ��� Ǯ���� ������
        FireFlying newBullet = ObjectPoolManager.Instance.GetFromPool<FireFlying>("AncientFire");        
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