using UnityEngine;

//�߻�ȭ Ŭ���� Character
//NPC�� PC ��� �ش� Ŭ������ ���
public abstract class Character : MonoBehaviour
{ 
    public float maxHp; //�ִ�ü��
    public float currentHp; //���� ü��
    public float armor; //����
    public float attack; //���ݷ�
    public float attackSpeed; //���ݼӵ�
    public float moveSpeed; //�̵��ӵ�  
    public float critical; //ġ��Ÿ Ȯ��
    public float criticalDamage; //ġ��Ÿ ����

    //HP ����
    //�ִ� ü���� �����ϰ� ���� ü���� �׿� �°� ����
    public abstract void SetHp(float hp);
    public abstract float GetHp();

    //ġ��Ÿ ���� ���� ���¸�ŭ ���ظ� �氨�Ͽ� ������ ���� 
    public abstract void TakeDamage(float damage);

    //ü���� ȸ����
    public abstract void HealHp(float Heal);


}
