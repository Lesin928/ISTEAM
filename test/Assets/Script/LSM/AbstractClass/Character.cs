using UnityEngine;

//�߻�ȭ Ŭ���� Character
//NPC�� PC ��� �ش� Ŭ������ ���
public abstract class Character : MonoBehaviour
{ 
    public float maxHp; //�ִ�ü��
    public float currentHp; //���� ü��
    public float armor; //����
    public float attack; //���ݷ�
    public float attackSpeed; //���ݼӵ� (Weapon���� Ȱ��)
    public float moveSpeed; //�̵��ӵ� (Weapon���� Ȱ��)
    public float critical; //ġ��Ÿ Ȯ�� 
    public float criticalDamage; //ġ��Ÿ ����

    //HP ����
    //�ִ� ü���� �����ϰ� ���� ü���� �׿� �°� ����
    public virtual void SetHp(float hp) 
    {
        this.maxHp = hp;
        this.currentHp = hp;
    }

    public virtual float GetHp()
    {
        return currentHp;
    }

    //ġ��Ÿ ���� ���� ���¸�ŭ ���ظ� �氨�Ͽ� ������ ���� 
    //���ʹ� ġ��Ÿ �߻�����Ŵ
    public abstract void TakeDamage(float damage); 

}
