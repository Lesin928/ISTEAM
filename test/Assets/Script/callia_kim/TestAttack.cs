using UnityEngine;

public class TestAttack : MonoBehaviour
{
    public StageManagerForBG stageManager;  //StageManagerForBG ��ũ��Ʈ ����
    public int testDamage = 50; //�⺻ ���ݷ�

    void Start()
    {
        //StageManagerForBG�� �Ҵ���� �ʾҴٸ�, ���� ������Ʈ���� ��������
        if (stageManager == null)
        {
            stageManager = GetComponent<StageManagerForBG>();
        }
    }

    void Update()
    {
        //�����̽� �ٰ� ������ ��, �������� ���ظ� �ִ� ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StageManager�� ���� ������ ��ȿ���� Ȯ��
            if (stageManager != null && stageManager.currentBoss != null)
            {
                //������ BossForBG ������Ʈ ��������
                BossForBG boss = stageManager.currentBoss.GetComponent<BossForBG>();

                if (boss != null)
                {
                    //�������� ���� �ֱ�
                    boss.TakeDamage(testDamage);
                }
            }
        }
    }
}