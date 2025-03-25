using UnityEngine;

public class BossForBG : MonoBehaviour
{
    public int maxHP = 100; //������ �ִ� HP
    private int currentHP;  //������ ���� HP

    [HideInInspector] public StageManagerForBG stageManager;  //StageManagerForBG ��ũ��Ʈ ����

    void Start()
    {
        //������ HP�� �ִ� HP�� �ʱ�ȭ
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        //����� ������ HP ����
        currentHP -= damage;

        //HP�� 0 ���ϰ� �Ǹ�
        if (currentHP <= 0)
        {
            //���� ����
            Die();
        }
    }

    //���� ���� �Լ�
    void Die()
    {
        if (stageManager != null)
        {
            //������ ������ StageManager�� �˸���
            stageManager.OnBossDeath();
        }
        //���� ������Ʈ ����
        Destroy(gameObject);
    }
}