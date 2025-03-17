using UnityEngine;

//�߻�ȭ Ŭ���� Character
//NPC�� PC ��� �ش� Ŭ������ �����
public abstract class Character : MonoBehaviour
{
    public float maxHp; //�ִ�ü��
    public float currentHp; //���� ü��
    public float armor; //����
    public float attack; //���ݷ�
    public float attackSpeed; //���ݼӵ�
    public float moveSpeed; //�̵��ӵ�  

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
    
    //���¸�ŭ ���ظ� �氨�Ͽ� ������ ����
    //�����÷ο� ������ ���� double ����� ��ȯ �� ���
    public virtual void TakeDamage(float damage)
    {
        currentHp -= (float)((double)damage * (double)damage / ((double)armor + (double)damage));
    }

    //ü���� ȸ����
    public virtual void HealHp(float Heal)
    {
        if (maxHp < currentHp + Heal)
        {
            currentHp = maxHp;
        }
        else
        {
            currentHp += Heal;
        }        
    }

}
